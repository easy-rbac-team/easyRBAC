using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using EasyRbac.Application.Application;
using EasyRbac.Application.Login;
using EasyRbac.Application.User;
using EasyRbac.Domain.Entity;
using EasyRbac.Dto.Application;
using EasyRbac.Dto.AppResource;
using EasyRbac.Dto.Exceptions;
using EasyRbac.Dto.User;
using EasyRbac.Web.WebExtentions;
using Microsoft.AspNetCore.Mvc;
using MyUtility.CollectionExtentions;

namespace EasyRbac.Web.Controllers.SsoApi
{
    public class AppLoginController : Controller
    {
        private readonly ILoginService _loginService;
        private readonly IUserService _userService;
        private readonly IApplicationService _applicationService;

        public AppLoginController(ILoginService loginService, IUserService userService,IApplicationService applicationService)
        {
            this._loginService = loginService;
            this._userService = userService;
            this._applicationService = applicationService;
        }

        [HttpGet("app/user/{userToken}")]
        [ResourceTag("AppGetUserInfo")]
        public async Task<UserInfoDto> GetUserInfo(string userToken)
        {
            (var userId, var appId) = await this.GetBaseInfo(userToken);

            var userInfo = await this._userService.GetUserInfo(userId);
            userInfo.Roles = await this._loginService.GetUserRoles(userId);
            return userInfo;
        }

        [HttpGet("app/resource/{userToken}")]
        [ResourceTag("AppGetUserResources")]
        public async Task<List<AppResourceDto>> GetUserResources(string userToken)
        {
            (var userId, var appId) = await this.GetBaseInfo(userToken);
            var result = await this._loginService.GetUserAppResourcesAsync(userId, appId);
            result = result.ToToMultiTree(x => x.Id, x => x.Id.Substring(0, x.Id.Length - 2));
            return result;
        }

        private async Task<(long, long)> GetBaseInfo(string userToken)
        {
            LoginTokenEntity userTokenEntity = await this._loginService.GetTokenEntityByTokenAsync(userToken);
            var identity = this.User.Identity as UserIdentity;
            var appInfo = await this._applicationService.GetAppByUserId(identity.UserId);
            
            //TODO:需要加上
            //if (userTokenEntity.IsExpire())
            //{
            //    throw new EasyRbacException("token expired");
            //}

            
           
            return (userTokenEntity.UserId,appInfo.Id);
        }
    }
}
