using System;
using System.Collections.Generic;
using System.Text;

namespace EasyRbac.Dto.Application
{
    public class ApplicationInfoDto
    {
        public long Id { get; set; }
        public string AppName { get; set; }

        public string AppCode { get; set; }

        public bool Enable { get; set; } = true;

        public DateTime CreateTime { get; set; } = DateTime.Now;

        public string Descript { get; set; }

        public string CallbackUrl { get; set; }

        //public string AppScret { get; set; }
    }
}
