using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Driver.Core;

namespace CosmosMongoDemo.Model
{
    public class CatalogItem
    {
        public CatalogItem()
        {
            this.Vendor = new List<Vendor>();
        }

        [BsonId(IdGenerator =typeof(StringObjectIdGenerator))]
        public string Id { get; set; }

        public string Name { get; set; }

        public double Price { get; set; }

        public int Quantity { get; set; }

        public List<Vendor> Vendor { get; set; }
    }

    public class Vendor
    {
        public int Id { get; set; }
        public string Name { get; set; }

    }
}
