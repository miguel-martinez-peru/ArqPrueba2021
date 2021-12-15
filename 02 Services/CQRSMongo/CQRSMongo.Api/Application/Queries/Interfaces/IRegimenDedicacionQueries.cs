using System.Threading.Tasks;
using System.Collections.Generic;
using AcademicoOds.Api.Application.ViewModels;
using AcademicoOds.Api.Application.ViewModels.DocenteModel;

namespace AcademicoOds.Api.Application.Queries
{
    public interface IRegimenDedicacionQueries
    {
        Task<PaginatedItemsResponseViewModel<RegimenDedicacionResponseDto>> Listar(RegimenDedicacionRequestDto request);
    }
}
