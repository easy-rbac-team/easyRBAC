using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EasyRbac.Domain.Entity;
using EasyRbac.Domain.Relations;
using EasyRbac.Dto.AppResource;
using EasyRbac.Reponsitory.BaseRepository;
using EasyRbac.Reponsitory.Helper;
using EasyRbac.Utils;

namespace EasyRbac.Application.Resources
{
    public class ResourceService : IAppResourceService
    {
        private IKeyedIdGenerate _keyedIdGenerate;
        private IRepository<AppResourceRelation> _appResouceRel;
        private IRepository<AppResourceEntity> _resouceRepository;
        private IIdGenerator _idGenerator;

        public ResourceService(IRepository<AppResourceRelation> appResouceRel, IIdGenerator idGenerator, IRepository<AppResourceEntity> resouceRepository, IKeyedIdGenerate keyedIdGenerate)
        {
            this._appResouceRel = appResouceRel;
            this._idGenerator = idGenerator;
            this._resouceRepository = resouceRepository;
            this._keyedIdGenerate = keyedIdGenerate;
        }

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
