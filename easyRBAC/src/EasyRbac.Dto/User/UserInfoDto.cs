using System;
using System.Collections.Generic;
using System.Text;

namespace EasyRbac.Dto.User
{
    public class UserInfoDto
    {
        public string UserName { get; set; }

        public string RealName { get; set; }

        public string MobilePhone { get; set; }

        public bool Enable { get; set; }
    }
}
