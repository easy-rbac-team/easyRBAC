using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EasyRbac.DomainService;
using EasyRbac.Dto.AppResource;
using EasyRbac.Dto.Exceptions;
using EasyRbac.Dto.UserScope;

namespace EasyRbac.Application.UserManageScope
{
    public class ManagerScopeService:IManagerScopeService
    {
        private IUserManagerScopeDomainService _userManagerScopeDomainService;
        private IUserResourceDomainService _userResourceDomainService;

        public ManagerScopeService(IUserManagerScopeDomainService userManagerScopeDomainService, IUserResourceDomainService userResourceDomainService)
        {
            this._userManagerScopeDomainService = userManagerScopeDomainService;
            this._userResourceDomainService = userResourceDomainService;
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

        public async Task ChangeUserResource(long operatorId, long userId, long appId, List<string> ids)
        {
            if (!await this._userManagerScopeDomainService.CheckCanManager(operatorId, ids))
            {
                throw new EasyRbacException("权限校验不通过");
            }
            await this._userResourceDomainService.ChangeUserResource(userId, appId, ids);
        }
    }
}
