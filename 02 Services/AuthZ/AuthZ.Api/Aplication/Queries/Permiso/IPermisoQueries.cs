using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using System.Data;
using AuthZ.Api.Aplication.ViewModels;
using AuthZ.Api.Application.ViewModels;
using AuthZ.Api.Application.ViewModels.PermisoViewModel;

namespace AuthZ.Api.Application.Queries.Permiso
{
    public interface IPermisoQueries
    {
        Task<PaginatedItemsResponseViewModel<PermisoResponseViewModel>> Buscar(PaginatedItemsRequestViewModel<PermisoRequestViewModel> request);
    }
}
