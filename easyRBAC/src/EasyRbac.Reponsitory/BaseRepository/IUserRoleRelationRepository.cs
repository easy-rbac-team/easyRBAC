using System.Collections.Generic;
using System.Threading.Tasks;
using EasyRbac.Domain.Relations;

namespace EasyRbac.Reponsitory.BaseRepository
{
    public interface IUserRoleRelationRepository : IRepository<UserRoleRelation>
    {
        Task<List<long>> GetUserIdsAsync(long roleId);

        Task DeleteRelAsync(long roleId, params long[] ids);
    }
}