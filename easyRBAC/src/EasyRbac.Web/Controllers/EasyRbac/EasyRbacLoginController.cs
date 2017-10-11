using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using EasyRbac.Application.Application;
using EasyRbac.Application.Login;
using EasyRbac.Dto.AppResource;
using EasyRbac.Utils;
using EasyRbac.Web.Options;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace EasyRbac.Web.Controllers.EasyRbac
{
    [Route("easyRBAC/login")]
    public class EasyRbacLoginController : Controller
    {
        private readonly ILoginService _loginService;
        private readonly IApplicationService _applicationService;
        private IOptions<AppOption> _appOptions;

        public EasyRbacLoginController(ILoginService loginService, IApplicationService applicationService, IOptions<AppOption> appOptions)
        {
            this._loginService = loginService;
            this._applicationService = applicationService;
            this._appOptions = appOptions;
        }

        [HttpGet()]
        public async Task<string> LoginCheck(string token,string callback)
        {
            var tokenResult = await this._loginService.GetEntityByTokenAsync(token);
            if (tokenResult == null)
            {
                return string.Format("{0}({1})", callback, "{\"success\":false,\"message\":\"token not exist\"}");
            }

            if (tokenResult.IsExpire())
            {
                return string.Format("{0}({1})", callback, "{\"success\":false,\"message\":\"token is expired\"}");
            }

            this.Response.Cookies.Append("token",token,new CookieOptions()
            {
                SameSite = SameSiteMode.None
            });
            
            return string.Format("{0}({1})", callback, "{\"success\":true}");
        }

        [HttpGet("userMenu")]
        [Authorize]
        public async Task<List<AppResourceDto>> GetMenus()
        {
            var app = await this._applicationService.GetOneAsync(this._appOptions.Value.AppCode);
            var identity = this.User.Identity as ClaimsIdentity;
            var userId = long.Parse(identity.Actor.Name);

            List<AppResourceDto> resource = await this._loginService.GetUserAppResourcesAsync(userId,app.Id);

            var tree = resource.ToToMultiTree(x => x.Id, x => x.Id.Substring(0, x.Id.Length - 2));
            return tree;
        }
    }
}
