using MongoDB.Driver;

namespace Authentication_API.Services
{
    public class MongoDbConnectionService
    {
        public IMongoClient Client { get; private set; }

        public IMongoDatabase Database { get; private set; }

        public MongoDbConnectionService(IConfiguration config)
        {
            var connectionString = config.GetSection("MongoDB:ConnectionURI").Value;
            var databaseName = config.GetSection("MongoDB:DatabaseName").Value;

            Client = new MongoClient(connectionString);
            Database = Client.GetDatabase(databaseName);
        }
    }
}
