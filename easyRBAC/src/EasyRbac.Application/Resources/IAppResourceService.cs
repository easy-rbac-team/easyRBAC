using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EasyRbac.Dto.AppResource;

namespace EasyRbac.Application.Resources
{
    public interface IAppResourceService
    {
        Task<List<AppAndResouceDto>> GetUserManagedResouceAsync(long userId);
        Task<List<AppResourceDto>> GetUserResouceAsync(long appId, long userId);
        Task<List<AppResourceDto>> GetResouceTreeAsync(string parentResouceId);
        Task<List<AppResourceDto>> GetAppResouceAsync(long appId);
        Task<AppResourceDto> GetOneAsync(string id);
        Task EditAsync(string id, AppResourceDto resouce);
        Task AddResouceAsync(AppResourceDto resouce);
        Task DisableResouceAsync(string id);
    }
}
