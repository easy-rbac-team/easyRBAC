using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
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

       
    }
}
