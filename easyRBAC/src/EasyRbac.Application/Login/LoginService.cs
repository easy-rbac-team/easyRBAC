using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using EasyRbac.Domain.Entity;
using EasyRbac.DomainService;
using EasyRbac.Dto.AppLogin;
using EasyRbac.Dto.AppResource;
using EasyRbac.Dto.Exceptions;
using EasyRbac.Dto.UserLogin;
using EasyRbac.Reponsitory.BaseRepository;
using EasyRbac.Utils;
using EasyRbac.Web.Options;
using Microsoft.Extensions.Options;

namespace EasyRbac.Application.Login
{
    public class LoginService : ILoginService
    {
        private IRepository<LoginTokenEntity> _loginTokenRepository;
        private IRepository<UserEntity> _userRepository;
        private INumberConvert _numberConvert;
        private IEncryptHelper _encryptHelper;
        private IOptions<AppOption> _appOptions;
        private IUserResourceDomainService _userResourceDomainService;
        private IRepository<ApplicationEntity> _appRepository;
        private IMapper _mapper;

        public LoginService(IRepository<LoginTokenEntity> loginTokenRepository, INumberConvert numberConvert, IRepository<UserEntity> userRepository, IEncryptHelper encryptHelper, IUserResourceDomainService userResourceDomainService, IMapper mapper, IRepository<ApplicationEntity> appRepository, IOptions<AppOption> appOptions)
        {
            this._loginTokenRepository = loginTokenRepository;
            this._numberConvert = numberConvert;
            this._userRepository = userRepository;
            this._encryptHelper = encryptHelper;
            this._userResourceDomainService = userResourceDomainService;
            this._mapper = mapper;
            this._appRepository = appRepository;
            this._appOptions = appOptions;
        }

        public async Task<LoginTokenEntity> GetEntityByTokenAsync(string token)
        {
            var result = await this._loginTokenRepository.QueryFirstAsync(x => x.Token == token);
            return result;
        }

        public async Task<UserTokenDto> UserLoginAsync(UserLoginDto login)
        {
            UserEntity userEntity = await this._userRepository.QueryFirstAsync(x => x.UserName == login.UserName && x.Enable);
           
            bool loginSucces = userEntity?.PasswordIsMatch(login.Password, this._encryptHelper)??false;
            if (!loginSucces)
            {
                throw new EasyRbacException("用户名/密码错误");
            }
            var token = new LoginTokenEntity()
            {
                UserId = userEntity.Id,
                CreateOn = DateTime.Now,
                ExpireIn = _appOptions.Value.UserLoginExpireIn,
                Token = $"U{this._numberConvert.ToString(userEntity.Id)}-{DateTime.Now:MMddHHmmss}-{Guid.NewGuid():N}"
            };
            await this._loginTokenRepository.InsertAsync(token);
          
            return new UserTokenDto()
            {
                ExpireIn = token.ExpireIn,
                Schema = "token",
                Token = token.Token,
            };
        }

        public async Task<string> GetAppLoginCallback(string appCode)
        {
            var url = await this._appRepository.QueryAndSelectFirstOrDefaultAsync<string>(x => x.AppCode == appCode,x=>x.CallbackUrl);
            return url;
        }

        public async Task<AppLoginResult> AppLoginAsync(AppLoginDto request)
        {
            var appEntity = await this._appRepository.QueryFirstAsync(x => x.AppCode == request.AppCode);
            if (appEntity.AppScret != request.AppSecret)
            {
                throw new EasyRbacException("app code/securet erro");
            }
            var login = new LoginTokenEntity()
            {
                UserId = appEntity.Id,
                CreateOn = DateTime.Now,
                ExpireIn = (int)TimeSpan.FromDays(1).TotalSeconds,
                Token = $"A{this._numberConvert.ToString(appEntity.Id)}-{DateTime.Now:MMddHHmmss}-{Guid.NewGuid():N}"
            };
            await this._loginTokenRepository.InsertAsync(login);
            return new AppLoginResult()
            {
                ExpireIn = login.ExpireIn,
                Token = login.Token
            };
        }

        public Task UserLogout(string token)
        {
            return this._loginTokenRepository.UpdateAsync(
                () => new LoginTokenEntity()
                {
                    ExpireIn = 0
                },
                x => x.Token == token);
        }

        public async Task<List<AppResourceDto>> GetUserAppResourcesAsync(long userId, long appId)
        {
            List<AppResourceDto> resources = await this._userResourceDomainService.GetUserAllAppResourcesAsync(userId, appId);
            return resources;
        }

        public async Task<ClaimsIdentity> GetUserClaimsIdentity(long userId, string appCode)
        {
            var appEntity = await this._appRepository.QueryFirstAsync(x => x.AppCode == appCode && x.Enable);
            List<AppResourceDto> appResourceDtos = await this.GetUserAppResourcesAsync(userId, appEntity.Id);
            IEnumerable<Claim> claims = appResourceDtos.Select(x => new Claim("token", x.ResourceCode));
            var claimsIdentity = new ClaimsIdentity(claims);
            claimsIdentity.Actor = new GenericIdentity(userId.ToString(),"user");
            
            return claimsIdentity;
        }
    }
}
