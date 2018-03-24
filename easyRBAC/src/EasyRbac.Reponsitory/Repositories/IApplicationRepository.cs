using System.Threading.Tasks;
using EasyRbac.Domain.Entity;
using EasyRbac.Reponsitory.BaseRepository;

namespace EasyRbac.Reponsitory
{
    public interface IApplicationRepository : IRepository<ApplicationEntity>
    {
        Task ChangeAppSecuretAsync(ApplicationEntity entity);
        Task<ApplicationEntity> GetAppInfoEntityAsync(long id);
    }
}