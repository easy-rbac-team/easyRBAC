using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyRbac.Application.Application;
using EasyRbac.Dto;
using EasyRbac.Dto.Application;
using EasyRbac.Web.WebExtentions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EasyRbac.Web.Controllers
{
    [Route("[controller]")]
    public class ApplicationsController : Controller
    {
        private IApplicationService _applicationService;

        public ApplicationsController(IApplicationService applicationService)
        {
            this._applicationService = applicationService;
        }

        [ResourceTag("SearchApp",CheckPermission =false)]
        //[AllowAnonymous]
        [HttpGet]
        public Task<PagingList<ApplicationInfoDto>> Get(string appName, int pageIndex, int pageSize)
        {
            return this._applicationService.SearchAppAsync(appName, pageIndex, pageSize);
        }
    }
}
