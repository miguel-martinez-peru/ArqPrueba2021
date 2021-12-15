using System.Threading.Tasks;
using System.Collections.Generic;
using AcademicoOds.Api.Application.ViewModels;

namespace AcademicoOds.Api.Application.Queries
{
    public interface IModalidadIngresoQueries
    {
        Task<PaginatedItemsResponseViewModel<ModalidadIngresoResponseDto>> Listar(ModalidadIngresoRequestDto request);
    }
}
