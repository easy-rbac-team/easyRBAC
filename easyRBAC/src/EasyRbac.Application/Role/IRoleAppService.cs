using System.Collections.Generic;
using System.Threading.Tasks;
using EasyRbac.Dto;
using EasyRbac.Dto.Role;

namespace EasyRbac.Application.Role
{
    public interface IRoleAppService
    {
        Task<PagingList<RoleDto>> SearchByPagingAsync(string roleName, int pageIndex, int pageSize);

        Task AddRoleAsync(RoleDto role);

        Task DisableRoleAsync(long roleId);

        Task EditRoleAsync(long roleId, RoleDto role);

        Task<RoleDto> GetRoleInfoAsync(long roleId);

        Task ChangeMember(long roleId, List<long> memberList);
        Task ChangeResouces(long roleId, List<long> resouceList);
    }
}