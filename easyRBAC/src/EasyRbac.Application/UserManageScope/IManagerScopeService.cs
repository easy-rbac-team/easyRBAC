using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EasyRbac.Dto.UserScope;

namespace EasyRbac.Application.UserManageScope
{
    public interface IManagerScopeService
    {
        Task<List<string>> GetScopeIdsAsync(long userId, long appId);

        Task ChangeScopeAsync(long userId, long appId, List<string> resources);

    }
}
