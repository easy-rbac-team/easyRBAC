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

        public override async Task InsertAsync(ApplicationEntity obj)
        {
            using(var transaction = new TransactionScope(TransactionScopeOption.Required, TransactionScopeAsyncFlowOption.Enabled))
            {
                await base.InsertAsync(obj);
                var sql =  obj.Account.ToSQLinqInsert(dialect:base.SqlDialect).ToSQL();
                await this.Connection.ExecuteAsync(sql.ToQuery(), sql.Parameters);
                transaction.Complete();
            }
        }

    }
}
