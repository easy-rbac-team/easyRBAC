using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EasyRbac.Dto;
using EasyRbac.Dto.Role;

namespace EasyRbac.Application.Role
{
    public class RoleAppService : IRoleAppService
    {
        public Task<PagingList<RoleDto>> SearchByPagingAsync(string roleName, int pageIndex, int pageSize)
        {
            throw new NotImplementedException();
        }

        public Task AddRoleAsync(RoleDto role)
        {
            throw new NotImplementedException();
        }

        public Task DisableRoleAsync(long roleId)
        {
            throw new NotImplementedException();
        }

        public Task EditRoleAsync(long roleId, RoleDto role)
        {
            throw new NotImplementedException();
        }

        public Task<RoleDto> GetRoleInfoAsync(long roleId)
        {
            throw new NotImplementedException();
        }

        public Task ChangeMember(long roleId, List<long> memberList)
        {
            throw new NotImplementedException();
        }

        public Task ChangeResouces(long roleId, List<long> resouceList)
        {
            throw new NotImplementedException();
        }
    }
}
