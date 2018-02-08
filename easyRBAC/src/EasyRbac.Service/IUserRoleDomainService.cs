using EasyRbac.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EasyRbac.DomainService
{
    public interface IUserRoleDomainService
    {
        Task<List<RoleEntity>> GetRolesAsync(long userId);
    }
}
