using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyRbac.Web.Options
{
    public class AppOption
    {
        public string AppCode { get; set; }

        public int UserLoginExpireIn { get; set; }

        public int AppLoginExpireIn { get; set; }
    }
}
