using System;
using System.Collections.Generic;
using System.Text;

namespace EasyRbac.Domain.Enums
{
    [Flags]
    public enum CallbackType : short
    {
        None = 0,
        Jsonp =1,
        Cors,
        Redirect
    }
}
