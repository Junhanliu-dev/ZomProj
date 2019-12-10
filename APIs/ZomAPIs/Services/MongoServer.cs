using System.Threading.Tasks;
using MongoDB.Driver;

namespace ZomAPIs.Services
{
    public class MongoServer
    {
        private MongoClient _client;
        private string _connectionString;
        
        public MongoServer(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task Connect()
        {
            _client = new MongoClient(_connectionString);
            
        }
        
        

    }
}