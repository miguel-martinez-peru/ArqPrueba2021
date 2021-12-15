using Soporte.Domain.SeedworkMongoDB;
using System;
using System.Threading.Tasks;

namespace AuthZ.Domain.AggregatesModel.RolPermisoAggregate
{
    public interface IRolPermisoRepository
    {
        RolPermiso Add(RolPermiso permiso);
        RolPermiso Update(RolPermiso permiso);
        Task<RolPermiso> FindByIdAsync(Guid id);
        bool DeleteLogic(RolPermiso permiso);
    }
}
