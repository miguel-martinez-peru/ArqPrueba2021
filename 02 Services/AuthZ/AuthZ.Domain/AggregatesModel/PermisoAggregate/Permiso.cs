using AuthZ.Domain.SeedworkMongoDB;
using MongoDB.Bson.Serialization.Attributes;
using MongoDbGenericRepository.Attributes;
using System;
using System.Collections.Generic;

namespace AuthZ.Domain.AggregatesModel.PermisoAggregate
{
    [CollectionName("permiso")]
    public class Permiso : BaseMongoDocument
    {
        public Permiso()
        {
            Version = 1;
        }

        [BsonElement("IdPer")]
        public int IdPermiso { get; set; }

        [BsonElement("IdSis")]
        public Guid IdSistema { get; set; }

        [BsonElement("Cod")]
        public string Codigo { get; set; }

        [BsonElement("Nom")]
        public string Nombre { get; set; }

        [BsonElement("Desc")]
        public string Descripcion { get; set; }

    }
}
