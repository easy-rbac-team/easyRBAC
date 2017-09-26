using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyRbac.Dto.AppLogin;
using EasyRbac.Dto.User;

namespace EasyRbac.Web.Controllers.SsoApi
{
    public class AppLoginController
    {
        public Task<AppLoginResult> AppLogin(AppLoginDto dto)
        {
            throw new NotImplementedException();
        }
        
        public Task<UserInfoDto> GetUserInfo(string appToken, string userToken)
        {
            throw new NotImplementedException();
        }
    }
}
