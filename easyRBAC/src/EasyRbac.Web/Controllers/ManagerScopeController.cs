using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using EasyRbac.Application.UserManageScope;
using EasyRbac.Dto.AppResource;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EasyRbac.Web.Controllers
{
    [Route("[controller]")]
    public class ManagerScopeController : Controller
    {
        private IManagerScopeService _managerScopeService;

        public ManagerScopeController(IManagerScopeService managerScopeService)
        {
            this._managerScopeService = managerScopeService;
        }

        [HttpPut("{userId}/{appId}")]
        public Task SetManagerScope(long userId, long appId,[FromBody] List<string> resources)
        {
            return this._managerScopeService.ChangeScopeAsync(userId, appId, resources);
        }

        [HttpGet("{userId}/{appId}")]
        public Task<List<string>> GetManagedScopeIds(long userId, long appId)
        {
            return this._managerScopeService.GetScopeIdsAsync(userId, appId);
        }

        [HttpPut("userResource/{userId}/{appId}")]
        public Task ChangeUserResource(long userId,long appId,[FromBody]List<string> ids)
        {
            var identity = this.User.Identity as ClaimsIdentity;
            var operatorId = long.Parse(identity.Actor.Name);
            return this._managerScopeService.ChangeUserResource(operatorId, userId, appId, ids);
        }

        [HttpGet("manage")]
        [Authorize]
        public Task<List<AppAndResourceDto>> GetManagedResourceAndApp()
        {
            var identity = this.User.Identity as ClaimsIdentity;
            var userId = long.Parse(identity.Actor.Name);
            var result = this._managerScopeService.GetUserManagedResourceAsync(userId);
            return result;
        }
    }
}
