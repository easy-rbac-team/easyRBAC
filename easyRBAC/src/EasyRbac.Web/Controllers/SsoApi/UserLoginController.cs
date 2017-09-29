using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EasyRbac.Application.Login;
using EasyRbac.Dto.AppResource;
using EasyRbac.Dto.UserLogin;
using Microsoft.AspNetCore.Mvc;

namespace EasyRbac.Web.Controllers.SsoApi
{
    [Route("/sso/[controller]")]
    public class UserLoginController
    {
        private ILoginService _loginService;

        public UserLoginController(ILoginService loginService)
        {
            this._loginService = loginService;
        }

        [HttpPost]
        public Task<UserTokenDto> Login([FromBody]UserLoginDto dto)
        {
            return this._loginService.UserLoginAsync(dto);
        }

        public Task<List<AppResourceDto>> GetUserResources(string userToken)
        {
            throw new NotImplementedException();
        }
    }
}
