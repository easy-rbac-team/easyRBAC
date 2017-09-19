using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

    public class RoleResourceDomainService
    {
        private IKeyedIdGenerate _keyedIdGenerate;
        private IRepository<RoleResourceRelation> _roleResourceRel;
        private IRepository<AppResourceEntity> _appResourceRepository;
        private IMapper _mapper;

        /// <summary>Initializes a new instance of the <see cref="T:System.Object"></see> class.</summary>
        public RoleResourceDomainService(IKeyedIdGenerate keyedIdGenerate, IRepository<RoleResourceRelation> roleResourceRel, IRepository<AppResourceEntity> appResourceRepository, IMapper mapper)
        {
            _keyedIdGenerate = keyedIdGenerate;
            _roleResourceRel = roleResourceRel;
            _appResourceRepository = appResourceRepository;
            _mapper = mapper;
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
    }
}
