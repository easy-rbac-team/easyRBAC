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
        private IRepository<ApplicationEntity> _appRepository;
        private IIdGenerator _idGenerator;

        /// <summary>Initializes a new instance of the <see cref="T:System.Object"></see> class.</summary>
        public ResourceDomainService(IKeyedIdGenerate keyedIdGenerate, IRepository<AppResourceRelation> appResouceRel, IRepository<AppResourceEntity> resouceRepository, IIdGenerator idGenerator, IRepository<ApplicationEntity> appRepository)
        {
            this._keyedIdGenerate = keyedIdGenerate;
            this._appResouceRel = appResouceRel;
            this._resouceRepository = resouceRepository;
            this._idGenerator = idGenerator;
            this._appRepository = appRepository;
        }


    }
}
