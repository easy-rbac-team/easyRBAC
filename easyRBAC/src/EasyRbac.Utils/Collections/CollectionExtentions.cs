using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EasyRbac.Utils.Collections;

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

        public static List<T> ToToMultiTree<T, TKey>(this IEnumerable<T> source, Func<T, TKey> keySelector, Func<T, TKey> parentKeySelector)
            where T : IMultiTree<T>
        {
            var dic = source.ToDictionary(keySelector);

            var rootList = new List<T>();
            foreach (T item in dic.Values)
            {
                var parentKey = parentKeySelector(item);

                if (dic.TryGetValue(parentKey, out T parent))
                {
                    if (parent.Children == null)
                    {
                        parent.Children = new List<T>()
                        {
                            item
                        };
                    }
                    else
                    {
                        parent.Children.Add(item);
                    }
                }
                else
                {
                    rootList.Add(item);
                }
            }


            return rootList;
        }
    }
}
