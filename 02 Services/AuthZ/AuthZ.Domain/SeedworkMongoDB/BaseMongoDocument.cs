using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace AuthZ.Domain.SeedworkMongoDB
{
    public abstract class BaseMongoDocument : MongoDbGenericRepository.Models.Document
    {
        public BaseMongoDocument()
        {
            EsActivo = true;
        }

        [BsonElement("_idStr", Order = 1)]
        public string IdString { get { return base.Id.ToString(); } }

        [BsonElement("Act", Order = 2)]
        public bool EsActivo { get; set; }

        [BsonElement("Eli", Order = 3)]
        public bool EsEliminado { get; set; }
    }

    public abstract class AuditableMongoDocument : BaseMongoDocument
    {
        [BsonElement("CrU")]
        public string UsuarioCreacion { get; set; }
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        [BsonElement("CrF")]
        public DateTime? FechaCreacion { get; set; }
        [BsonElement("CrI")]
        public string IpCreacion { get; set; }

        [BsonElement("MdU")]
        public string UsuarioModificacion { get; set; }

        [BsonElement("MdF")]
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime? FechaModificacion { get; set; }

        [BsonElement("MdI")]
        public string IpModificacion { get; set; }
    }

}
