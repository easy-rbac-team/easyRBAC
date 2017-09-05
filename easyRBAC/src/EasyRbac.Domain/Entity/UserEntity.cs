using System;
using System.Collections.Generic;
using System.Text;
using EasyRbac.Utils;

namespace EasyRbac.Domain.Entity
{
    public class UserEntity
    {
        public long Id { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string Salt { get; set; }

        public string RealName { get; set; }

        public bool Enable { get; set; }

        public string MobilePhone { get; set; }

        public List<RoleEntity> Roles { get; set; }

        public List<ApplicationEntity> Applications { get; set; }

        public List<AppResourceEntity> Resources { get; set; }

        public List<ResourceScope> ResourceScopes { get; set; }

        public static UserEntity NewUser(long id,string userName,string encryptedPwd,string salt,string realName)
        {
            var entity = new UserEntity();
            entity.Id = id;
            entity.UserName = userName;
            entity.Password = encryptedPwd;
            entity.Salt = salt;
            entity.RealName = realName;
            entity.Enable = true;
            return entity;
        }

        public bool PasswordIsMatch(string input,IEncryptHelper encryptHelper)
        {
            var encryptedPwd = encryptHelper.Sha256Encrypt($"{input}-{this.Salt}");
            return encryptedPwd == this.Password;
        }
    }
}
