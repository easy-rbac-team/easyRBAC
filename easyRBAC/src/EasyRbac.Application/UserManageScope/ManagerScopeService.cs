using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EasyRbac.DomainService;
using EasyRbac.Dto.AppResource;
using EasyRbac.Dto.UserScope;

namespace EasyRbac.Application.UserManageScope
{
    public class ManagerScopeService:IManagerScopeService
    {
        private IUserManagerScopeDomainService _userManagerScopeDomainService;

        public ManagerScopeService(IUserManagerScopeDomainService userManagerScopeDomainService)
        {
            this._userManagerScopeDomainService = userManagerScopeDomainService;
        }

        public Task<List<string>> GetScopeIdsAsync(long userId, long appId)
        {
            return this._userManagerScopeDomainService.GetScopeIdsAsync(userId, appId);
        }

        public Task ChangeScopeAsync(long userId, long appId, List<string> resources)
        {
            return this._userManagerScopeDomainService.ChangeScopeAsync(userId, appId, resources);
        }

        public Task<List<AppAndResourceDto>> GetUserManagedResourceAsync(long userId)
        {
            return this._userManagerScopeDomainService.GetUserManagedResourceAsync(userId);
        }
    }
}
