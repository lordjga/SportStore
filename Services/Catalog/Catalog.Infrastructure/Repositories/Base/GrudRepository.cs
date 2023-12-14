using Catalog.Core.Core;
using Catalog.Core.Entities;
using Catalog.Core.Repositories.Base;
using MongoDB.Driver;

namespace Catalog.Infrastructure.Repositories.Base
{
    public abstract class GrudRepository<TEntity> : BaseRepository<TEntity>, IGrudRepository<TEntity> where TEntity : BaseEntity
    {
        protected GrudRepository(ICatalogContext catalogContext) : base(catalogContext)
        {
        }

        public virtual async Task<TEntity> Create(TEntity entity)
        {
            //TODO think about this after the UI is completed
            //_catalogContext.AddCommand(() => collection.InsertOneAsync(product));

            await collection.InsertOneAsync(entity);
            return entity;
        }

        public virtual async Task<bool> Delete(string id)
        {
            //_catalogContext.AddCommand(() => collection.DeleteOneAsync(Builders<Product>.Filter.Eq(p => p.Id, id));

            DeleteResult deleteResult = await collection.DeleteOneAsync(Builders<TEntity>.Filter.Eq(p => p.Id, id));
            return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
        }

        public virtual async Task<bool> Update(TEntity entity)
        {
            //_catalogContext.AddCommand(() => collection.ReplaceOneAsync(p => p.Id == product.Id, product));

            var updateResult = await collection.ReplaceOneAsync(p => p.Id == entity.Id, entity);
            return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
        }
    }
}
