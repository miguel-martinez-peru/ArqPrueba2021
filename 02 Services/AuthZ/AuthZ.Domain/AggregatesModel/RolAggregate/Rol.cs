using AuthZ.Domain.SeedworkMongoDB;
using MongoDB.Bson.Serialization.Attributes;
using MongoDbGenericRepository.Attributes;
using System;
using System.Collections.Generic;

namespace AuthZ.Domain.AggregatesModel.RolAggregate
{
    [CollectionName("rol")]
    public class Rol : BaseMongoDocument
    {
        public Rol()
        {
            Version = 1;
        }

        [BsonElement("IdRol")]
        public Guid IdRol { get; set; }

        [BsonElement("IdSis")]
        public int IdSistema { get; set; }

        [BsonElement("Nom")]
        public string Nombre { get; set; }

    }
}
