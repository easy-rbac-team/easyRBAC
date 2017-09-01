using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using EasyRbac.Dto;
using SQLinq;
using SQLinq.Dialect;

namespace EasyRbac.Reponsitory.BaseRepository
{
    

    public abstract class BaseRepository<T> : IBaseRepository<T>
    {
        protected IDbConnection Connection;
        protected ISqlDialect SqlDialect;

        protected BaseRepository(IDbConnection connection, ISqlDialect sqlDialect)
        {
            this.Connection = connection;
            this.SqlDialect = sqlDialect;
        }

        public Task InsertAsync(T obj)
        {
            var sql = obj.ToSQLinqInsert(dialect: this.SqlDialect).ToSQL();
            return this.Connection.ExecuteAsync(sql.ToQuery(), sql.Parameters);
        }

        public Task DeleteAsync(Expression<Func<T, bool>> expression)
        {
            ISQLinqResult sql = new SQLinq<T>().Where(expression).ToSQL();
            var selectSQL = (SQLinqSelectResult)sql;
            var sqlCmd = $"DELETE FROM {selectSQL.Table} WHERE {selectSQL.Where}";
            return this.Connection.ExecuteAsync(sqlCmd, sql.Parameters);
        }

        public Task DeleteById(object key)
        {
            var sql = new SQLinq<T>().ToSQL() as SQLinqSelectResult;
            var cmd = $"DELETE FROM {sql.Table} WHERE ID= {this.SqlDialect.ParameterPrefix}id";
            return this.Connection.ExecuteAsync(
               cmd,
               new
               {
                   id = key
               });
        }

        public Task<IEnumerable<T>> QueryAsync(Expression<Func<T,bool>> condition)
        {
            var sql = new SQLinq<T>().Where(condition).ToSQL();
            return this.Connection.QueryAsync<T>(sql.ToQuery(), sql.Parameters);
        }

        public async Task<PagingList<T>> QueryByPagingAsync(Expression<Func<T, bool>> condition, int pageIndex, int pageSize)
        {
            var sql = new SQLinq<T>().Where(condition).ToSQL();
            var allQuery = sql.ToQuery();
            var totalCount = await this.Connection.ExecuteScalarAsync<int>(allQuery, sql.Parameters);
            var pageSql = new SQLinq<T>().Where(condition).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToSQL();
            var items = await this.Connection.QueryAsync<T>(pageSql.ToQuery(), pageSql.Parameters);
            return new PagingList<T>(totalCount,pageIndex,pageSize,items);
        }

    }
}
