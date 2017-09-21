using System.Collections.Generic;
using System.Threading.Tasks;
using EasyRbac.Dto.AppResource;

namespace EasyRbac.DomainService
{
    public interface IRoleResourceDomainService
    {
        Task<AppResourceDto> GetAppResource(long appId, long roleId);

        Task<List<string>> GetRoleResourceIds(long appid, params long[] roleId);

        Task ChangeRoleResource(long roleId,List<string> newResourceIds);
    }
}