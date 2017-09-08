using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EasyRbac.Dto
{
    public class PagingList<T>
    {
        public PagingList(int totalCount,int pageIndex,int pageSize,IEnumerable<T> items)
        {
            this.TotalCount = totalCount;
            this.PageIndex = pageIndex;
            this.Items = items.ToList();
            this.TotalPage = (int)Math.Ceiling((double)(totalCount / pageSize));
            this.PageSize = pageSize;
        }

        public PagingList() { }

        public int TotalCount { get; set; }

        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public int TotalPage { get; set; }

        public List<T> Items { get; set; }
    }
}
