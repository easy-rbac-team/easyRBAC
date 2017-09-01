using EasyRbac.Domain.Entity;
using EasyRbac.Dto.User;
using EasyRbac.Reponsitory.BaseRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace EasyRbac.Service
{
    public class UserService
    {
        IRepository<UserEntity> _userRepository;

        public UserService(IRepository<UserEntity> userRepository)
        {
            this._userRepository = userRepository;
        }

        public void AddUser(CreateUserDto user)
        {
            var entity = new UserEntity();
            
        }
    }
}
