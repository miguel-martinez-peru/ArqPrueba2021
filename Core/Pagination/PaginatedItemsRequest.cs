using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Pagination
{
    public class PaginatedItemsRequest<TEntity> where TEntity : class
    {
        public int PageSize { get; set; }
        public int Skip { get; set; }
        public string SortField { get; set; }
        public string SortDir { get; set; }
        public TEntity Filter { get; set; } = Activator.CreateInstance<TEntity>();
    }
}
