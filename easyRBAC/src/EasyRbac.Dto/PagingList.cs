using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EasyRbac.Dto
{
    public class PagingList<T>
    {
        public PagingList(int totalCount, int pageIndex, int pageSize, IEnumerable<T> items)
        {

            this.Items = items.ToList();

            this.Page = new Page()
            {
                TotalCount = totalCount,
                PageIndex = pageIndex,
                TotalPage = (int)Math.Ceiling((double)(totalCount / pageSize)),
                PageSize = pageSize
            };
        }

        public PagingList() { }

        public Page Page { get; set; }

        public List<T> Items { get; set; }
    }

    public class Page
    {
        public int TotalCount { get; set; }

        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public int TotalPage { get; set; }
    }
}
