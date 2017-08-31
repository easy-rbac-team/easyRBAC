using System;
using System.Collections.Generic;
using System.Text;

namespace EasyRbac.Domain.Entity
{
    public class UserEntity
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string Salt { get; set; }

        public string RealName { get; set; }

        public bool Enable { get; set; }

        public List<RoleEntity> Roles { get; set; }

        public List<ApplicationEntity> Applications { get; set; }

        public List<AppResourceEntity> Resources { get; set; }

    }
}
