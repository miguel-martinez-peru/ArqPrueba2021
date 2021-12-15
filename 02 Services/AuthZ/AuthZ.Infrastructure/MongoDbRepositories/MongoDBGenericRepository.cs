using AuthZ.Domain.AggregatesModel;
using MongoDB.Driver;
using MongoDbGenericRepository;

namespace AuthZ.Infrastructure.MongoDbRepositories
{
    public class MongoDBGenericRepository : BaseMongoRepository, IMongoDBGenericRepository
    {
        public MongoDBGenericRepository(IMongoDatabase mongoDatabase)
             : base(mongoDatabase)
        {
        }
    }
}
