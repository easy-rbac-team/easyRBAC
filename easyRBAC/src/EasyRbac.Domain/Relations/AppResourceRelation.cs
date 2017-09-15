using System;
using System.Collections.Generic;
using System.Text;
using SQLinq;

namespace EasyRbac.Domain.Relations
{
    [SQLinqTable("app_resource_rel")]
    public class AppResourceRelation
    {
        public long Id { get; set; }

        public long AppId { get; set; }

        public string AppResouceId { get; set; }
    }
}
