using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using EasyRbac.Domain.Entity;
using EasyRbac.Domain.Enums;
using EasyRbac.DomainService;
using EasyRbac.Dto.AppResource;
using EasyRbac.Dto.Exceptions;
using EasyRbac.Dto.User;
using EasyRbac.Dto.UserLogin;
using EasyRbac.Reponsitory;
using EasyRbac.Reponsitory.BaseRepository;
using EasyRbac.Utils;
using EasyRbac.Web.Options;
using Microsoft.Extensions.Options;
using MyUtility.CollectionExtentions;
using MyUtility.Commons.Encrypt;
using MyUtility.Commons.NumberConvert;

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
        private IApplicationRepository _appRepository;
        private IUserRoleDomainService userRoleDomainService;
        private IMapper _mapper;


        public LoginService(IRepository<LoginTokenEntity> loginTokenRepository, INumberConvert numberConvert, IRepository<UserEntity> userRepository, IEncryptHelper encryptHelper, IUserResourceDomainService userResourceDomainService, IMapper mapper, IApplicationRepository appRepository, IOptions<AppOption> appOptions, IUserRoleDomainService userRoleDomainService)
        {
            this._loginTokenRepository = loginTokenRepository;
            this._numberConvert = numberConvert;
            this._userRepository = userRepository;
            this._encryptHelper = encryptHelper;
            this._userResourceDomainService = userResourceDomainService;
            this._mapper = mapper;
            this._appRepository = appRepository;
            this._appOptions = appOptions;
            this.userRoleDomainService = userRoleDomainService;
        }

        public async Task<LoginTokenEntity> GetTokenEntityByTokenAsync(string token)
        {
            var result = await this._loginTokenRepository.QueryFirstAsync(x => x.Token == token);
            return result;
        }

        public async Task<UserTokenDto> UserLoginAsync(UserLoginDto login)
        {
            UserEntity userEntity = await this._userRepository.QueryFirstAsync(x => x.UserName == login.UserName && x.Enable);

            bool loginSucces = userEntity?.PasswordIsMatch(login.Password, this._encryptHelper) ?? false;
            if (!loginSucces)
            {
                throw new EasyRbacException("用户名/密码错误");
            }

            var expireIn = userEntity.AccountType == AccountType.User ? _appOptions.Value.UserLoginExpireIn : _appOptions.Value.AppLoginExpireIn;
            var token = LoginTokenEntity.NewLoginToken(userEntity, expireIn, login.AppCode);
            await this._loginTokenRepository.InsertAsync(token);

            return new UserTokenDto()
            {
                AppCode = login.AppCode,
                ExpireIn = token.ExpireIn,
                Schema = "token",
                Token = token.Token,
            };
        }

        public async Task<LoginCallbackDto> GetAppLoginCallback(string appCode,string env)
        {
            //var url = await this._appRepository.QueryAndSelectFirstAsync<LoginCallbackDto>(x => x.AppCode == appCode, x => new LoginCallbackDto { CallbackUrl = x.CallbackUrl, CallbackType = x.CallbackType });
            //return url;
            env = string.IsNullOrEmpty(env) ? "prod" : env;
            var appInfo = await this._appRepository.GetAppInfoEntityAsync(x => x.AppCode == appCode);
            var callbackInfo = appInfo.CallbackConfigs.Where(x => x.Enviroment == env).FirstOrDefault();
            return new LoginCallbackDto()
            {
                CallbackType = callbackInfo?.CallbackType,
                CallbackUrl = callbackInfo?.CallbackUrl
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

        public virtual async Task<List<AppResourceDto>> GetUserAppResourcesAsync(long userId, long appId)
        {
            List<AppResourceDto> resources = await this._userResourceDomainService.GetUserAllAppResourcesAsync(userId, appId);
            //var result = resources.ToToMultiTree(x => x.Id, x => x.Id.Substring(0, x.Id.Length - 2));
            return resources;
        }

        public async Task<UserIdentity> GetUserClaimsIdentity(long userId, string appCode)
        {
            var appEntity = await this._appRepository.QueryFirstAsync(x => x.AppCode == appCode && x.Enable);

            List<AppResourceDto> appResourceDtos = await this.GetUserAppResourcesAsync(userId, appEntity.Id);

            var userEntity = await this._userRepository.QueryFirstAsync(x => x.Id == userId);

            return new UserIdentity(userEntity, appResourceDtos);
        }

        public async Task<List<string>> GetUserRoles(long userId)
        {
            var entities = await this.userRoleDomainService.GetRolesAsync(userId);
            return entities.Select(x => x.RoleName).ToList();
        }
    }
}
