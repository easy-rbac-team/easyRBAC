using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using EasyRbac.Dto.AppResource;

namespace EasyRbac.DomainService.Extentions
{
    public static class AppResourceExtention
    {
        public static AppResourceDto GenerateTree(this IEnumerable<AppResourceDto> source)
        {
            AppResourceDto root = null;
            foreach (AppResourceDto item in source)
            {
                if (root == null)
                {
                    root = item;
                }
                if (string.CompareOrdinal(item.Id,root?.Id) < 0)
                {
                    root = item;
                }
                var subRex = new Regex($"{item.Id}\\d\\d$");
                List<AppResourceDto> children = source.Where(x =>
                    {
                        subRex.IsMatch(x.Id);
                        return subRex.IsMatch(x.Id);
                    }
                ).ToList();
                item.Children = children;
            }

            return root;
        }
    }
}
