using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Models
{
    public class PagedResultSet<TEntity> where TEntity : class
    {
        public int PageIndex { get; }
        public int PageSize { get; }
        public int TotalPages { get; }
        public int TotalRowCount { get; }
        public bool HasPreviousPage => PageIndex > 1;
        public bool HasNextPage => PageIndex < TotalPages;
        public IEnumerable<TEntity> Data { get; set; }

        public PagedResultSet(IEnumerable<TEntity> data, int pageIndex, int pageSize, int totalRowCount)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            TotalRowCount = totalRowCount;
            Data = data;
            TotalPages = (int) Math.Ceiling(totalRowCount/(double)pageSize);
        }
    }
}
