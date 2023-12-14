using Catalog.Core.Entities;
using MongoDB.Driver;
using System.Text.Json;

namespace Catalog.Infrastructure.Data.SeedData
{
    internal class CatalogData
    {
        public static void SeedData<T>(IMongoCollection<T> brandCollection, string fileName, bool isInDocker) where T : BaseEntity
        {
            bool checkData = brandCollection.Find(b => true).Any();
            string path = isInDocker
                ? Path.Combine("Data", "SeedData", $"{fileName}.json")
                : Path.Combine("../Catalog.Infrastructure/Data/SeedData/" + $"{fileName}.json");
            Console.WriteLine(isInDocker.ToString());
            if (!checkData)
            {
                var data = File.ReadAllText(path);
                var collection = JsonSerializer.Deserialize<List<T>>(data);
                if (collection != null)
                {
                    foreach (var item in collection)
                    {
                        brandCollection.InsertOneAsync(item);
                    }
                }
            }
        }
    }
}
