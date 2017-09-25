using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using EasyRbac.Domain.Entity;
using EasyRbac.Dto.UserScope;
using EasyRbac.Reponsitory.BaseRepository;
using EasyRbac.Utils;

namespace EasyRbac.DomainService
{
    public class UserManagerScopeDomanService : IUserManagerScopeDomainService
    {
        private IRepository<UserManageResourceScope> _userManageResourceScope;
        private IIdGenerator _idGenerator;

        /// <summary>Initializes a new instance of the <see cref="T:System.Object"></see> class.</summary>
        public UserManagerScopeDomanService(IRepository<UserManageResourceScope> userManageResourceScope, IIdGenerator idGenerator)
        {
            this._userManageResourceScope = userManageResourceScope;
            this._idGenerator = idGenerator;
        }

        public async Task<List<string>> GetScopeIdsAsync(long userId, long appId)
        {
            var rels = await this._userManageResourceScope.QueryAsync(x => x.UserId == userId && x.AppId == appId);
            return rels.Select(x=>x.ResourceId).ToList();
        }

        public async Task ChangeScopeAsync(long userId, long appId, List<string> resources)
        {
            var rels = await this._userManageResourceScope.QueryAsync(x => x.UserId == userId && x.AppId == appId).ContinueWith(x=>x.Result.Select(v=>v.ResourceId));
            var(addIds,subIds) = rels.CalcluteChange(rels);
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
    }
}
