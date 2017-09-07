using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyRbac.Domain.Entity;
using EasyRbac.Dto.Exceptions;
using EasyRbac.Dto.Mapper;
using EasyRbac.Dto.User;
using EasyRbac.Reponsitory.BaseRepository;
using EasyRbac.Utils;

namespace EasyRbac.Application.User
{
    public class UserControllerService : IUserControllerService
    {
        private IIdGenerator _idGenerate;
        private IEncryptHelper _encryptHelper;
        private IRepository<UserEntity> _userRepository;

        /// <summary>Initializes a new instance of the <see cref="T:System.Object"></see> class.</summary>
        public UserControllerService(IIdGenerator idGenerate, IEncryptHelper encryptHelper, IRepository<UserEntity> userRepository)
        {
            this._idGenerate = idGenerate;
            this._encryptHelper = encryptHelper;
            this._userRepository = userRepository;
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
            return user.ToUserInfoDto();

        }

        public Task ChangeResouces(long userId, List<long> resouceList)
        {
            throw new NotImplementedException();
        }
    }
}
