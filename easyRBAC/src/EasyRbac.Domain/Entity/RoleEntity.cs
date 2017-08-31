using System;
using System.Collections.Generic;
using System.Text;

namespace EasyRbac.Domain.Entity
{
    public class RoleEntity
    {
        public long Id { get; set; }

        public string RoleName { get; set; }

        public string Descript { get; set; }

        public List<ApplicationEntity> Applications { get; set; }
    }
}
