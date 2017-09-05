using System;
using System.Collections.Generic;
using System.Text;

namespace EasyRbac.Dto.User
{
    public class CreateUserDto
    {
        public string UserName { get; set; }

        public string RealName { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }
    }
}
