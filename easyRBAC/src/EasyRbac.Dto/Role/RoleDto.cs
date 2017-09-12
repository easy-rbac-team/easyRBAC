using System;

namespace EasyRbac.Dto.Role
{
    public class RoleDto
    {
        public long Id { get; set; }

        public string RoleName { get; set; }

        public string Descript { get; set; }

        public bool Enable { get; set; } = true;

        public DateTime CreateTime { get; set; } = DateTime.Now;
    }
}
