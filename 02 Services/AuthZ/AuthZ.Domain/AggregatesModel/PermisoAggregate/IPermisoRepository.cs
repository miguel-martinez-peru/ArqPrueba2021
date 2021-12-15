using Soporte.Domain.SeedworkMongoDB;
using System;
using System.Threading.Tasks;

namespace AuthZ.Domain.AggregatesModel.PermisoAggregate
{
    public interface IPermisoRepository
    {
        Permiso Add(Permiso permiso);
        Permiso Update(Permiso permiso);
        Task<Permiso> FindByIdAsync(Guid id);
        bool DeleteLogic(Permiso permiso);
    }
}
