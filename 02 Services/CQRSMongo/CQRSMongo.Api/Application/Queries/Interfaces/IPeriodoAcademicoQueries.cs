using System.Threading.Tasks;
using System.Collections.Generic;
using AcademicoOds.Api.Application.ViewModels;
using AcademicoOds.Api.Application.ViewModels.IngresanteModel;

namespace AcademicoOds.Api.Application.Queries
{
    public interface IPeriodoAcademicoQueries
    {
        Task<PaginatedItemsResponseViewModel<PeriodoAcademicoResponseDto>> Listar(PeriodoAcademicoRequestDto request);
    }
}
