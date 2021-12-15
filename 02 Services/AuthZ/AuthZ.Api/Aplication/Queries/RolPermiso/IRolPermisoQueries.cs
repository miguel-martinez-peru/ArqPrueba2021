using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using System.Data;
using AuthZ.Api.Aplication.ViewModels;
using AuthZ.Api.Application.ViewModels;
using AuthZ.Api.Application.ViewModels.RolPermisoViewModel;

namespace AuthZ.Api.Application.Queries.RolPermiso
{
    public interface IRolPermisoQueries
    {
        bool ValidaPermiso(ValidaRequestViewModel request);
        Task<PaginatedItemsResponseViewModel<RolPermisoResponseViewModel>> Buscar(PaginatedItemsRequestViewModel<RolPermisoRequestViewModel> request);
    }
}
