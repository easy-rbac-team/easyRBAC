using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using EasyRbac.Dto;
using Microsoft.Extensions.Logging;
using SQLinq;
using SQLinq.Dialect;

namespace EasyRbac.Reponsitory.BaseRepository
{
    public class BaseRepository<T> : IRepository<T>
    {
        protected IDbConnection Connection;
        protected ISqlDialect SqlDialect;
        protected ILogger Logger;

        public BaseRepository(IDbConnection connection, ISqlDialect sqlDialect, ILoggerFactory loggerFactory)
        {
            this.Connection = connection;
            this.SqlDialect = sqlDialect;
            this.Logger = loggerFactory.CreateLogger<BaseRepository<T>>();
        }

        public Task InsertAsync(T obj)
        {
            var sql = obj.ToSQLinqInsert(dialect: this.SqlDialect).ToSQL();
            this.Logger.LogDebug($"SQL:{sql.ToQuery()}{Environment.NewLine}Params:{sql.Parameters}");
            return this.Connection.ExecuteAsync(sql.ToQuery(), sql.Parameters);
        }

        public Task DeleteAsync(Expression<Func<T, bool>> expression)
        {
            ISQLinqResult sql = new SQLinq<T>(this.SqlDialect).Where(expression).ToSQL();
            var selectSQL = (SQLinqSelectResult)sql;
            var sqlCmd = $"DELETE FROM {selectSQL.Table} WHERE {selectSQL.Where}";
            this.Logger.LogDebug($"SQL:{sqlCmd}{Environment.NewLine}Params:{sql.Parameters}");
            return this.Connection.ExecuteAsync(sqlCmd, sql.Parameters);
        }

        public Task<IEnumerable<T>> QueryAsync(Expression<Func<T,bool>> condition)
        {
            var sql = new SQLinq<T>(this.SqlDialect).Where(condition).ToSQL();
            this.Logger.LogDebug($"SQL:{sql.ToQuery()}{Environment.NewLine}Params:{sql.Parameters}");
            return this.Connection.QueryAsync<T>(sql.ToQuery(), sql.Parameters);
        }

        public async Task<PagingList<T>> QueryByPagingAsync(Expression<Func<T, bool>> condition, Expression<Func<T, object>> orderBy, int pageIndex, int pageSize)
        {
            var sql = new SQLinq<T>(this.SqlDialect).Where(condition).Count().ToSQL();
            var allQuery = sql.ToQuery();
            this.Logger.LogDebug($"SQL:{sql.ToQuery()}{Environment.NewLine}Params:{sql.Parameters}");
            var totalCount = await this.Connection.ExecuteScalarAsync<int>(allQuery, sql.Parameters);


            var pageSql = new SQLinq<T>(this.SqlDialect).Where(condition).OrderBy(orderBy).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToSQL();
            this.Logger.LogDebug($"SQL:{pageSql.ToQuery()}{Environment.NewLine}Params:{pageSql.Parameters}");
            var items = await this.Connection.QueryAsync<T>(pageSql.ToQuery(), pageSql.Parameters);
            return new PagingList<T>(totalCount,pageIndex,pageSize,items);
        }

        public async Task UpdateAsync(Expression<Func<T>> update,Expression<Func<T,bool>> condition)
        {
            var updateSql = new SQLinqUpdate<T>(this.SqlDialect);
            updateSql.Where(condition);
            updateSql.UpdateSet(update);
            var queryRst = updateSql.ToSQL();

            this.Logger.LogDebug($"SQL:{queryRst.ToQuery()}{Environment.NewLine}Params:{queryRst.Parameters}");
            await this.Connection.ExecuteAsync(queryRst.ToQuery(), queryRst.Parameters);

        }

        public Task<T> QueryFirstAsync(Expression<Func<T, bool>> condition)
        {
            var sql = new SQLinq<T>(this.SqlDialect).Where(condition).ToSQL();
            this.Logger.LogDebug($"SQL:{sql.ToQuery()}{Environment.NewLine}Params:{sql.Parameters}");

            return this.Connection.QueryFirstOrDefaultAsync<T>(sql.ToQuery(),sql.Parameters);
        }

        public Task<TOut> QueryAndSelectFirstAsync<TOut>(Expression<Func<T, bool>> condition, Expression<Func<T, object>> selector)
        {
            var sql = new SQLinq<T>(this.SqlDialect).Where(condition).Select(selector).ToSQL();
            this.Logger.LogDebug($"SQL:{sql.ToQuery()}{Environment.NewLine}Params:{sql.Parameters}");
            return this.Connection.QueryFirstOrDefaultAsync<TOut>(sql.ToQuery(), sql.Parameters);
        }

        public Task<IEnumerable<TOut>> QueryAndSelectAsync<TOut>(Expression<Func<T, bool>> condition, Expression<Func<T, object>> selector)
        {
            var sql = new SQLinq<T>(this.SqlDialect).Where(condition).Select(selector).ToSQL();
            this.Logger.LogDebug($"SQL:{sql.ToQuery()}{Environment.NewLine}Params:{sql.Parameters}");
            return this.Connection.QueryAsync<TOut>(sql.ToQuery(), sql.Parameters);
        }

        public IDbConnection DbConnection => this.Connection;
    }
}
