using Catalog.Core.Entities;

namespace Catalog.Core.Repositories.Base
{
    public interface ICrudRepository<TEntity> : IReadRepository<TEntity> where TEntity : BaseEntity
    {
        Task<TEntity> Create(TEntity entity);

        Task<bool> Update(TEntity entity);

        Task<bool> Delete(string id);
    }
}
