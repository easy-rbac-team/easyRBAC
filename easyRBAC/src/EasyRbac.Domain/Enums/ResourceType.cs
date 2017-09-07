using System;
using System.Collections.Generic;
using System.Text;

namespace EasyRbac.Domain.Enums
{
    [Flags]
    public enum ResourceType : short
    {
        Resource=1,
        Menu=2,
        Public = 4
    }
}
