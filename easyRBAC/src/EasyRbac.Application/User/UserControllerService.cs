using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using EasyRbac.Domain.Entity;
using EasyRbac.DomainService;
using EasyRbac.Dto;
using EasyRbac.Dto.Exceptions;
using EasyRbac.Dto.Mapper;
using EasyRbac.Dto.User;
using EasyRbac.Reponsitory.BaseRepository;
using EasyRbac.Utils;

namespace EasyRbac.Application.User
{
    public class UserControllerService : IUserControllerService
    {
        private readonly IIdGenerator _idGenerate;
        private readonly IEncryptHelper _encryptHelper;
        private readonly IRepository<UserEntity> _userRepository;
        private readonly IUserResourceDomainService _userResourceDomainService;
        private IMapper _mapper;

        /// <summary>Initializes a new instance of the <see cref="T:System.Object"></see> class.</summary>
        public UserControllerService(IIdGenerator idGenerate, IEncryptHelper encryptHelper, IRepository<UserEntity> userRepository, IMapper mapper, IUserResourceDomainService userResourceDomainService)
        {
            this._idGenerate = idGenerate;
            this._encryptHelper = encryptHelper;
            this._userRepository = userRepository;
            this._mapper = mapper;
            this._userResourceDomainService = userResourceDomainService;
        }

        public Task AddUser(CreateUserDto user)
        {
            var salt = this._encryptHelper.GenerateSalt();
            var encryptedPwd = this._encryptHelper.Sha256Encrypt($"{user.Password}-{salt}");
            var userEntity = UserEntity.NewUser(this._idGenerate.NewId(), user.UserName, encryptedPwd, salt, user.RealName);
            userEntity.MobilePhone = user.MobilePhone;
            return this._userRepository.InsertAsync(userEntity);
        }

        public async Task ChangePwd(long userId, ChangePwd change)
        {
            var userQueryResult = await this._userRepository.QueryAsync(x => x.Id == userId);

            var user = userQueryResult.FirstOrDefault();
            if (user == null)
            {
                throw new EasyRbacException("用户ID错误");
            }
            if (!user.PasswordIsMatch(change.Password, _encryptHelper))
            {
                throw new EasyRbacException("旧密码错误");
            }

            var salt = this._encryptHelper.GenerateSalt();
            var encryptedPwd = this._encryptHelper.Sha256Encrypt($"{change.Password}-{salt}");

            await this._userRepository.UpdateAsync(() => new UserEntity()
            {
                Password = encryptedPwd,
                Salt = salt
            }, x => x.Id == userId);
        }

        public Task DisableUser(long userId)
        {
            return this._userRepository.UpdateAsync(
                 () => new UserEntity()
                 {
                     Enable = false
                 },
                 x => x.Id == userId);
        }

        public async Task<UserInfoDto> GetUserInfo(long userId)
        {
            var users = await this._userRepository.QueryAsync(x => x.Id == userId);
            var user = users.FirstOrDefault();
            return this._mapper.Map<UserInfoDto>(user);

        }

        public Task ChangeResouces(long userId, long appId, List<string> resouceList)
        {
            return this._userResourceDomainService.ChangeUserResource(userId, appId, resouceList);
        }

        public async Task<PagingList<UserInfoDto>> SearchUser(string userName, int pageIndex, int pageSize)
        {
            PagingList<UserEntity> users = await this._userRepository.QueryByPagingAsync(x => x.UserName.StartsWith(userName) || x.RealName.StartsWith(userName)&&x.Enable == true, x=>x.UserName, pageIndex, pageSize);
            var result = this._mapper.Map<PagingList<UserInfoDto>>(users);
            return result;
        }

        public async Task<Dictionary<string, List<string>>> GetUserResourceIds(long userId, long appId)
        {
            var userResource = await this._userResourceDomainService.GetUserAssociationResourcesAsync(userId, appId);
            var roleResource = await this._userResourceDomainService.GetUserAssociationRolseResourcesAsync(userId, appId);
            return new Dictionary<string, List<string>>
            {
                {nameof(userResource),userResource },
                {nameof(roleResource),roleResource }
            };
        }
    }
}
