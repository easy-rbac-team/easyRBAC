using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EasyRbac.Utils
{
    public static class CollectionExtentions
    {
        /// <summary>
        /// 计算集合变化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="old">原集合</param>
        /// <param name="newElementes">新集合</param>
        /// <returns>第一个返回值为新增的元素集合，第二个集合为减少的元素</returns>
        public static (IEnumerable<T>, IEnumerable<T>) CalcluteChange<T>(this IEnumerable<T> old, IEnumerable<T> newElementes)
        {
            var subElements = old.Except(newElementes);
            var addElements = newElementes.Except(old);
            return (addElements, subElements);
        }
    }
}
