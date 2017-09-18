using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using AutoMapper;
using EasyRbac.Domain.Entity;
using EasyRbac.Domain.Relations;
using EasyRbac.Dto;
using EasyRbac.Dto.Role;
using EasyRbac.Reponsitory.BaseRepository;
using EasyRbac.Utils;

namespace EasyRbac.Application.Role
{
    public class RoleAppService : IRoleAppService
    {
        private IRepository<RoleEntity> _roleRepository;
        private IUserRoleRelationRepository _userRoleRel;
        private IMapper _mapper;
        private IIdGenerator _idGenerator;

        public RoleAppService(IRepository<RoleEntity> roleRepository, IMapper mapper, IIdGenerator idGenerator, IUserRoleRelationRepository userRoleRel)
        {
            this._roleRepository = roleRepository;
            this._mapper = mapper;
            this._idGenerator = idGenerator;
            _userRoleRel = userRoleRel;
        }

        public async Task<PagingList<RoleDto>> SearchByPagingAsync(string roleName, int pageIndex, int pageSize)
        {
            PagingList<RoleEntity> roles = await this._roleRepository.QueryByPagingAsync(x => x.RoleName.StartsWith(roleName) && x.Enable == true, x => x.Id, pageIndex, pageSize);
            return this._mapper.Map<PagingList<RoleDto>>(roles);
        }

        public Task AddRoleAsync(RoleDto role)
        {
            var id = this._idGenerator.NewId();
            var roleEntity = this._mapper.Map<RoleEntity>(role);
            roleEntity.Id = id;
            return this._roleRepository.InsertAsync(roleEntity);
        }

        public Task DisableRoleAsync(long roleId)
        {
            return this._roleRepository.UpdateAsync(
                 () => new RoleEntity()
                 {
                     Enable = false
                 },
                 x => x.Id == roleId);
        }

        public Task EditRoleAsync(long roleId, RoleDto role)
        {
            return this._roleRepository.UpdateAsync(
                () => new RoleEntity()
                {
                    Id = roleId,
                    Descript = role.Descript,
                    RoleName = role.RoleName
                },
                x => x.Id == roleId);
        }

        public async Task<RoleDto> GetRoleInfoAsync(long roleId)
        {
            var result = await this._roleRepository.QueryAsync(x => x.Id == roleId && x.Enable);
            var entity = result.FirstOrDefault();
            return this._mapper.Map<RoleDto>(entity);
        }

        public async Task ChangeMember(long roleId, List<long> memberList)
        {
            using (TransactionScope scop = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var userIds = await this._userRoleRel.GetUserIdsAsync(roleId);
                var subUsers = userIds.Except(memberList).ToArray();
                var addUsers = memberList.Except(userIds).ToList();

                if (subUsers.Length > 0)
                {
                    await this._userRoleRel.DeleteRelAsync(roleId, subUsers);
                }
                if (addUsers.Count > 0)
                {
                    var addRels = addUsers.Select(x =>
                        new UserRoleRelation() { Id = this._idGenerator.NewId(), RoleId = roleId, UserId = x });
                    foreach (var rel in addRels)
                    {
                        await this._userRoleRel.InsertAsync(rel);
                    }
                }
                scop.Complete();
            }

        }

        public Task ChangeResouces(long roleId, List<long> resouceList)
        {
            throw new NotImplementedException();
        }

        public async Task<List<long>> GetUserIdsInRole(long roleId)
        {
            var rels = await this._userRoleRel.GetUserIdsAsync(roleId);
            return rels;
        }
    }
}
