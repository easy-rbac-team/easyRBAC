using System.Collections.Generic;
using System.Threading.Tasks;
using EasyRbac.Dto.AppResource;

namespace EasyRbac.DomainService
{
    public interface IUserResourceDomainService
    {
        Task<List<string>> GetUserAssociationResourcesAsync(long userId, long appId);

        /// <summary>
        /// 获取用户关联的角色所关联的资源
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="appId"></param>
        /// <returns></returns>
        Task<List<string>> GetUserAssociationRolseResourcesAsync(long userId, long appId);

        /// <summary>
        /// 获取用户所有资源
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="appId"></param>
        /// <returns></returns>
        Task<List<AppResourceDto>> GetUserAllAppResourcesAsync(long userId,long appId);

        /// <summary>
        /// 根据APPCODE获取用户资源列表
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="appCode"></param>
        /// <returns></returns>
        Task<List<AppResourceDto>> GetUserAllAppResourcesAsync(long userId, string appCode);

        Task ChangeUserResource(long userId,long appId, List<string> resourceId);
    }
}