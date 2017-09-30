using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using EasyRbac.Domain.Entity;
using EasyRbac.Dto.AppLogin;
using EasyRbac.Dto.AppResource;
using EasyRbac.Dto.UserLogin;

namespace EasyRbac.Application.Login
{
    public interface ILoginService
    {
        Task<LoginTokenEntity> GetEntityByTokenAsync(string token);

        Task<UserTokenDto> UserLoginAsync(UserLoginDto login);

        Task<AppLoginResult> AppLoginAsync(AppLoginDto request);

        Task UserLogout(string token);

        Task<List<AppResourceDto>> GetUserAppResourcesAsync(long userId, long appId);

        Task<ClaimsIdentity> GetUserClaimsIdentity(long userId, string appCode);

        Task<string> GetAppLoginCallback(string appCode);
    }
}
