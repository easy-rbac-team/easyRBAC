using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyRbac.Domain.Entity;
using EasyRbac.Domain.Relations;
using EasyRbac.Reponsitory.BaseRepository;

namespace EasyRbac.DomainService
{
    public class UserRoleDomainService : IUserRoleDomainService
    {
        private IRepository<RoleEntity> roleRepository;
        private IRepository<UserRoleRelation> userRoleRelation;

        public UserRoleDomainService(IRepository<RoleEntity> roleRepository, IRepository<UserRoleRelation> userRoleRelation)
        {
            this.roleRepository = roleRepository;
            this.userRoleRelation = userRoleRelation;
        }
        

        public async Task<List<RoleEntity>> GetRolesAsync(long userId)
        {
            var rels = await this.userRoleRelation.QueryAsync(x => x.UserId == userId);
            var roleIds = rels.Select(x => x.RoleId);
            var result = await this.roleRepository.QueryAsync(x => roleIds.Contains(x.Id));
            return result.ToList();
        }
    }
}
