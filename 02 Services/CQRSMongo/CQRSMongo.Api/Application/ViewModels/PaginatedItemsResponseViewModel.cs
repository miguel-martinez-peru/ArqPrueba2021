using System.Collections.Generic;

namespace AcademicoOds.Api.Application.ViewModels
{
    public class PaginatedItemsResponseViewModel<TEntity> where TEntity : class
    {
        public int Skip { get; private set; }

        public int PageSize { get; private set; }

        public long Count { get; private set; }

        public IEnumerable<TEntity> Data { get; private set; }

        public PaginatedItemsResponseViewModel(int skip, int pageSize, long count, IEnumerable<TEntity> data)
        {
            this.Skip = skip;
            this.PageSize = pageSize;
            this.Count = count;
            this.Data = data;
        }
    }
}
