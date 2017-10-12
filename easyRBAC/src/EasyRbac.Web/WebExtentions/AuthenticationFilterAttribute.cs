using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyRbac.Application.Login;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace EasyRbac.Web.WebExtentions
{
    [AttributeUsage(AttributeTargets.Class| AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
    public class ResourceTagAttribute : Attribute, IAuthorizationFilter
    {
        public string ResourceName { get; }

        /// <summary>Initializes a new instance of the <see cref="T:System.Attribute"></see> class.</summary>
        public ResourceTagAttribute(string resourceName)
        {
            this.ResourceName = resourceName;
        }

        /// <summary>
        /// Called early in the filter pipeline to confirm request is authorized.
        /// </summary>
        /// <param name="context">The <see cref="T:Microsoft.AspNetCore.Mvc.Filters.AuthorizationFilterContext" />.</param>
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var loginService = context.HttpContext.RequestServices.GetService<ILoginService>();
        }
    }
}
