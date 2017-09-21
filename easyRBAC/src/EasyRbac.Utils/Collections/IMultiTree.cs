using System;
using System.Collections.Generic;
using System.Text;

namespace EasyRbac.Utils.Collections
{
    public interface IMultiTree<T>
    {
        List<T> Children { get; set; }
    }
}
