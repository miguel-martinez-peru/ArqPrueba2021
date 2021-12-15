using System.Threading.Tasks;
using System.Collections.Generic;
using CQRSSqlServer.Api.Application.ViewModels;
using CQRSSqlServer.Api.Application.ViewModels.DocenteModel;
using Core.Pagination;

namespace CQRSSqlServer.Api.Application.Queries
{
    public interface ICondicionLaboralQueries
    {
        Task<PaginatedItemsResponse<CondicionLaboralResponseDto>> Listar(CondicionLaboralRequestDto request);
    }
}
