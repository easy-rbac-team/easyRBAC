using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EasyRbac.Dto.UserScope;

namespace EasyRbac.Application.UserManageScope
{
    public class ManagerScopeService:IManagerScopeService
    {
        public Task<List<string>> GetScopeIdsAsync(long userId, long appId)
        {
            throw new NotImplementedException();
        }

        public Task ChangeScopeAsync(long userId, long appId, List<UserScopeDto> resources)
        {
            throw new NotImplementedException();
        }
    }
}
