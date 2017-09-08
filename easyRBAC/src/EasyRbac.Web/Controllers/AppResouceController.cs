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
    public class AppResouceController : Controller
    {
        private IAppResourceService _resouceService;

        public AppResouceController(IAppResourceService resouceService)
        {
            this._resouceService = resouceService;
        }

        [HttpPost]
        public Task AddResouce(AppResourceDto resouce)
        {
            return this._resouceService.AddResouceAsync(resouce);
        }

        [HttpDelete]
        public Task DisableResouce(string id)
        {
            return this._resouceService.DisableResouceAsync(id);
        }

        [HttpPut("{id}")]
        public Task EditResouce(string id,[FromBody] AppResourceDto resouce)
        {
            return this._resouceService.EditAsync(id, resouce);
        }

        [HttpGet("{id}")]
        public Task<AppResourceDto> GetResouceInfo(string id)
        {
            return this._resouceService.GetOneAsync(id);
        }

        [HttpGet("app/{id}")]
        public Task<List<AppResourceDto>> GetAppResouce(long appId)
        {
            return this._resouceService.GetAppResouceAsync(appId);
        }

        [HttpGet("tree/{parentResouceId}")]
        public Task<List<AppResourceDto>> GetResouceTree(string parentResouceId)
        {
            return this._resouceService.GetResouceTreeAsync(parentResouceId);
        }

        [HttpGet("{userId}/resouces/{appId}")]
        public Task<List<AppResourceDto>> GetUserResouce(long appId,long userId)
        {
            return this._resouceService.GetUserResouceAsync(appId, userId);
        }

        [HttpGet("managedresouce/{userId}")]
        public Task<List<AppAndResouceDto>> GetUserManagedResouce(long userId)
        {
            return this._resouceService.GetUserManagedResouceAsync(userId);
        }
    }
}