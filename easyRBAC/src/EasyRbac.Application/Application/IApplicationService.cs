using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
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
        Task<PagingList<ApplicationInfoDto>> SearchAppAsync(string appName, int pageIndex, int pageSize);
    }
}
