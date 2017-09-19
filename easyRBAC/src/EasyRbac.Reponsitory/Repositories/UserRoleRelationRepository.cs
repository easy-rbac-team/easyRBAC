using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using EasyRbac.Domain.Relations;
using Microsoft.Extensions.Logging;
using SQLinq;
using SQLinq.Dialect;

namespace EasyRbac.Reponsitory.BaseRepository
{
    public class UserRoleRelationRepository : BaseRepository<UserRoleRelation>, IUserRoleRelationRepository
    {
        public UserRoleRelationRepository(IDbConnection connection, ISqlDialect sqlDialect, ILoggerFactory loggerFactory) : base(connection, sqlDialect, loggerFactory)
        {
        }

        public Task<List<long>> GetUserIdsAsync(long roleId)
        {
            var sql = new SQLinq<UserRoleRelation>(new MySqlDialect()).Where(x => x.RoleId == roleId).Select(x => x.UserId).ToSQL();
            return base.Connection.QueryAsync<long>(sql.ToQuery(), sql.Parameters).ContinueWith(x=>x.Result.ToList());
        }

        public Task DeleteRelAsync(long roleId, params long[] ids)
        {
            var sql = "DELETE FROM `role_user_rel` WHERE roleId= @roleId and userId in @ids";
           return  base.Connection.ExecuteAsync(sql, new {roleId, ids});
        }
    }
}
