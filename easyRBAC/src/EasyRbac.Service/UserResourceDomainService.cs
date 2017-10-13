using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using AutoMapper;
using EasyRbac.Domain.Entity;
using EasyRbac.Domain.Relations;
using EasyRbac.Dto.AppResource;
using EasyRbac.Reponsitory.BaseRepository;
using EasyRbac.Utils;
using MyUtility.CollectionExtentions;
using MyUtility.Commons.IdGenerate;

namespace EasyRbac.DomainService
{
    public class UserResourceDomainService : IUserResourceDomainService
    {
        private IUserRoleRelationRepository _userRoleRel;
        private IRepository<ApplicationEntity> _appRepository;
        private IRepository<UserResourceRelation> _userResourceRel;
        private IRepository<AppResourceEntity> _resourceRepository;
        private IRoleResourceDomainService _roleResourceDomainService;
        private IMapper _mapper;
        private IIdGenerator _idGenerator;

        /// <summary>Initializes a new instance of the <see cref="T:System.Object"></see> class.</summary>
        public UserResourceDomainService(IUserRoleRelationRepository userRoleRel, IRepository<UserResourceRelation> userResourceRel, IRepository<AppResourceEntity> resourceRepository, IRoleResourceDomainService roleResourceDomainService, IMapper mapper, IIdGenerator idGenerator, IRepository<ApplicationEntity> appRepository)
        {
            this._userRoleRel = userRoleRel;
            this._userResourceRel = userResourceRel;
            this._resourceRepository = resourceRepository;
            this._roleResourceDomainService = roleResourceDomainService;
            this._mapper = mapper;
            this._idGenerator = idGenerator;
            this._appRepository = appRepository;
        }

        public Task<List<string>> GetUserAssociationResourcesAsync(long userId, long appId)
        {
            var rels = this._userResourceRel.QueryAsync(x => x.UserId == userId && x.AppId == appId).ContinueWith(x=>x.Result.Select(r=>r.ResourceId).ToList());
            return rels;
        }

        /// <summary>
        /// 获取用户关联的角色所关联的资源
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="appId"></param>
        /// <returns></returns>
        public async Task<List<string>> GetUserAssociationRolseResourcesAsync(long userId, long appId)
        {
            IEnumerable<UserRoleRelation> userRoleRelations = await this._userRoleRel.QueryAsync(x => x.UserId == userId);
            List<string> resourceIds = await this._roleResourceDomainService.GetRoleResourceIds(appId, userRoleRelations.Select(x => x.RoleId).ToArray());
            return resourceIds;
        }

        /// <summary>
        /// 获取用户所有资源
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="appId"></param>
        /// <returns></returns>
        public async Task<List<AppResourceDto>> GetUserAllAppResourcesAsync(long userId,long appId)
        {
            var userResources = await this.GetUserAssociationResourcesAsync(userId, appId);
            var roleResources = await this.GetUserAssociationRolseResourcesAsync(userId, appId);
            userResources.AddRange(roleResources);
            var ids = userResources.Distinct();
            var results = await this._resourceRepository.QueryAsync(x => ids.Contains(x.Id) && x.Enable);
            return this._mapper.Map<List<AppResourceDto>>(results);
        }

        /// <summary>
        /// 根据APPCODE获取用户资源列表
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="appCode"></param>
        /// <returns></returns>
        public async Task<List<AppResourceDto>> GetUserAllAppResourcesAsync(long userId, string appCode)
        {
            var app = await this._appRepository.QueryFirstAsync(x => x.AppCode == appCode);
            return await this.GetUserAllAppResourcesAsync(userId, app.Id);
        }

        public async Task ChangeUserResource(long userId,long appId, List<string> resourceId)
        {
            if (!resourceId.Any())
            {
                return;
            }
            var resourceRels = await this._userResourceRel.QueryAsync(x => x.UserId == userId && x.AppId == appId);
            var ids = resourceRels.Select(x => x.ResourceId);
            var (addIds,subIds) = ids.CalcluteChange(resourceId);
            using (var tran = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                await this._userResourceRel.DeleteAsync(x => subIds.Contains(x.ResourceId) && x.UserId == userId);
                foreach (var id in addIds)
                {
                    var rel = new UserResourceRelation()
                    {
                        AppId = appId,
                        Id = this._idGenerator.NewId(),
                        ResourceId = id,
                        UserId = userId
                    };
                    await this._userResourceRel.InsertAsync(rel);
                }
                tran.Complete();
            }
        }
    }
}
