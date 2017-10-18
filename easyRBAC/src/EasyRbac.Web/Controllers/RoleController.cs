using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyRbac.Application.Role;
using EasyRbac.Dto;
using EasyRbac.Dto.Role;
using EasyRbac.Web.WebExtentions;
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

        [HttpPost]
        [ResourceTag]
        public Task AddRole([FromBody]RoleDto role)
        {
            return this._roleService.AddRoleAsync(role);
        }

        [HttpDelete("{roleId}")]
        [ResourceTag]
        public Task DisableRole(long roleId)
        {
            return this._roleService.DisableRoleAsync(roleId);
        }

        [HttpPut("{roleId}")]
        [ResourceTag]
        public Task EditRoleInfo(long roleId,[FromBody] RoleDto role)
        {
            return this._roleService.EditRoleAsync(roleId, role);
        }

        [HttpGet("{roleId}")]
        [ResourceTag]
        public Task<RoleDto> GetRoleInfo(long roleId)
        {
            return this._roleService.GetRoleInfoAsync(roleId);
        }

        [HttpPut("{roleId}/user")]
        [ResourceTag]
        public Task ChangeMember(long roleId, [FromBody]List<long> memberList)
        {
            return this._roleService.ChangeMember(roleId, memberList);
        }

        [HttpGet("{roleId}/user")]
        [ResourceTag]
        public Task<List<long>> GetUserIdsInRole(long roleId)
        {
            return this._roleService.GetUserIdsInRole(roleId);
        }

        [HttpPut("{roleId}/resource")]
        [ResourceTag]
        public Task ChangeResouces(long roleId, [FromBody]List<long> resouceList)
        {
            return this._roleService.ChangeResouces(roleId, resouceList);
        }
    }
}