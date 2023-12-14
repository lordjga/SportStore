using Catalog.Core.Entities;
using MongoDB.Driver;

namespace Catalog.Core.Core
{
    public interface ICatalogContext : IDisposable
    {
        void AddCommand(Func<Task> function);

        Task<int> SaveChanges();

        IMongoCollection<T> GetCollection<T>(string name);
    }
}
