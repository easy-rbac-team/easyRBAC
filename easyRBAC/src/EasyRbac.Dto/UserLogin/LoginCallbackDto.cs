using EasyRbac.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace EasyRbac.Dto.UserLogin
{
    public class LoginCallbackDto 
    {
        public string CallbackUrl { get; set; }

        public CallbackType? CallbackType { get; set; }
    }
}
