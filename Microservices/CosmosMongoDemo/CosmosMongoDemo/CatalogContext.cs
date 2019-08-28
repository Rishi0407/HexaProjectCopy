using CosmosMongoDemo.Model;
using MongoDB.Driver;

namespace CosmosMongoDemo
{
    class CatalogContext
    {
        private readonly IMongoDatabase database;
        public CatalogContext()
        {
            var connectionString = "mongodb://rabi-mongoapi:syIg31wXK1fHJsn93vQas1qdGxjW1VPMIJ6RpVMysYBjZUsEdz5DiOJ0dTqZEYYAmuz4YqlakM6eYSuaUt8idg==@rabi-mongoapi.documents.azure.com:10255/?ssl=true&replicaSet=globaldb";
            var databaseName = "eshopdb1";
            MongoClientSettings clientSettings = MongoClientSettings.FromConnectionString(connectionString);
            MongoClient client = new MongoClient(clientSettings);
            if (client != null)
            {
                this.database = client.GetDatabase(databaseName);
            }
        }

        public IMongoCollection<CatalogItem> CatalogItems
        {
            get {
                return this.database.GetCollection<CatalogItem>("CatalogItems");
            }
        }
    }
}
