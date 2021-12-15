using AuthZ.Api.Aplication.Queries;
using AuthZ.Api.Aplication.ViewModels;
using AuthZ.Api.Application.ViewModels;
using AuthZ.Api.Application.ViewModels.RolPermisoViewModel;
using AuthZ.Domain.AggregatesModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace AuthZ.Api.Application.Queries.RolPermiso
{
    public class RolPermisoQueries : IRolPermisoQueries
    {
        private readonly IMongoDBGenericRepository _genericRepository;
        private readonly CacheManager _cacheManager;

        public RolPermisoQueries(IMongoDBGenericRepository genericRepository,
            CacheManager cacheManager)
        {
            _genericRepository = genericRepository ?? throw new ArgumentNullException(nameof(genericRepository));
            _cacheManager = cacheManager;
        }

        public bool ValidaPermiso(ValidaRequestViewModel request)
        {
            try
            {
                var lista = _cacheManager.ObtenerPermisosPorRol(request.rol);
                
                var existe = lista.Any(x => x.IdPermiso == request.permiso);
                if (existe)
                    return true;
            }
            catch (Exception ex)
            {
                Serilog.Log.Error(ex, $"RolPermisoQueries.ValidaPermiso. Ocurrió un error: {ex.Message}");
            }

            return false;
        }

        public async Task<PaginatedItemsResponseViewModel<RolPermisoResponseViewModel>> Buscar(PaginatedItemsRequestViewModel<RolPermisoRequestViewModel> request)
        {
            try
            {
                var data = await _genericRepository.GetSortedPaginatedAsync<Domain.AggregatesModel.RolPermisoAggregate.RolPermiso, Guid>(
                    x => (request.Filter.IdRol == null || (request.Filter.IdRol != null && x.IdRol == request.Filter.IdRol))
                        && (request.Filter.IdPermiso == null || (request.Filter.IdPermiso != null && x.IdPermiso == request.Filter.IdPermiso))
                        && x.EsEliminado == false,
                    null,
                    request.SortDir == "ASC" ? true : false,
                    request.Skip,
                    request.PageSize,
                    null);

                var datatotal = await _genericRepository.CountAsync<Domain.AggregatesModel.RolPermisoAggregate.RolPermiso>(x =>
                        (request.Filter.IdRol == null || (request.Filter.IdRol != null && x.IdRol == request.Filter.IdRol))
                        && (request.Filter.IdPermiso == null || (request.Filter.IdPermiso != null && x.IdPermiso == request.Filter.IdPermiso))
                        && x.EsEliminado == false);

                var resp = new PaginatedItemsResponseViewModel<RolPermisoResponseViewModel>(
                    request.Skip,
                    request.PageSize,
                    datatotal,
                    data.Select(x => new RolPermisoResponseViewModel
                    {
                        IdPermiso = x.IdPermiso,
                        IdRol = x.IdRol,
                        EsEliminado = x.EsEliminado
                    })
                    );

                return resp;

            }
            catch (Exception ex)
            {
                Serilog.Log.Error(ex, $"PermisoQueries.Buscar. Ocurrió un error: {ex.Message}");
            }

            return null;
        }

    }
}