using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyRbac.Application.Resources;
using EasyRbac.Dto.AppResource;
using EasyRbac.Web.WebExtentions;
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

        [ResourceTag("AddResource")]
        [HttpPost("{parentId}")]
        public Task AddResource(string parentId,[FromBody]AppResourceDto resource)
        {
            return this._resourceService.AddResourceAsync(resource, parentId);
        }

        [ResourceTag("DisableResource")]
        [HttpDelete("{id}")]
        public Task DisableResource(string id)
        {
            return this._resourceService.DisableResourceAsync(id);
        }

        [ResourceTag("EditResource")]
        [HttpPut("{id}")]
        public Task EditResource(string id,[FromBody] AppResourceDto resource)
        {
            return this._resourceService.EditAsync(id, resource);
        }

        [HttpGet("{id}")]
        [ResourceTag]
        public Task<AppResourceDto> GetResourceInfo(string id)
        {
            return this._resourceService.GetOneAsync(id);
        }

        [HttpGet("app/{appId}")]
        [ResourceTag]
        public Task<List<AppResourceDto>> GetAppResource(long appId)
        {
            return this._resourceService.GetAppResourceAsync(appId);
        }

        [HttpGet("tree/{parentResourceId}")]
        [ResourceTag]
        public Task<List<AppResourceDto>> GetResourceTree(string parentResourceId)
        {
            return this._resourceService.GetResourceTreeAsync(parentResourceId);
        }

        [HttpGet("{userId}/Resources/{appId}")]
        [ResourceTag]
        public Task<List<AppResourceDto>> GetUserResource(long appId,long userId)
        {
            return this._resourceService.GetUserResourceAsync(appId, userId);
        }

        [HttpGet("role/{roleId}/{appId}")]
        [ResourceTag]
        public Task<List<string>> GetRoleResourceIds(long roleId,long appId)
        {
            return this._resourceService.GetRoleResourceIdsAsync(appId, roleId);
        }

        [HttpPut("role/{roleId}")]
        [ResourceTag]
        public Task ChangeRoleResources(long roleId, [FromBody]List<string> resourceIds)
        {
            return this._resourceService.ChangeRoleResourcesAsync(roleId, resourceIds);
        }
    }
}