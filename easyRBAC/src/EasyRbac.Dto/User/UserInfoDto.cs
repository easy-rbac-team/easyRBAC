using EasyRbac.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace EasyRbac.Dto.User
{
    public class UserInfoDto
    {
        public long Id { get; set; }
        public string UserName { get; set; }

        public string RealName { get; set; }

        public string MobilePhone { get; set; }

        public bool Enable { get; set; }

        public List<string> Roles { get; set; } = new List<string>();

        public AccountType AccountType { get; set; }
    }
}
