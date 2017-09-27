using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyRbac.Application.Resources;
using EasyRbac.Dto.AppResource;
using Microsoft.AspNetCore.Mvc;

namespace EasyRbac.Web.Controllers
{
    [Route("[controller]")]
    public class AppResourceController : Controller
    {
        private readonly IAppResourceService _resourceService;

        public AppResourceController(IAppResourceService resourceService)
        {
            this._resourceService = resourceService;
        }

        [HttpPost("{parentId}")]
        public Task AddResource(string parentId,[FromBody]AppResourceDto resource)
        {
            return this._resourceService.AddResourceAsync(resource, parentId);
        }

        [HttpDelete]
        public Task DisableResource(string id)
        {
            return this._resourceService.DisableResourceAsync(id);
        }

        [HttpPut("{id}")]
        public Task EditResource(string id,[FromBody] AppResourceDto Resource)
        {
            return this._resourceService.EditAsync(id, Resource);
        }

        [HttpGet("{id}")]
        public Task<AppResourceDto> GetResourceInfo(string id)
        {
            return this._resourceService.GetOneAsync(id);
        }

        [HttpGet("app/{appId}")]
        public Task<List<AppResourceDto>> GetAppResource(long appId)
        {
            return this._resourceService.GetAppResourceAsync(appId);
        }

        [HttpGet("tree/{parentResourceId}")]
        public Task<List<AppResourceDto>> GetResourceTree(string parentResourceId)
        {
            return this._resourceService.GetResourceTreeAsync(parentResourceId);
        }

        [HttpGet("{userId}/Resources/{appId}")]
        public Task<List<AppResourceDto>> GetUserResource(long appId,long userId)
        {
            return this._resourceService.GetUserResourceAsync(appId, userId);
        }

        [HttpGet("role/{roleId}/{appId}")]
        public Task<List<string>> GetRoleResourceIds(long roleId,long appId)
        {
            return this._resourceService.GetRoleResourceIdsAsync(appId, roleId);
        }

        [HttpPut("role/{roleId}")]
        public Task ChangeRoleResources(long roleId, [FromBody]List<string> resourceIds)
        {
            return this._resourceService.ChangeRoleResourcesAsync(roleId, resourceIds);
        }
    }
}