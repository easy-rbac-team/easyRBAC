using System;
using System.Collections.Generic;
using System.Text;
using EasyRbac.Domain.Entity;
using EasyRbac.Domain.Enums;

namespace EasyRbac.Dto.Application
{
    public class ApplicationInfoDto
    {
        public ApplicationInfoDto()
        {
            this.CallbackConfigs = new List<ApplicationCallbackConfig>();
        }
        public long Id { get; set; }
        public string AppName { get; set; }

        public string AppCode { get; set; }

        public bool Enable { get; set; } = true;

        public DateTime CreateTime { get; set; } = DateTime.Now;

        public string Descript { get; set; }

        public string CallbackUrl { get; set; }

        public CallbackType CallbackType { get; set; }

        public string AppScret { get; set; }

        public string HomePageUrl { get; set; }

        public string IconUrl { get; set; }

        public List<ApplicationCallbackConfig> CallbackConfigs { get; set; }
    }
}
