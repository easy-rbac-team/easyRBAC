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
    }
}
