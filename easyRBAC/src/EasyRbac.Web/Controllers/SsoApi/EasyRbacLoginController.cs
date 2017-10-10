using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Cors;

namespace EasyRbac.Web.Controllers.SsoApi
{
    [Route("easyRBAC/login")]
    public class EasyRbacLoginController : Controller
    {
        [HttpGet()]
        public string LoginCheck(string token,string callback)
        {
            this.Response.Cookies.Append("token",token,new CookieOptions()
            {
                SameSite = SameSiteMode.None
            });
            
            return string.Format("{0}({1})", callback, "{success:true}");
        }
    }
}
