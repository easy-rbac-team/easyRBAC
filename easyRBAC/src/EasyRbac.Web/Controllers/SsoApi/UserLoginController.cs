using EasyRbac.Application.Login;
using EasyRbac.Dto.UserLogin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EasyRbac.Web.Controllers.SsoApi
{
    [Route("/sso/[controller]")]
    [AllowAnonymous]
    public class UserLoginController : Controller
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

        [HttpGet("callbackUrl")]
        public Task<LoginCallbackDto> GetAppCallback(string appCode)
        {
            return this._loginService.GetAppLoginCallback(appCode);
        }
    }
}
