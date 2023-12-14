using Catalog.Core.Core;
using Catalog.Core.Entities;
using Catalog.Infrastructure.Data.SeedData;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Catalog.Infrastructure.Data
{
    public class CatalogContext : ICatalogContext
    {
        private readonly List<Func<Task>> _commands;
        private readonly IConfiguration _configuration;

        private IMongoDatabase Database { get; set; }

        public IClientSessionHandle Session { get; set; }

        public MongoClient MongoClient { get; set; }

        public CatalogContext(IConfiguration configuration)
        {
            _configuration = configuration;

            // Every command will be stored and it'll be processed at SaveChanges
            _commands = new List<Func<Task>>();
        }

        public async Task<int> SaveChanges()
        {
            ConfigureMongo();

            using (Session = await MongoClient.StartSessionAsync())
            {
                Session.StartTransaction();

                var commandTasks = _commands.Select(c => c());

                await Task.WhenAll(commandTasks);

                await Session.CommitTransactionAsync();
            }

            return _commands.Count;
        }


        public IMongoCollection<T> GetCollection<T>(string name)
        {
            ConfigureMongo();

            return Database.GetCollection<T>(name);
        }

        public void Dispose()
        {
            Session?.Dispose();
            GC.SuppressFinalize(this);
        }

        //TODO think about this after the UI is completed
        public void AddCommand(Func<Task> func)
        {
            _commands.Add(func);
        }

        private void ConfigureMongo()
        {
            if (MongoClient != null)
            {
                return;
            }

            MongoClient = new MongoClient(_configuration.GetValue<string>("ConnectionStrings:DefaultConnection"));

            Database = MongoClient.GetDatabase(_configuration.GetValue<string>("ConnectionStrings:DatabaseName"));

            var Brands = Database.GetCollection<ProductBrand>(typeof(ProductBrand).Name);
            var Types = Database.GetCollection<ProductType>(typeof(ProductType).Name);
            var Products = Database.GetCollection<Product>(typeof(Product).Name);

            var isInDocker = _configuration.GetValue<bool>("IsInDocker");
            
            //TODO придумать как проверять на isDevelopment
            CatalogData.SeedData(Brands, "brands", isInDocker);
            CatalogData.SeedData(Types, "types", isInDocker);
            CatalogData.SeedData(Products, "products", isInDocker);
        }
    }
}
