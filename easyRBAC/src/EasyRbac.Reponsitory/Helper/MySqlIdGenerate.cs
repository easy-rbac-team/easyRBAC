using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Dapper;

namespace EasyRbac.Reponsitory.Helper
{
    public class MySqlIdGenerate : IKeyedIdGenerate
    {
        private IDbConnection _connection;
        public MySqlIdGenerate(IDbConnection connection)
        {
            this._connection = connection;
        }

        public int NewId(string key)
        {
            this._connection.Open();
            IDbTransaction tran = this._connection.BeginTransaction(IsolationLevel.ReadCommitted);

            try
            {
                var command = @"INSERT INTO id_generate (parentId, subId)
                                VALUES(@key, 1)
                                ON DUPLICATE KEY UPDATE
                                subId = subid + 1";
                this._connection.Execute(command,new{key});
                command = @"SELECT subId FROM id_generate WHERE parentId = @key";
                var id = this._connection.ExecuteScalar<int>(command, new { key });
                tran.Commit();
                return id;
            }
            catch 
            {
                tran.Rollback();
                throw;
            }
            finally
            {
                tran.Dispose();
            }
        }
    }
}
