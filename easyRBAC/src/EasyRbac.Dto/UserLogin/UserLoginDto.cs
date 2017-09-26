using System;
using System.Collections.Generic;
using System.Text;

namespace EasyRbac.Dto.UserLogin
{
    public class UserLoginDto
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        public string VerifyCode { get; set; }

        public string AppCode { get; set; }
    }
}
