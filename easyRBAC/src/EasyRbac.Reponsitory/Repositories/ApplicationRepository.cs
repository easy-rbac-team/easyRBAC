using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Dapper;
using EasyRbac.Domain.Entity;
using EasyRbac.Reponsitory.BaseRepository;
using Microsoft.Extensions.Logging;
using SQLinq;

namespace EasyRbac.Reponsitory
{
    public class ApplicationRepository : BaseRepository<ApplicationEntity>, IApplicationRepository
    {
        public ApplicationRepository(IDbConnection connection, ISqlDialect sqlDialect, ILoggerFactory loggerFactory)
            : base(connection, sqlDialect, loggerFactory)
        {
        }

        public async Task ChangeAppSecuretAsync(long id, string newSecuret)
        {
            var app = await base.QueryFirstAsync(x => x.Id == id);
            var sqlGenerator = new SQLinq<UserEntity>(base.SqlDialect);
            var updateSql = new SQLinqUpdate<UserEntity>(this.SqlDialect);
            updateSql.Where(x => x.Id == app.AppUserId);
            updateSql.UpdateSet(() => new UserEntity { Password = newSecuret });
            var sqlResult = updateSql.ToSQL();
            var sql = sqlResult.ToQuery();
            var paramaters = sqlResult.Parameters;

            await this.Connection.ExecuteAsync(sql, paramaters);
        }

        public async Task<ApplicationEntity> GetAppInfoEntityAsync(long id)
        {
            var app = await base.QueryFirstAsync(x => x.Id == id);
            var sqlGenerator = new SQLinq<UserEntity>(base.SqlDialect);
            sqlGenerator.Where(x => x.Id == app.AppUserId);
            var sqlResult = sqlGenerator.ToSQL();
            var sql = sqlResult.ToQuery();

            var userEntity = await this.Connection.QueryFirstAsync<UserEntity>(sql, sqlResult.Parameters);
            app.Account = userEntity;
            return app;
        }

        public async Task ChangeAppSecuretAsync(ApplicationEntity entity)
        {
            var sqlGenerator = new SQLinq<UserEntity>(base.SqlDialect);
            sqlGenerator.Where(x => x.Id == entity.AppUserId);
            var sqlResult = sqlGenerator.ToSQL();
            var sql = sqlResult.ToQuery();

            var updateSqlGenerator = new SQLinqUpdate<UserEntity>(base.SqlDialect);
            updateSqlGenerator.Where(x => x.Id == entity.AppUserId);
            updateSqlGenerator.UpdateSet(() => new UserEntity { Password = entity.Account.Password, Salt = entity.Account.Salt });
            var updateSqlResult = updateSqlGenerator.ToSQL();
            var updateSql = updateSqlResult.ToQuery();
            await this.Connection.ExecuteAsync(updateSql, updateSqlResult.Parameters);
        }

        public override async Task InsertAsync(ApplicationEntity obj)
        {
            using (var transaction = new TransactionScope(TransactionScopeOption.Required, TransactionScopeAsyncFlowOption.Enabled))
            {
                await base.InsertAsync(obj);
                var sql = obj.Account.ToSQLinqInsert(dialect: base.SqlDialect).ToSQL();
                await this.Connection.ExecuteAsync(sql.ToQuery(), sql.Parameters);
                transaction.Complete();
            }
        }

    }
}
