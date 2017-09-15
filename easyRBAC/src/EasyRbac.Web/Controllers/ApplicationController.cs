using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyRbac.Application.Application;
using EasyRbac.Dto;
using EasyRbac.Dto.Application;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EasyRbac.Web.Controllers
{
    [Route("Application")]
    public class ApplicationController : Controller
    {
        private IApplicationService _applicationService;

        public ApplicationController(IApplicationService applicationService)
        {
            this._applicationService = applicationService;
        }
        
        // GET: api/Application/5
        [HttpGet("{id}", Name = "Get")]
        public Task<ApplicationInfoDto> Get(long id)
        {
            return this._applicationService.GetOneAsync(id);
        }
        
        // POST: api/Application
        [HttpPost]
        public Task<ApplicationInfoDto> Post([FromBody]ApplicationInfoDto app)
        {
            return this._applicationService.AddAppAsync(app);
        }
        
        // PUT: api/Application/5
        [HttpPut("{id}")]
        public Task Put(long id, [FromBody]ApplicationInfoDto value)
        {
            return this._applicationService.EditAsync(id, value);
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public Task Delete(long id)
        {
            return this._applicationService.DisableApp(id);
        }

        [HttpGet("appSecret/{appId}")]
        public Task<string> GetAppSecret(long appId)
        {
            return this._applicationService.GetAppScretAsync(appId);
        }

        [HttpPut("appSecret/{appId}")]
        public Task ChangeAppSecret(long appId)
        {
            return this._applicationService.EditAppScretAsync(appId);
        }
    }
}
