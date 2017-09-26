using System;
using System.Collections.Generic;
using System.Text;

namespace EasyRbac.Dto.AppLogin
{
    public class AppLoginResult
    {
        public string Token { get; set; }

        public int ExpireIn { get; set; }
    }
}
