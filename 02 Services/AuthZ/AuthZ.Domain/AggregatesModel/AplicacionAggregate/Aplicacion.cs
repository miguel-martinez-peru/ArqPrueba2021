using AuthZ.Domain.SeedworkMongoDB;
using MongoDB.Bson.Serialization.Attributes;
using MongoDbGenericRepository.Attributes;
using System;
using System.Collections.Generic;

namespace AuthZ.Domain.AggregatesModel.AplicacionAggregate
{
    [CollectionName("aplicacion")]
    public class Aplicacion : BaseMongoDocument
    {
        public Aplicacion()
        {
            Version = 1;
        }

        [BsonElement("IdSis")]
        public Guid IdSistema { get; set; }

        [BsonElement("Cod")]
        public int Codigo { get; set; }

        [BsonElement("Nom")]
        public string Nombre { get; set; }

        [BsonElement("Abr")]
        public string Abreviatura { get; set; }

    }
}
