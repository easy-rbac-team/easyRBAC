using EasyRbac.Dto.User;

namespace EasyRbac.Application.User
{
    public interface IUserControllerService
    {
        void AddUser(CreateUserDto user);
    }
}