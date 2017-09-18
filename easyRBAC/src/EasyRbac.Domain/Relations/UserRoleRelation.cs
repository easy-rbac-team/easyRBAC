using System;
using System.Collections.Generic;
using System.Text;
using SQLinq;

namespace EasyRbac.Domain.Relations
{
    [SQLinqTable("role_user_rel")]
    public class UserRoleRelation
    {
        public long Id { get; set; }

        public long UserId { get; set; }

        public long RoleId { get; set; }
    }
}
