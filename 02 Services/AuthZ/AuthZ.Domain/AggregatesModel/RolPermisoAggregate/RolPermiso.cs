using AuthZ.Domain.SeedworkMongoDB;
using MongoDB.Bson.Serialization.Attributes;
using MongoDbGenericRepository.Attributes;
using System;
using System.Collections.Generic;

namespace AuthZ.Domain.AggregatesModel.RolPermisoAggregate
{
    [CollectionName("rolPermiso")]
    public class RolPermiso : AuditableMongoDocument
    {
        public RolPermiso()
        {
            Version = 1;
        }

        [BsonElement("IdPer")]
        public int IdPermiso { get; set; }

        [BsonElement("IdRol")]
        public Guid IdRol { get; set; }

    }
}
