using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EasyRbac.Dto.AppResource;
using EasyRbac.Dto.UserLogin;

namespace EasyRbac.Web.Controllers.SsoApi
{
    public class UserLoginController
    {
        public Task<UserTokenDto> Login(UserLoginDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<List<AppResourceDto>> GetUserResources(string userToken)
        {
            throw new NotImplementedException();
        }
    }
}
