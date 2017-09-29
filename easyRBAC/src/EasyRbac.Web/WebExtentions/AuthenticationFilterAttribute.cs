using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace EasyRbac.Web.WebExtentions
{
    [AttributeUsage(AttributeTargets.Class| AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
    public class ResourceTagAttribute : Attribute
    {
        public string ResourceName { get; set; }

        /// <summary>Initializes a new instance of the <see cref="T:System.Attribute"></see> class.</summary>
        public ResourceTagAttribute(string resourceName)
        {
            this.ResourceName = resourceName;
        }
    }
}
