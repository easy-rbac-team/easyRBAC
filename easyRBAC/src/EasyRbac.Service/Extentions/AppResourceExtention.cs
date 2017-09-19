using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using EasyRbac.Dto.AppResource;

namespace EasyRbac.DomainService.Extentions
{
    public static class AppResourceExtention
    {
        public static AppResourceDto GenerateTree(this IEnumerable<AppResourceDto> source)
        {
            foreach (AppResourceDto item in source)
            {
                var subRex = new Regex($"{item.Id}\\d\\d$");
                List<AppResourceDto> children = source.Where(x =>
                    {
                        subRex.IsMatch(x.Id);
                        return subRex.IsMatch(x.Id);
                    }
                ).ToList();
                item.Children = children;
            }
            AppResourceDto root = source.FirstOrDefault(x => x.Id == source.Min(y => y.Id));
            
            return root;
        }
    }
}
