using System;
using System.Collections.Generic;
using System.Text;
using EasyRbac.Domain.Enums;
using MyUtility.Commons.Encrypt;
using SQLinq;

namespace EasyRbac.Domain.Entity
{
    [SQLinqTable("application")]
    public class ApplicationEntity
    {
        public long Id { get; set; }
        public string AppName { get; set; }

        public string AppCode { get; set; }

        public bool Enable { get; set; }

        public DateTime CreateTime { get; set; } = DateTime.Now;

        public string Descript { get; set; }

        [SQLinqColumn(Ignore = true)]
        public List<AppResourceEntity> AppResouce { get; set; } = new List<AppResourceEntity>();

        public long AppUserId { get; set; }

        [SQLinqColumn(Ignore =true)]
        public UserEntity Account { get; set; }

        [SQLinqColumn(Ignore = true)]
        public List<ApplicationCallbackConfig> CallbackConfigs { get; set; } = new List<ApplicationCallbackConfig>();

        [SQLinqColumn(Ignore = true)]
        public List<RoleEntity> UserRole { get; set; } = new List<RoleEntity>();

        public string HomePageUrl { get; set; }

        public string IconUrl { get; set; }

        public void ChangeSecuret(string newPassword, IEncryptHelper encryptHelper)
        {
            this.Account.ChangePassword(newPassword, encryptHelper);
        }
    }
}
