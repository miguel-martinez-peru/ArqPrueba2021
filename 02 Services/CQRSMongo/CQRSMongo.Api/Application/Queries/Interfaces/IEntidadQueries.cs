using System.Threading.Tasks;
using System.Collections.Generic;
using AcademicoOds.Api.Application.ViewModels;
using AcademicoOds.Api.Application.ViewModels.DocenteModel;

namespace AcademicoOds.Api.Application.Queries
{
    public interface IEntidadQueries
    {
        Task<PaginatedItemsResponseViewModel<EntidadResponseDto>> Listar(EntidadRequestDto request);
    }
}
