using System.Collections.Generic;
using System.Threading.Tasks;
using EasyRbac.Dto;
using EasyRbac.Dto.User;

namespace EasyRbac.Application.User
{
    public interface IUserService
    {
        Task<long> AddUser(CreateUserDto user);

        Task ChangePwd(long userId, ChangePwd change);

        Task DisableUser(long userId);

        Task<UserInfoDto> GetUserInfo(long userId);
        Task ChangeResouces(long userId, long appId, List<string> resouceList);

        Task<PagingList<UserInfoDto>> SearchUser(string userName, int pageIndex, int pageSize);

        Task<Dictionary<string,List<string>>> GetUserResourceIds(long userId, long appId);
    }
}