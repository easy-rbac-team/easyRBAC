using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using AutoMapper;
using EasyRbac.Domain.Entity;
using EasyRbac.Domain.Relations;
using EasyRbac.DomainService.Extentions;
using EasyRbac.Dto.AppResource;
using EasyRbac.Reponsitory.BaseRepository;
using EasyRbac.Reponsitory.Helper;
using EasyRbac.Utils;

namespace EasyRbac.DomainService
{

    public class RoleResourceDomainService : IRoleResourceDomainService
    {
        private IKeyedIdGenerate _keyedIdGenerate;
        private IRepository<RoleResourceRelation> _roleResourceRel;
        private IRepository<AppResourceEntity> _appResourceRepository;
        private IMapper _mapper;
        private IIdGenerator _idGenerator;

        /// <summary>Initializes a new instance of the <see cref="T:System.Object"></see> class.</summary>
        public RoleResourceDomainService(IKeyedIdGenerate keyedIdGenerate, IRepository<RoleResourceRelation> roleResourceRel, IRepository<AppResourceEntity> appResourceRepository, IMapper mapper, IIdGenerator idGenerator)
        {
            this._keyedIdGenerate = keyedIdGenerate;
            this._roleResourceRel = roleResourceRel;
            this._appResourceRepository = appResourceRepository;
            this._mapper = mapper;
            this._idGenerator = idGenerator;
        }

        public async Task<AppResourceDto> GetAppResource(long appId, long roleId)
        {
            var rels = await this._roleResourceRel.QueryAsync(x => x.RoleId == roleId);
            var resourceIds = rels.Select(x => x.ResourceId);
            var resource = this._appResourceRepository.QueryAsync(x =>
                resourceIds.Contains(x.Id) && x.ApplicationId == appId && x.Enable);
            var lst = this._mapper.Map<List<AppResourceDto>>(resource);
            return lst.GenerateTree();
        }

        public async Task<List<string>> GetRoleResourceIds(long appid, params long[] roleId)
        {
            IEnumerable<RoleResourceRelation> rels = await this._roleResourceRel.QueryAsync(x => roleId.Contains(x.RoleId));
            var ids = rels.Select(x => x.ResourceId);
            var appResourceEntities = await this._appResourceRepository.QueryAsync(x => ids.Contains(x.Id) && x.Enable);
            return appResourceEntities.Select(x => x.Id).ToList();
        }

        public async Task ChangeRoleResource(long roleId,List<string> newResourceIds)
        {
            using (var tran = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                IEnumerable<RoleResourceRelation> rels = await this._roleResourceRel.QueryAsync(x => x.RoleId == roleId);
                var resourceIds = rels.Select(x => x.ResourceId);
                var (addResources,subResources) = resourceIds.CalcluteChange(newResourceIds);
                await this._roleResourceRel.DeleteAsync(x => subResources.Contains(x.ResourceId)&&x.RoleId == roleId);
                foreach (var resourceId in addResources)
                {
                    var rel = new RoleResourceRelation()
                    {
                        Id = this._idGenerator.NewId(),
                        ResourceId = resourceId,
                        RoleId = roleId
                    };
                    await this._roleResourceRel.InsertAsync(rel);
                }
                tran.Complete();
            }
        }
    }
}
