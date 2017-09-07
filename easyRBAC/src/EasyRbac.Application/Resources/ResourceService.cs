using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EasyRbac.Dto.AppResource;

namespace EasyRbac.Application.Resources
{
    public class ResourceService : IAppResourceService
    {
        public Task<List<AppAndResouceDto>> GetUserManagedResouceAsync(long userId)
        {
            throw new NotImplementedException();
        }

        public Task<List<AppResourceDto>> GetUserResouceAsync(long appId, long userId)
        {
            throw new NotImplementedException();
        }

        public Task<List<AppResourceDto>> GetResouceTreeAsync(string parentResouceId)
        {
            throw new NotImplementedException();
        }

        public Task<List<AppResourceDto>> GetAppResouceAsync(long appId)
        {
            throw new NotImplementedException();
        }

        public Task<AppResourceDto> GetOneAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task EditAsync(string id, AppResourceDto resouce)
        {
            throw new NotImplementedException();
        }

        public Task AddResouceAsync(AppResourceDto resouce)
        {
            throw new NotImplementedException();
        }

        public Task DisableResouceAsync(string id)
        {
            throw new NotImplementedException();
        }
    }
}
