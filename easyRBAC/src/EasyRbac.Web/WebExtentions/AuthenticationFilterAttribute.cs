using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using EasyRbac.Application.Login;
using EasyRbac.Domain.Entity;
using EasyRbac.Dto.User;
using EasyRbac.Web.Options;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace EasyRbac.Web.WebExtentions
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
    public class ResourceTagAttribute : Attribute, IAsyncAuthorizationFilter
    {
        public string[] ResourceName { get; }

        /// <summary>Initializes a new instance of the <see cref="T:System.Attribute"></see> class.</summary>
        public ResourceTagAttribute(params string[] resourceName)
        {
            this.ResourceName = resourceName;
        }

        /// <summary>
        /// Called early in the filter pipeline to confirm request is authorized.
        /// </summary>
        /// <param name="context">The <see cref="T:Microsoft.AspNetCore.Mvc.Filters.AuthorizationFilterContext" />.</param>
        /// <returns>
        /// A <see cref="T:System.Threading.Tasks.Task" /> that on completion indicates the filter has executed.
        /// </returns>
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            ClaimsPrincipal pricipal = context.HttpContext.User;

            if (!pricipal.Identity.IsAuthenticated)
            {
                this.Challenge(context);
                return;
            }

            var success = this.Authentica(pricipal);
            if (!success)
            {
                this.Forbid(context);
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
            var result = principal.Claims.Any(x => this.ResourceName.Contains(x.Value));
            return result;
        }
        
    }
}
