using System.Threading.Tasks;
using EasyRbac.Dto.User;

namespace EasyRbac.Application.User
{
    public interface IUserControllerService
    {
        Task AddUser(CreateUserDto user);

        Task ChangePwd(long userId, ChangePwd change);

        Task DisableUser(long userId);

        Task<UserInfoDto> GetUserInfo(long userId);
    }
}