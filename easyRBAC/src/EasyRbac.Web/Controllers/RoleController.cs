using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyRbac.Application.Role;
using EasyRbac.Dto;
using EasyRbac.Dto.Role;
using Microsoft.AspNetCore.Mvc;

namespace EasyRbac.Web.Controllers
{
    [Route("[controller]")]
    public class RoleController : Controller
    {
        private IRoleAppService _roleService;

        public RoleController(IRoleAppService roleService)
        {
            this._roleService = roleService;
        }

        [HttpGet]
        public Task<PagingList<RoleDto>> Search(string roleName,int pageIndex,int pageSize)
        {
            return this._roleService.SearchByPagingAsync(roleName, pageIndex, pageSize);
        }

        [HttpPost]
        public Task AddRole([FromBody]RoleDto role)
        {
            return this._roleService.AddRoleAsync(role);
        }

        [HttpDelete("{roleId}")]
        public Task DisableRole(long roleId)
        {
            return this._roleService.DisableRoleAsync(roleId);
        }

        [HttpGet("{roleId}")]
        public Task EditRoleInfo(long roleId, RoleDto role)
        {
            return this._roleService.EditRoleAsync(roleId, role);
        }

        [HttpGet("{roleId}")]
        public Task<RoleDto> GetRoleInfo(long roleId)
        {
            return this._roleService.GetRoleInfoAsync(roleId);
        }

        [HttpPut("{roleId}/user")]
        public Task ChangeMember(long roleId, [FromBody]List<long> memberList)
        {
            return this._roleService.ChangeMember(roleId, memberList);
        }

        [HttpPut("{roleId}/user")]
        public Task ChangeResouces(long roleId, [FromBody]List<long> resouceList)
        {
            return this._roleService.ChangeResouces(roleId, resouceList);
        }
    }
}