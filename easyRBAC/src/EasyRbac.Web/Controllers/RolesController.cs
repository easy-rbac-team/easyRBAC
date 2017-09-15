using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyRbac.Application.Role;
using EasyRbac.Dto;
using EasyRbac.Dto.Role;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EasyRbac.Web.Controllers
{
    
    [Route("[controller]")]
    public class RolesController : Controller
    {
        private IRoleAppService _roleService;

        public RolesController(IRoleAppService roleService)
        {
            this._roleService = roleService;
        }

        [HttpGet]
        public Task<PagingList<RoleDto>> Search(string roleName, int pageIndex, int pageSize)
        {
            return this._roleService.SearchByPagingAsync(roleName, pageIndex, pageSize);
        }
    }
}