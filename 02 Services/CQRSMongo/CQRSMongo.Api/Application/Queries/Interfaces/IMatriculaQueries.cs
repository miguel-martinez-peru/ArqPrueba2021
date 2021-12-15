using System.Threading.Tasks;
using System.Collections.Generic;
using AcademicoOds.Api.Application.ViewModels;
using AcademicoOds.Api.Application.ViewModels.MatriculaModel;

namespace AcademicoOds.Api.Application.Queries
{
    public interface IMatriculaQueries
    {
        Task<PaginatedItemsResponseViewModel<MatriculaResponseDto>> ListarMatriculas(PaginatedItemsRequestViewModel<MatriculaRequestDto> request);
    }
}
