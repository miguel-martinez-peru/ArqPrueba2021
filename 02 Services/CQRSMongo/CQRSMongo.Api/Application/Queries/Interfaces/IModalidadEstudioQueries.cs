using System.Threading.Tasks;
using System.Collections.Generic;
using AcademicoOds.Api.Application.ViewModels;

namespace AcademicoOds.Api.Application.Queries
{
    public interface IModalidadEstudioQueries
    {
        Task<PaginatedItemsResponseViewModel<ModalidadEstudioResponseDto>> Listar(ModalidadEstudioRequestDto request);
    }
}
