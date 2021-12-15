using System.Threading.Tasks;
using System.Collections.Generic;
using AcademicoOds.Api.Application.ViewModels;
using AcademicoOds.Api.Application.ViewModels.DocenteModel;

namespace AcademicoOds.Api.Application.Queries
{
    public interface ILocalProgramasQueries
    {
        Task<PaginatedItemsResponseViewModel<EntidadFilialResponseDto>> ListarFilial(LocalProgramasRequestDto request);
        Task<PaginatedItemsResponseViewModel<EntidadLocalResponseDto>> ListarLocal(LocalProgramasRequestDto request);
        Task<PaginatedItemsResponseViewModel<EntidadFacultadResponseDto>> ListarFacultad(LocalProgramasRequestDto request);
        Task<PaginatedItemsResponseViewModel<EntidadProgramaResponseDto>> ListarPrograma(LocalProgramasRequestDto request);
    }
}
