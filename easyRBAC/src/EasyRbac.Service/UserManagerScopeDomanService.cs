using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using AutoMapper;
using EasyRbac.Domain.Entity;
using EasyRbac.Dto.AppResource;
using EasyRbac.Dto.UserScope;
using EasyRbac.Reponsitory;
using EasyRbac.Reponsitory.BaseRepository;
using EasyRbac.Utils;

namespace EasyRbac.DomainService
{
    public class UserManagerScopeDomanService : IUserManagerScopeDomainService
    {
        private IRepository<UserManageResourceScope> _userManageResourceScope;
        private IApplicationRepository _applicationRepository;
        private IRepository<AppResourceEntity> _appResourceRepository;
        private IIdGenerator _idGenerator;
        private readonly IMapper _mapper;

        /// <summary>Initializes a new instance of the <see cref="T:System.Object"></see> class.</summary>
        public UserManagerScopeDomanService(IRepository<UserManageResourceScope> userManageResourceScope, IIdGenerator idGenerator, IApplicationRepository applicationRepository, IRepository<AppResourceEntity> appResourceRepository, IMapper mapper)
        {
            this._userManageResourceScope = userManageResourceScope;
            this._idGenerator = idGenerator;
            this._applicationRepository = applicationRepository;
            this._appResourceRepository = appResourceRepository;
            this._mapper = mapper;
        }

        public async Task<List<string>> GetScopeIdsAsync(long userId, long appId)
        {
            var rels = await this._userManageResourceScope.QueryAsync(x => x.UserId == userId && x.AppId == appId);
            return rels.Select(x => x.ResourceId).ToList();
        }

        public async Task ChangeScopeAsync(long userId, long appId, List<string> resources)
        {
            var rels = await this._userManageResourceScope.QueryAsync(x => x.UserId == userId && x.AppId == appId).ContinueWith(x => x.Result.Select(v => v.ResourceId));
            var (addIds, subIds) = rels.CalcluteChange(resources);
            using (var scope = new TransactionScope(
                TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted },
                TransactionScopeAsyncFlowOption.Enabled))
            {
                await this._userManageResourceScope.DeleteAsync(x => subIds.Contains(x.ResourceId) && x.UserId == userId);
                foreach (string addId in addIds)
                {
                    await this._userManageResourceScope.InsertAsync(
                        new UserManageResourceScope()
                        {
                            AppId = appId,
                            Id = this._idGenerator.NewId(),
                            ResourceId = addId,
                            UserId = userId
                        });
                }
                scope.Complete();
            }
        }

        public async Task<bool> CheckCanManager(long userId, string resourceId)
        {
            var result = await this._userManageResourceScope.QueryFirstAsync(x => x.UserId == userId && x.ResourceId == resourceId);
            return result != null;
        }

        public async Task<List<AppAndResourceDto>> GetUserManagedResourceAsync(long userId)
        {
            var rels = await this._userManageResourceScope.QueryAsync(x => x.UserId == userId);

            var appIds = rels.Select(x => x.AppId).Distinct();
            var apps = await this._applicationRepository.QueryAsync(x => appIds.Contains(x.Id));
            var resourceIds = rels.Select(x => x.ResourceId);
            var resources = await this._appResourceRepository.QueryAsync(x => resourceIds.Contains(x.Id));
            var joinResult = from resource in resources
                             join app in apps
                                 on resource.ApplicationId equals app.Id
                             select new
                             {
                                 app,
                                 resource
                             };
            var result = joinResult.GroupBy(x => x.app, x => x.resource).Select(
                x => new AppAndResourceDto()
                {
                    AppId = x.Key.Id,
                    AppName = x.Key.AppName,
                    AppCode = x.Key.AppCode,
                    AppResouces = this._mapper.Map<List<AppResourceDto>>(x.ToList()).ToToMultiTree(t=>t.Id,t=>t.Id.Substring(0,t.Id.Length-2))
                });
            return result.ToList();
        }
    }
}
