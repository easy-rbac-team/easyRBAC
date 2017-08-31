using System;
using System.Collections.Generic;
using System.Text;

namespace EasyRbac.Domain.Entity
{
    public class ResourceScope
    {
        public long Id { get; set; }

        public UserEntity User { get; set; }

        public bool IncludeChildren { get; set; }

        public ApplicationEntity Application { get; set; }

        public List<AppResourceEntity> Resources { get; set; }
    }
}
