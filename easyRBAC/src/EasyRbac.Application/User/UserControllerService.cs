using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EasyRbac.Domain.Entity;
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
    }
}
