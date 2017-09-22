using System;
using System.Collections.Generic;
using System.Text;

namespace EasyRbac.Dto.UserScope
{
    public class UserScopeDto
    {
        public long Id { get; set; }

        public long AppId { get; set; }

        public bool IncludeChildren { get; set; }

        public string ResourceId { get; set; }

        public long UserId { get; set; }
      
    }
}
