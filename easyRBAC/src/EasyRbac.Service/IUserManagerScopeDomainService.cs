using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EasyRbac.Dto.AppResource;
using EasyRbac.Dto.UserScope;

namespace EasyRbac.DomainService
{
    public interface IUserManagerScopeDomainService
    {
        Task<List<string>> GetScopeIdsAsync(long userId, long appId);

        Task ChangeScopeAsync(long userId, long appId, List<string> resources);

        Task<bool> CheckCanManager(long userId, IEnumerable<string> resourceIds);

        Task<List<AppAndResourceDto>> GetUserManagedResourceAsync(long userId);
    }
}
