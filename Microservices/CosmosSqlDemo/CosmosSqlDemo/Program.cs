using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using System;
using System.Threading.Tasks;

namespace CosmosSqlDemo
{
    class Program
    {
        static DocumentClient client;
        static string endPoint;
        static string authKey;

        static void Main(string[] args)
        {
            Console.WriteLine("Process started...");
            endPoint = "https://rabi-coresql.documents.azure.com:443/";
            authKey = "uHvPfY30pTLgYYUUCKeHzCKk7DBQQB3C6zcmhkLVvwtMatHzrnLp4B9ru0H0s8u366ZmMKSTiuTwfgsLtjgbqw==";
            client = new DocumentClient(new Uri(endPoint), authKey);

            //Create database
            CreateDatabaseAsync("eshopdb").Wait();
            //Create collection
            CreateCollectionAsync("eshopdb", "products", "/Category").Wait();
            //Create a document
            var product = new
            {
                Name = "Thumsup",
                Category = "Drinks",
                Price = 40,
                Quantity = 3
            };
            //CreateDocumentAsync("eshopdb", "products", product).Wait();
            //Read document
            ReadDocumentAsync("eshopdb", "products").Wait();

            Console.WriteLine("Process completed successfully!");            
            Console.ReadKey();
        }

        static async Task CreateDatabaseAsync(string databaseName)
        {
            await client.CreateDatabaseIfNotExistsAsync(new Database { Id = databaseName });
        }

        static async Task CreateCollectionAsync(string dbName, string collectionName, string partitionKey)
        {
            DocumentCollection collection = new DocumentCollection();
            collection.Id = collectionName;
            collection.PartitionKey.Paths.Add(partitionKey);

            await client.CreateDocumentCollectionIfNotExistsAsync(
                UriFactory.CreateDatabaseUri(dbName),
                collection,
                new RequestOptions { OfferThroughput = 500 }
                );
        }

        static async Task CreateDocumentAsync(string dbName, string collectionName, dynamic item)
        {
            await client.CreateDocumentAsync(
                    UriFactory.CreateDocumentCollectionUri(dbName, collectionName),
                    item,
                    new RequestOptions
                    {
                        PartitionKey = new PartitionKey(item.Category),
                        ConsistencyLevel = ConsistencyLevel.ConsistentPrefix
                    }
                );
        }

        static async Task ReadDocumentAsync(string dbName, string collectionName)
        {
            string continuationToken = null;
            var feed = await client.ReadDocumentFeedAsync(
                UriFactory.CreateDocumentCollectionUri(dbName, collectionName),
                new FeedOptions { MaxItemCount = 10, RequestContinuation = continuationToken }
                );

            continuationToken = feed.ResponseContinuation;
            foreach (Document document in feed)
            {
                Console.WriteLine(document);
            }
        }

    }
}
