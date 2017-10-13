using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;
using EasyRbac.Application.Login;
using EasyRbac.Domain.Entity;
using EasyRbac.DomainService;
using EasyRbac.Dto.User;
using EasyRbac.Web.Options;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace EasyRbac.Web.WebExtentions
{
    public class TokenAuthenticationHandler: IAuthenticationHandler
    {
        private AuthenticationScheme scheme;
        private HttpContext context;
        private ILoginService loginService;
        private AppOption appOption;
        /// <summary>
        /// The handler should initialize anything it needs from the request and scheme here.
        /// </summary>
        /// <param name="scheme">The <see cref="T:Microsoft.AspNetCore.Authentication.AuthenticationScheme" /> scheme.</param>
        /// <param name="context">The <see cref="T:Microsoft.AspNetCore.Http.HttpContext" /> context.</param>
        /// <returns></returns>
        public Task InitializeAsync(AuthenticationScheme scheme, HttpContext context)
        {
            this.scheme = scheme;
            this.context = context;
            this.loginService = this.context.RequestServices.GetService<ILoginService>();
            var appConfig = this.context.RequestServices.GetService<IOptions<AppOption>>();
            if (appConfig.Value == null)
            {
                throw new ApplicationException("need app config");
            }
            this.appOption = appConfig.Value;
            return Task.CompletedTask;
            
        }

        /// <summary>Authentication behavior.</summary>
        /// <returns>The <see cref="T:Microsoft.AspNetCore.Authentication.AuthenticateResult" /> result.</returns>
        public async Task<AuthenticateResult> AuthenticateAsync()
        {
            var authHeader = this.context.Request.Headers["Authorization"];
            
            if (AuthenticationHeaderValue.TryParse(authHeader, out var authValue))
            {
                var token = authValue.Parameter;
                return await this.TokenVerify(token);
            }
            else if(this.context.Request.Cookies.TryGetValue("token", out string token))
            {
                return await this.TokenVerify(token);
            }
            else
            {
                return AuthenticateResult.Fail("need token");
            }
        }

        private async Task<AuthenticateResult> TokenVerify(string token)
        {
            var loginService = this.context.RequestServices.GetService<ILoginService>();
            var tokenEntity = await loginService.GetTokenEntityByTokenAsync(token);
            if (tokenEntity == null)
            {
                return AuthenticateResult.Fail("token error");
            }
            if (tokenEntity.IsExpire())
            {
                return AuthenticateResult.Fail("token expired");
            }
            //eturn AuthenticateResult.Fail("token expired");
            var result = await this.GetIdentityByToken(tokenEntity.UserId);
            return AuthenticateResult.Success(new AuthenticationTicket(result, "token"));
        }

        private async Task<ClaimsPrincipal> GetIdentityByToken(long userId)
        {
            UserIdentity result = await loginService.GetUserClaimsIdentity(userId, appOption.AppCode);

            var principal = new EasyRbacPrincipal(result);
            return principal;
        }

        /// <summary>Challenge behavior.</summary>
        /// <param name="properties">The <see cref="T:Microsoft.AspNetCore.Authentication.AuthenticationProperties" /> that contains the extra meta-data arriving with the authentication.</param>
        /// <returns>A task.</returns>
        /// <summary>
        /// Override this method to deal with 401 challenge concerns, if an authentication scheme in question
        /// deals an authentication interaction as part of it's request flow. (like adding a response header, or
        /// changing the 401 result to 302 of a login page or external sign-in location.)
        /// </summary>
        /// <param name="properties"></param>
        /// <returns>A Task.</returns>
       

        public Task ChallengeAsync(AuthenticationProperties properties)
        {
            this.context.Response.StatusCode = 401;
            return Task.CompletedTask;
        }

        /// <summary>Forbid behavior.</summary>
        /// <param name="properties">The <see cref="T:Microsoft.AspNetCore.Authentication.AuthenticationProperties" /> that contains the extra meta-data arriving with the authentication.</param>
        /// <returns>A task.</returns>
        public Task ForbidAsync(AuthenticationProperties properties)
        {
            this.context.Response.StatusCode = 403;
            return Task.CompletedTask;
        }
      
    }
}
