using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EasyRbac.Utils.Collections
{
    public class MultiTreeHelper
    {
        public static List<T> ToToMultiTree<T,TKey>(IEnumerable<T> source,Func<T,TKey> keySelector,Func<T,TKey> parentKeySelector)
            where T:IMultiTree<T>
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
