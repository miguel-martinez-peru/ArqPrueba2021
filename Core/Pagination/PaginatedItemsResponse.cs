using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Pagination
{
    public class PaginatedItemsResponse<TEntity> where TEntity : class
    {
        public int PageIndex { get; private set; }

        public int PageSize { get; private set; }

        public long Count { get; private set; }

        public IEnumerable<TEntity> Data { get; set; }
        public PaginatedItemsResponse()
        {

        }
        [JsonConstructor]
        public PaginatedItemsResponse(int pageIndex, int pageSize, long count, IEnumerable<TEntity> data)
        {
            this.PageIndex = pageIndex;
            this.PageSize = pageSize;
            this.Count = count;
            this.Data = data;
        }
    }
}
