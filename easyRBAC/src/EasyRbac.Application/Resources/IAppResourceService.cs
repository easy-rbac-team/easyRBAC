using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EasyRbac.Dto.AppResource;

namespace EasyRbac.Application.Resources
{
    public interface IAppResourceService
    {
        Task<List<AppResourceDto>> GetUserResourceAsync(long appId, long userId);
        Task<List<AppResourceDto>> GetResourceTreeAsync(string parentResourceId);
        Task<List<AppResourceDto>> GetAppResourceAsync(long appId);
        Task<AppResourceDto> GetOneAsync(string id);
        Task EditAsync(string id, AppResourceDto Resource);
        Task AddResourceAsync(AppResourceDto Resource, string parentId);
        Task DisableResourceAsync(string id);

        Task<List<string>> GetRoleResourceIdsAsync(long appId, long roleId);

        Task ChangeRoleResourcesAsync(long roleId,long appId, List<string> resourceIds);
    }
}
