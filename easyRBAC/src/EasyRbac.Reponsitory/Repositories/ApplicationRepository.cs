using Dapper;
using EasyRbac.Domain.Entity;
using EasyRbac.Domain.Relations;
using EasyRbac.Reponsitory.BaseRepository;
using Microsoft.Extensions.Logging;
using MyUtility.CollectionExtentions;
using MyUtility.Commons.IdGenerate;
using SQLinq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace EasyRbac.Reponsitory
{
    public class ApplicationRepository : BaseRepository<ApplicationEntity>, IApplicationRepository
    {
        private IRepository<ApplicationCallbackConfig> _applicationCallbackConfigRepository;
        private readonly IIdGenerator _idGenerator;
        public ApplicationRepository(IDbConnection connection, ISqlDialect sqlDialect, ILoggerFactory loggerFactory, IRepository<ApplicationCallbackConfig> applicationCallbackConfigRepository,IIdGenerator idGenerator)
            : base(connection, sqlDialect, loggerFactory)
        {
            this._idGenerator = idGenerator;
            this._applicationCallbackConfigRepository = applicationCallbackConfigRepository;
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



        public async Task<ApplicationEntity> GetAppInfoEntityAsync(Expression<Func<ApplicationEntity,bool>> condition)
        {
            var app = await base.QueryFirstAsync(condition);
            var sqlGenerator = new SQLinq<UserEntity>(base.SqlDialect);
            sqlGenerator.Where(x => x.Id == app.AppUserId);
            var sqlResult = sqlGenerator.ToSQL();
            var sql = sqlResult.ToQuery();
            var userEntity = await this.Connection.QueryFirstAsync<UserEntity>(sql, sqlResult.Parameters);

            await this.FillCallbackConfigs(app);
            app.Account = userEntity;
            return app;
        }

        private async Task FillCallbackConfigs(ApplicationEntity app)
        {
            var lst = await this.GetApplicationCallbackConfigsAsync(app.Id);

            app.CallbackConfigs = lst;
        }

        private async Task<List<ApplicationCallbackConfig>> GetApplicationCallbackConfigsAsync(long id)
        {
            var appCallbackConfigSql = new SQLinq<ApplicationCallbackConfig>(base.SqlDialect);
            appCallbackConfigSql.Where(x => x.AppId == id);
            var sqlResult = appCallbackConfigSql.ToSQL();
            var sql = sqlResult.ToQuery();
            var appCallbackConfigs = await this.Connection.QueryAsync<ApplicationCallbackConfig>(sql, sqlResult.Parameters);
            return appCallbackConfigs.ToList();
        }

        public async Task UpdateApplicationConfigInfo(ApplicationEntity entity)
        {
            using (var transaction = new TransactionScope(TransactionScopeOption.Required, TransactionScopeAsyncFlowOption.Enabled))
            {
                await base.UpdateAsync(() => new ApplicationEntity()
                {
                    Enable = entity.Enable,
                    AppCode = entity.AppCode,
                    AppName = entity.AppName,
                    Descript = entity.Descript
                }, x => x.Id == entity.Id);
                var configs = await this.GetApplicationCallbackConfigsAsync(entity.Id);
                var (newItems,removeItems) = configs.CalcluteChange(entity.CallbackConfigs,(a,b)=>a.Id==b.Id);

                foreach(var config in removeItems)
                {
                    await this._applicationCallbackConfigRepository.DeleteAsync(x => x.Id == config.Id);
                }

                foreach (var config in entity.CallbackConfigs)
                {
                    if (newItems.Contains(config))
                    {
                        await this._applicationCallbackConfigRepository.InsertAsync(config);
                    }
                    else
                    {
                        await this._applicationCallbackConfigRepository.UpdateAsync(() => new ApplicationCallbackConfig
                        {
                            Enviroment = config.Enviroment,
                            CallbackType = config.CallbackType,
                            CallbackUrl = config.CallbackUrl,
                            Remark = config.Remark
                        }, x => x.Id == config.Id);
                    }
                }
            }
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
                var role = obj.UserRole.First();
                var rel = new UserRoleRelation()
                {
                    Id = this._idGenerator.NewId(),
                    RoleId = role.Id,
                    UserId = obj.Account.Id
                };

                var relInsert = rel.ToSQLinqInsert(dialect: base.SqlDialect);
                var relSql = relInsert.ToSQL();
                await this.Connection.ExecuteAsync(relSql.ToQuery(), relSql.Parameters);
                obj.CallbackConfigs.ForEach(async x =>
                {
                    sql = x.ToSQLinqInsert(dialect: base.SqlDialect).ToSQL();
                    await this.Connection.ExecuteAsync(sql.ToQuery(), sql.Parameters);
                });
                transaction.Complete();
            }
        }

    }
}
