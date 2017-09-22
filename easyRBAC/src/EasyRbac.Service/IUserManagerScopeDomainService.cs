using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EasyRbac.Dto.UserScope;

namespace EasyRbac.DomainService
{
    public interface IUserManagerScopeDomainService
    {
        Task<List<string>> GetScopeIdsAsync(long userId, long appId);

        Task ChangeScopeAsync(long userId, long appId, List<UserScopeDto> resources);

        Task<bool> CheckCanManager(long userId, string resourceId);
    }
}
