using MongoDB.Driver;
using MongoDbGenericRepository;
using Soporte.Domain.SeedworkMongoDB;
using System;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using AuthZ.Domain.AggregatesModel.PermisoAggregate;

namespace AuthZ.Infrastructure.MongoDBRepositories
{
    public class PermisoRepository : BaseMongoRepository, IPermisoRepository
    {
        public PermisoRepository(IMongoDatabase mongoDatabase)
            : base(mongoDatabase)
        {
        }

        public Permiso Add(Permiso permiso)
        {
            throw new NotImplementedException();
        }

        public Permiso Update(Permiso permiso)
        {
            throw new NotImplementedException();
        }

        Task<Permiso> IPermisoRepository.FindByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public bool DeleteLogic(Permiso permiso)
        {
            throw new NotImplementedException();
        }

        //public ArchivoCarga Add(ArchivoCarga archivoCarga)
        //{
        //    archivoCarga.IdArchivo = GetIncrementIdArchivoCarga();
        //    base.AddOne(archivoCarga);
        //    return archivoCarga;
        //}

        //public bool DeleteLogic(ArchivoCarga archivoCarga)
        //{
        //    var updEliminacionLogica = Builders<ArchivoCarga>.Update
        //       .Set(b => b.EsEliminado, true)
        //       .Set(b => b.FechaModificacion, DateTime.Now)
        //       .Set(b => b.IpModificacion, archivoCarga.IpModificacion)
        //       .Set(b => b.UsuarioModificacion, archivoCarga.UsuarioModificacion);

        //    return base.UpdateOne(archivoCarga, updEliminacionLogica);
        //}

        //public async Task<ArchivoCarga> FindByIdAsync(Guid id)
        //{
        //    return await base.GetByIdAsync<ArchivoCarga>(id);
        //}
        //public async Task<ArchivoCarga> FindByIdArchivoAsync(int idArchivo)
        //{
        //    return await base.GetOneAsync<ArchivoCarga>(x => x.IdArchivo == idArchivo); ;
        //}

        //private int GetIncrementIdArchivoCarga()
        //{
        //    string nombreColeccion = "archivosCarga";
        //    var filter = Builders<CollectionCounter>.Filter.Where(x => x.Id == nombreColeccion);
        //    var update = Builders<CollectionCounter>.Update.Inc(x => x.Identity, 1);
        //    var options = new FindOneAndUpdateOptions<CollectionCounter>() { IsUpsert = true, };
        //    var task = base.GetAndUpdateOne<CollectionCounter, string>(filter, update, options);
        //    task.Wait();
        //    var counter = task.Result;
        //    if (counter == null) return 1;
        //    return counter.Identity + 1;
        //}

        //public ArchivoCarga Update(ArchivoCarga archivoCarga)
        //{
        //    base.UpdateOne(archivoCarga);
        //    return archivoCarga;
        //}
        //public bool UpdateStatus(ArchivoCarga archivoCarga, EstadoCarga estado, bool actualizarFechaAsociada)
        //{
        //    return DoUpdateStatus(archivoCarga, estado, actualizarFechaAsociada);
        //}
        //public bool UpdateStatus(ArchivoCarga archivoCarga, EstadoCarga estado)
        //{
        //    return DoUpdateStatus(archivoCarga, estado, false);
        //}
        //private bool DoUpdateStatus(ArchivoCarga archivoCarga, EstadoCarga estado, bool actualizarFechaAsociada)
        //{
        //    UpdateDefinition<ArchivoCarga> updStatus;
        //    if (actualizarFechaAsociada == false)
        //    {
        //        updStatus = Builders<ArchivoCarga>.Update
        //                                                .Set(b => b.Estado, estado)
        //                                                .Set(b => b.FechaModificacion, DateTime.Now)
        //                                               ;
        //        return base.UpdateOne(archivoCarga, updStatus);
        //    }
        //    switch (estado)
        //    {
        //        case EstadoCarga.EN_REGISTRO:
        //            updStatus = Builders<ArchivoCarga>.Update.Set(b => b.Estado, estado)
        //                                                     .Set(b => b.FechaRegistroInicio, DateTime.Now)
        //                                                     .Set(b => b.FechaModificacion, DateTime.Now)
        //                                                     ;
        //            break;
        //        case EstadoCarga.FINALIZADO:
        //            updStatus = Builders<ArchivoCarga>.Update.Set(b => b.Estado, estado)
        //                                                     .Set(b => b.FechaRegistroFin, DateTime.Now)
        //                                                     .Set(b => b.FechaModificacion, DateTime.Now)
        //                                                     ;
        //            break;
        //        default:
        //            updStatus = Builders<ArchivoCarga>.Update
        //                                               .Set(b => b.Estado, estado)
        //                                               .Set(b => b.FechaModificacion, DateTime.Now)
        //                                              ;
        //            break;
        //    }


        //    return base.UpdateOne(archivoCarga, updStatus);
        //}

    }
}
