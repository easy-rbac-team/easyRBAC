using System;
using System.Collections.Generic;
using System.Text;

namespace EasyRbac.Domain.Entity
{
    public class ApplicationEntity
    {
        public long Id { get; set; }
        public string AppName { get; set; }

        public string AppCode { get; set; }

        public bool Enable { get; set; }

        public DateTime CreateTime { get; set; }

        public string Descript { get; set; }

        public List<AppResourceEntity> AppResouce {get;set;}
    }
}
