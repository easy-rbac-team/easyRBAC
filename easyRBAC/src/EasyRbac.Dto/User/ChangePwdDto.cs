using System;
using System.Collections.Generic;
using System.Text;

namespace EasyRbac.Dto.User
{
    public class ChangePwd
    {
        public string CurrentPassword { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }
    }
}
