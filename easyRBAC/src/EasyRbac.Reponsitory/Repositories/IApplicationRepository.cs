using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using EasyRbac.Domain.Entity;
using EasyRbac.Reponsitory.BaseRepository;

namespace EasyRbac.Reponsitory
{
    public interface IApplicationRepository : IRepository<ApplicationEntity>
    {
        Task ChangeAppSecuretAsync(ApplicationEntity entity);
        Task<ApplicationEntity> GetAppInfoEntityAsync(Expression<Func<ApplicationEntity,bool>> id);

        Task UpdateApplicationConfigInfo(ApplicationEntity entity);
    }
}