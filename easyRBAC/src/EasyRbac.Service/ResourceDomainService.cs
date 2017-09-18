using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyRbac.Domain.Entity;
using EasyRbac.Domain.Relations;
using EasyRbac.Dto.AppResource;
using EasyRbac.Reponsitory.BaseRepository;
using EasyRbac.Reponsitory.Helper;
using EasyRbac.Utils;

namespace EasyRbac.DomainService
{

    public class ResourceDomainService
    {
        private IKeyedIdGenerate _keyedIdGenerate;
        private IRepository<AppResourceRelation> _appResouceRel;
        private IRepository<AppResourceEntity> _resouceRepository;
        private IIdGenerator _idGenerator;
    }
}
