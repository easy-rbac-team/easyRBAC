using EasyRbac.Domain.Enums;
using SQLinq;
using System;
using System.Collections.Generic;
using System.Text;

namespace EasyRbac.Domain.Entity
{
    [SQLinqTable("application_callback_config")]
    public class ApplicationCallbackConfig
    {
        public long Id { get; set; }

        public long AppId { get; set; }

        public string Enviroment { get; set; }

        public string CallbackUrl { get; set; }

        public CallbackType CallbackType { get; set; }

        public string Remark { get; set; }

        public string CreateBy { get; set; }

        public DateTime CreateTime { get; set; } = DateTime.Now;        
    }
}
