using System;
using System.Collections.Generic;
using System.Text;
using EasyRbac.Domain.Enums;
using EasyRbac.Utils;
using MyUtility.Commons.Encrypt;
using SQLinq;

namespace EasyRbac.Domain.Entity
{
    [SQLinqTable("user")]
    public class UserEntity
    {
        public long Id { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string Salt { get; set; }

        public string RealName { get; set; }

        public bool Enable { get; set; } = true;

        public string MobilePhone { get; set; }

        public DateTime CreateTime { get; set; } = DateTime.Now;
        
        [SQLinqColumn(Ignore = true)]
        public List<ApplicationEntity> Applications { get; set; }

        [SQLinqColumn(Ignore = true)]
        public List<AppResourceEntity> Resources { get; set; }

        [SQLinqColumn(Ignore = true)]
        public List<UserManageResourceScope> ResourceScopes { get; set; }

        public void ChangePassword(string newPassword, IEncryptHelper encryptHelper)
        {
            this.Salt = encryptHelper.GenerateSalt();
            var encryptedPwd = EncryptPassword(newPassword, this.Salt, encryptHelper);
            this.Password = encryptedPwd;
        }

        public AccountType AccountType { get; set; } = AccountType.User;

        public static UserEntity NewUser(long id, string userName, string password, string realName, IEncryptHelper encryptHelper)
        {
            var salt = encryptHelper.GenerateSalt();

            var entity = new UserEntity();
            entity.Id = id;
            entity.UserName = userName;
            entity.Password = EncryptPassword(password, salt, encryptHelper);
            entity.Salt = salt;
            entity.RealName = realName;
            entity.Enable = true;
            return entity;
        }

        public bool PasswordIsMatch(string input, IEncryptHelper encryptHelper)
        {
            var encryptedPwd = EncryptPassword(input, this.Salt, encryptHelper);
            return encryptedPwd == this.Password;
        }

        public static string EncryptPassword(string password, string salt, IEncryptHelper encryptHelper)
        {
            var encryptedPwd = encryptHelper.Sha256Encrypt($"{password}-{salt}");
            return encryptedPwd;
        }
    }
}
