using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using EasyRbac.Domain.Entity;
using EasyRbac.Dto;
using EasyRbac.Dto.Application;
using EasyRbac.Reponsitory.BaseRepository;
using EasyRbac.Utils;

namespace EasyRbac.Application.Application
{
    public class ApplicationService : IApplicationService
    {
        private readonly IRepository<ApplicationEntity> _appRepository;
        private readonly IMapper _mapper;
        private readonly IIdGenerator _idGenerator;
        private readonly IEncryptHelper _encryptHelper;

        public ApplicationService(IRepository<ApplicationEntity> appRepository, IIdGenerator idGenerator, IMapper mapper, IEncryptHelper encryptHelper)
        {
            this._appRepository = appRepository;
            this._idGenerator = idGenerator;
            this._mapper = mapper;
            this._encryptHelper = encryptHelper;
        }

        public Task DisableApp(long id)
        {
            return this._appRepository.UpdateAsync(
                () => new ApplicationEntity()
                {
                    Enable = false
                },
                x => x.Id == id);
        }

        public Task EditAsync(long id, ApplicationInfoDto value)
        {
            return this._appRepository.UpdateAsync(() => new ApplicationEntity
            {
                AppName =  value.AppName,
                AppCode = value.AppCode,
                Descript = value.Descript
            }, x => x.Id == id);
        }

        public async Task<ApplicationInfoDto> AddAppAsync(ApplicationInfoDto app)
        {
            var applicationEntity = this._mapper.Map<ApplicationEntity>(app);
            applicationEntity.Id = this._idGenerator.NewId();
            applicationEntity.AppScret = this._encryptHelper.GenerateSalt();
            await this._appRepository.InsertAsync(applicationEntity);
            return app;
        }

        public Task<ApplicationInfoDto> GetOneAsync(long id)
        {
            return this._appRepository.QueryFirstAsync(x => x.Id == id)
                .ContinueWith(x=>this._mapper.Map<ApplicationInfoDto>(x.Result));
        }

        public async Task<PagingList<ApplicationInfoDto>> SearchAppAsync(string appName, int pageIndex, int pageSize)
        {
            PagingList<ApplicationEntity> rsult = await this._appRepository.QueryByPagingAsync(x => x.Enable == true && (x.AppName == appName || x.AppCode == appName), x => x.Id, pageIndex, pageSize);
            return this._mapper.Map<PagingList<ApplicationInfoDto>>(rsult);
        }

        public async Task<string> GetAppScretAsync(long appId)
        {
            var result = await this._appRepository.QueryFirstAsync(x => x.Id == appId);
            return result.AppScret;
        }

        public Task EditAppScretAsync(long appId)
        {
            string newSecuret = this._encryptHelper.GenerateSalt();
            return this._appRepository.UpdateAsync(
                () => new ApplicationEntity
                {
                    AppScret = newSecuret
                },
                x => x.Id == appId);
        }
    }
}
