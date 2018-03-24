using System;
using System.ComponentModel;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EasyRbac.Web.WebExtentions
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
    public class ResourceTagAttribute : Attribute, IAuthorizationFilter
    {
        /// <inheritdoc />
        /// <summary>Initializes a new instance of the <see cref="T:System.Attribute"></see> class.</summary>
        public ResourceTagAttribute(params string[] resourceName)
        {
            ResourceName = resourceName;
        }

        public string[] ResourceName { get; private set; }

        /// <inheritdoc />
        /// <summary>
        /// </summary>
        /// <param name="context"></param>
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (context.Filters.Any(x => x.GetType() == typeof(AllowAnonymousFilter)))
            {
                return;
            }
            var pricipal = context.HttpContext.User;

            if (!this.ResourceName.Any())
            {
                var descriptor = context.ActionDescriptor as ControllerActionDescriptor;
                this.ResourceName = new[] { descriptor?.ActionName };
            }

            if (!pricipal.Identity.IsAuthenticated)
            {
                Challenge(context);
                return;
            }

            var success = Authentica(pricipal);
            if (!success)
            {
                Forbid(context);
                return;
            }

            context.HttpContext.User = pricipal;
        }

        private void Challenge(AuthorizationFilterContext context)
        {
            context.Result = new ChallengeResult();
        }

        private void Forbid(AuthorizationFilterContext context)
        {
            context.Result = new ForbidResult();
        }

        private bool Authentica(ClaimsPrincipal principal)
        {
            var result = principal.Claims.Any(x => ResourceName.Contains(x.Value));
            return result;
        }
    }
}