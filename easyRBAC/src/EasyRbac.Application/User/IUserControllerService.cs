using System.Threading.Tasks;
using EasyRbac.Dto.User;

namespace EasyRbac.Application.User
{
    public interface IUserControllerService
    {
        Task AddUser(CreateUserDto user);
    }
}