using System;

namespace EasyRbac.Dto.Role
{
    public class RoleDto
    {
        public long Id { get; set; }

        public string RoleName { get; set; }

        public string Descript { get; set; }

        public bool Enable { get; set; }

        public DateTime CreateTime { get; set; }
    }
}
