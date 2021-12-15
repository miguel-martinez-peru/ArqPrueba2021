using AuthZ.Api.Aplication.ViewModels;
using AuthZ.Api.Application.ViewModels;
using AuthZ.Api.Application.ViewModels.PermisoViewModel;
using AuthZ.Domain.AggregatesModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace AuthZ.Api.Application.Queries.Permiso
{
    public class PermisoQueries : IPermisoQueries
    {
        private readonly IMongoDBGenericRepository _genericRepository;

        public PermisoQueries(IMongoDBGenericRepository genericRepository)
        {
            _genericRepository = genericRepository ?? throw new ArgumentNullException(nameof(genericRepository));
        }

        public async Task<PaginatedItemsResponseViewModel<PermisoResponseViewModel>> Buscar(PaginatedItemsRequestViewModel<PermisoRequestViewModel> request)
        {
            try
            {
                var data = await _genericRepository.GetSortedPaginatedAsync<Domain.AggregatesModel.PermisoAggregate.Permiso, Guid>(
                    x =>  (request.Filter.IdSistema == null || (request.Filter.IdSistema != null && x.IdSistema == request.Filter.IdSistema))
                        && x.EsEliminado == false,
                    x => x.IdPermiso,
                    request.SortDir == "ASC" ? true : false,
                    request.Skip,
                    request.PageSize,
                    null);

                var datatotal = await _genericRepository.CountAsync<Domain.AggregatesModel.PermisoAggregate.Permiso>(x =>
                    (request.Filter.IdSistema == null || (request.Filter.IdSistema != null && x.IdSistema == request.Filter.IdSistema))
                    && x.EsEliminado == false);

                var resp = new PaginatedItemsResponseViewModel<PermisoResponseViewModel>(
                    request.Skip, 
                    request.PageSize, 
                    datatotal, 
                    data.Select(x => new PermisoResponseViewModel
                        {
                            IdPermiso = x.IdPermiso,
                            IdSistema = x.IdSistema,
                            Nombre = x.Nombre,
                            Codigo = x.Codigo,
                            Descripcion = x.Descripcion
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