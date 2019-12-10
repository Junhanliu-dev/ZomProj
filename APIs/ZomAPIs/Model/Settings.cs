using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace ZomAPIs.Model
{
    public class Settings
    {
        public string ConnectionString;
        public string Database;
    }

    public class MongoContext
    {
        private readonly IMongoDatabase _database = null;

        public MongoContext(IOptions<Settings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            
            if (client != null)
            {
                _database = client.GetDatabase(settings.Value.Database);
            }
        }

        public IMongoCollection<Restaurant> Restaurants
        {
            get { return _database.GetCollection<Restaurant>("restaurants"); }
        }
    }
}