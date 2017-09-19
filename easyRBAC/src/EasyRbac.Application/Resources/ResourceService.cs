using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AutoMapper;
using EasyRbac.Domain.Entity;
using EasyRbac.Domain.Relations;
using EasyRbac.DomainService.Extentions;
using EasyRbac.Dto.AppResource;
using EasyRbac.Reponsitory.BaseRepository;
using EasyRbac.Reponsitory.Helper;
using EasyRbac.Utils;

namespace EasyRbac.Application.Resources
{
    public class ResourceService : IAppResourceService
    {
        private IKeyedIdGenerate _keyedIdGenerate;
        private IRepository<AppResourceRelation> _appResourceRel;
        private readonly IRepository<AppResourceEntity> _resourceRepository;
        private IIdGenerator _idGenerator;
        private IMapper _mapper;
        private INumberConvert _numberConvert;

        public ResourceService(IRepository<AppResourceRelation> appResourceRel, IIdGenerator idGenerator, IRepository<AppResourceEntity> resourceRepository, IKeyedIdGenerate keyedIdGenerate, IMapper mapper, INumberConvert numberConvert)
        {
            this._appResourceRel = appResourceRel;
            this._idGenerator = idGenerator;
            this._resourceRepository = resourceRepository;
            this._keyedIdGenerate = keyedIdGenerate;
            this._mapper = mapper;
            this._numberConvert = numberConvert;
        }

        public Task<List<AppAndResourceDto>> GetUserManagedResourceAsync(long userId)
        {
            throw new NotImplementedException();
        }

        public Task<List<AppResourceDto>> GetUserResourceAsync(long appId, long userId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<AppResourceDto>> GetResourceTreeAsync(string parentResourceId)
        {
            List<AppResourceEntity> t = await this._resourceRepository.QueryAsync(x => (x.ResourceName.StartsWith(parentResourceId) || x.ResourceCode.StartsWith(parentResourceId)) && x.Enable)
                .ContinueWith(x => x.Result.ToList());
            return this._mapper.Map<List<AppResourceDto>>(t);
        }

        public async Task<AppResourceDto> GetAppResourceAsync(long appId)
        {
            List<AppResourceEntity> t = await this._resourceRepository.QueryAsync(x => x.ApplicationId == appId && x.Enable).ContinueWith(x => x.Result.ToList());
            var lst = this._mapper.Map<List<AppResourceDto>>(t);
            var root = lst.GenerateTree();
            return root;
        }

        public async Task<AppResourceDto> GetOneAsync(string id)
        {
            var result = await this._resourceRepository.QueryFirstAsync(x => x.Id == id);
            return this._mapper.Map<AppResourceDto>(result);
        }

        public Task EditAsync(string id, AppResourceDto Resource)
        {
            return this._resourceRepository.UpdateAsync(
                () => new AppResourceEntity
                {
                    Id = Resource.Id,
                    ApplicationId = Resource.ApplicationId,
                    Describe = Resource.Describe,
                    IconUrl = Resource.IconUrl,
                    Parameters = Resource.Parameters,
                    ResourceCode = Resource.ResourceCode,
                    ResourceName = Resource.ResourceName,
                    ResourceType = Resource.ResourceType,
                    Url = Resource.Url
                },
                x => x.Id == id);
        }

        public Task AddResourceAsync(AppResourceDto Resource, string parentId)
        {
            var newId = this._keyedIdGenerate.NewId(parentId);
            var new62Id = this._numberConvert.ToString(newId).PadLeft(2, '0');
            var appResourceEntity = this._mapper.Map<AppResourceEntity>(Resource);
            appResourceEntity.Id = Convert.ToInt32(parentId) == 0 ? new62Id : (parentId + new62Id);
            return this._resourceRepository.InsertAsync(appResourceEntity);
        }

        public Task DisableResourceAsync(string id)
        {
            return this._resourceRepository.UpdateAsync(() => new AppResourceEntity()
            {
                Enable = false
            }, x => x.Id == id);
        }
    }
}
