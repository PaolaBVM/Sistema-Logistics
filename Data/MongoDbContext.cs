using MongoDB.Driver;
using Microsoft.Extensions.Options;
using SistemaLogistics.Models;

namespace SistemaLogistics.Data
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;

        public MongoDbContext(IOptions<MongoDBSettings> settings)
        {
            try
            {
                var client = new MongoClient(settings.Value.ConnectionString);
                _database = client.GetDatabase(settings.Value.DatabaseName);
                Console.WriteLine("MongoDB Context inicializado");
            }
            catch (Exception ex)
            {
                Console.WriteLine($" Error inicializando MongoDB: {ex.Message}");
                throw;
            }
        }

        public IMongoCollection<T> GetCollection<T>(string collectionName)
        {
            return _database.GetCollection<T>(collectionName);
        }
    }
}