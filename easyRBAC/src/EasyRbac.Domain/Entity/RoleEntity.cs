using System;
using System.Collections.Generic;
using System.Text;
using SQLinq;

namespace EasyRbac.Domain.Entity
{
    [SQLinqTable("role")]
    public class RoleEntity
    {
        public long Id { get; set; }

        public string RoleName { get; set; }

        public string Descript { get; set; }

        public bool Enable { get; set; } = true;

        public DateTime CreateTime { get; set; } = DateTime.Now;

        [SQLinqColumn(Ignore = true)]
        public List<ApplicationEntity> Applications { get; set; }
    }
}
