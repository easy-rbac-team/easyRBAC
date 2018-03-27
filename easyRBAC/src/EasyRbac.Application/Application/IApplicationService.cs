using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EasyRbac.Domain.Entity;
using EasyRbac.Dto;
using EasyRbac.Dto.Application;

namespace EasyRbac.Application.Application
{
    public interface IApplicationService
    {
        Task DisableApp(long id);
        Task EditAsync(long id, ApplicationInfoDto value);
        Task<ApplicationInfoDto> AddAppAsync(ApplicationInfoDto app);
        Task<ApplicationInfoDto> GetOneAsync(long id);

        Task<ApplicationInfoDto> GetOneAsync(string code);

        Task<PagingList<ApplicationInfoDto>> SearchAppAsync(string appName, int pageIndex, int pageSize);
        Task<string> ChangeAppSecuretAsync(long id);

        Task<ApplicationEntity> GetAppByUserId(long userId);
    }
}
