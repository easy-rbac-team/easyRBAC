using System;
using System.Collections.Generic;
using System.Text;
using SQLinq;

namespace EasyRbac.Domain.Entity
{
    [SQLinqTable("user_manage_resource_scope")]
    public class UserManageResourceScope
    {
        public long Id { get; set; }

        [SQLinqColumn(Ignore = true)]
        public UserEntity User { get; set; }

        public long AppId { get; set; }

        public string ResourceId { get; set; }

        public long UserId { get; set; }

        [SQLinqColumn(Ignore = true)]
        public ApplicationEntity Application { get; set; }

        [SQLinqColumn(Ignore = true)]
        public List<AppResourceEntity> Resources { get; set; }
    }
}
