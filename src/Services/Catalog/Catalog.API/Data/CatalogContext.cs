using Catalog.API.Entities;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.API.Data
{
    public class CatalogContext : ICatalogContext
    {
        public CatalogContext(IConfiguration configuration)
        {
            var dbstring = configuration.GetValue<string>("DatabaseSettings:ConnectionString");
            var client = new MongoClient(dbstring);

            var test = client.Cluster.Description.State;

            var database = client.GetDatabase("DatabaseSettings:DatabaseName");

            Products = database.GetCollection<Product>("DatabaseSettings:CollectionName");

            CatalogContextSeed.SeedData(Products);
        }

        public IMongoCollection<Product> Products { get; }
    }
}
