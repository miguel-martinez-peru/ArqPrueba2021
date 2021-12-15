using System.Threading.Tasks;
using System.Collections.Generic;
using AcademicoOds.Api.Application.ViewModels;
using AcademicoOds.Api.Application.ViewModels.IngresanteModel;

namespace AcademicoOds.Api.Application.Queries
{
    public interface IProcesoAdmisionQueries
    {
        Task<PaginatedItemsResponseViewModel<ProcesoAdmisionResponseDto>> Listar(ProcesoAdmisionRequestDto request);
    }
}
