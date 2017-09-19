using System;
using System.Collections.Generic;
using System.Text;
using SQLinq;

namespace EasyRbac.Domain.Relations
{
    [SQLinqTable("role_resource_rel")]
    public class RoleResourceRelation
    {
        public long Id { get; set; }

        public long RoleId { get; set; }

        public string ResourceId { get; set; }
    }
}
