using System;
using System.Collections.Generic;
using System.Text;
using SQLinq;

namespace EasyRbac.Domain.Entity
{
    [SQLinqTable("login_token")]
    public class LoginTokenEntity
    {
        public string Token { get; set; }

        public long UserId { get; set; }

        public DateTime CreateOn { get; set; }

        public int ExpireIn { get; set; }

        public string AppCode { get; set; }

        public bool IsExpire()
        {
            return this.CreateOn.AddSeconds(this.ExpireIn) < DateTime.Now;
        }

        public static LoginTokenEntity NewLoginToken(UserEntity user, int expireIn,string appCode)
        {
            var loginToken = new LoginTokenEntity();
            loginToken.CreateOn = DateTime.Now;
            loginToken.UserId = user.Id;
            loginToken.Token = $"{user.AccountType}-{user.Id}-{loginToken.CreateOn:MMddHHmmss}-{Guid.NewGuid():N}";
            loginToken.ExpireIn = expireIn;
            loginToken.AppCode = appCode;
            return loginToken;
        }
    }
}
