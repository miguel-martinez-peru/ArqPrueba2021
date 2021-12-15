using AuthZ.Api.Infrastructure.Cache;
using AuthZ.Domain.AggregatesModel;
using AuthZ.Domain.AggregatesModel.RolPermisoAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthZ.Api.Aplication.Queries
{
    public class CacheManager
    {
        private readonly IMongoDBGenericRepository _genericRepository;
        private Guid _rol;

        public CacheManager(IMongoDBGenericRepository genericRepository)
        {
            _genericRepository = genericRepository ?? throw new ArgumentNullException(nameof(genericRepository));
        }

        public List<RolPermiso> ObtenerPermisosPorRol(Guid rol)
        {
            _rol = rol;
            var lista = CacheApp.ResolverLista("Rol_" + rol.ToString(),
              ActualizaPermisosPorRol,
              "", "ASC", false, 5);

            return lista;
        }
        
        private List<RolPermiso> ActualizaPermisosPorRol()
        {
            var resp = _genericRepository.GetAll<Domain.AggregatesModel.RolPermisoAggregate.RolPermiso>(x => x.IdRol == _rol
                && x.EsEliminado == false);

            return resp;
        }


    }
}
