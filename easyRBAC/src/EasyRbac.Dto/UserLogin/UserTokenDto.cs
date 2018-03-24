using System;
using System.Collections.Generic;
using System.Text;

namespace EasyRbac.Dto.UserLogin
{
    public class UserTokenDto
    {
        public string Schema { get; set; }

        public string Token { get; set; }

        public int ExpireIn { get; set; }
        public string AppCode { get; set; }
    }
}
