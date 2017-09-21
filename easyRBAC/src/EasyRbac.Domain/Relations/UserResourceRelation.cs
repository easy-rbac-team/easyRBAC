using System;
using System.Collections.Generic;
using System.Text;
using SQLinq;

namespace EasyRbac.Domain.Relations
{
    [SQLinqTable("user_resource_rel")]
    public class UserResourceRelation
    {
        public long Id { get; set; }

        public long UserId { get; set; }

        public string ResourceId { get; set; }

        public long AppId { get; set; }
    }
}
