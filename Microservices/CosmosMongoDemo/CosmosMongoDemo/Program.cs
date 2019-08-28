using System;
using System.Collections.Generic;
using CosmosMongoDemo.Model;
using MongoDB.Driver;
using MongoDB.Driver.Core;

namespace CosmosMongoDemo
{
    class Program
    {
        static CatalogContext catalogContext = new CatalogContext();
        static void Main(string[] args)
        {
            Console.WriteLine("Process started...");
            CatalogItem item = new CatalogItem
            {
                Name = "Orange",
                Price = 80,
                Quantity = 40,
                Vendor = new List<Vendor> {
                    new Vendor{ Id=1,Name="ABC Export"},
                    new Vendor{ Id=2,Name="XYZ Export"},
                    new Vendor{ Id=3,Name="Sai Export"},
                }
            };
            InsertItem(item);
            GetCatalogItem();
            Console.WriteLine("Process completed!");
            Console.ReadKey();
        }

        private static void InsertItem(CatalogItem catalogItem)
        {
            catalogContext.CatalogItems.InsertOne(catalogItem);
        }

        private static void GetCatalogItem()
        {
            var items = catalogContext.CatalogItems.FindAsync<CatalogItem>(FilterDefinition<CatalogItem>.Empty);
            var catalogItems = items.Result.ToList();
            foreach (var item in catalogItems)
            {
                Console.WriteLine(item.Name);
                Console.WriteLine(item.Price);
                Console.WriteLine(item.Quantity);
            }
        }
    }

}
