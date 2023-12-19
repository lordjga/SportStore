using Catalog.Core.Core;
using Catalog.Core.Entities;
using Catalog.Core.Repositories.Base;
using MongoDB.Driver;

namespace Catalog.Infrastructure.Repositories.Base
{
    public abstract class ReadRepository<TEntity> : IReadRepository<TEntity> where TEntity : BaseEntity
    {
        protected readonly ICatalogContext _catalogContext;
        protected IMongoCollection<TEntity> collection;

        public ReadRepository(ICatalogContext catalogContext)
        {
            _catalogContext = catalogContext;
            collection = _catalogContext.GetCollection<TEntity>(typeof(TEntity).Name);
        }

        public virtual async Task<IEnumerable<TEntity>> GetAll()
        {
            return await collection.Find(p => true).ToListAsync();
        }

        public async Task<TEntity> GetById(string id)
        {
            return await collection.Find(p => p.Id == id).FirstOrDefaultAsync();
        }

        public void Dispose()
        {
            _catalogContext.Dispose();
        }
    }
}
