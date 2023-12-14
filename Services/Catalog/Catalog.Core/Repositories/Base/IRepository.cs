using Catalog.Core.Entities;

namespace Catalog.Core.Repositories.Base
{
    public interface IRepository<TEntity> : IDisposable where TEntity : BaseEntity
    {
        Task<IEnumerable<TEntity>> GetAll();

        Task<TEntity> GetById(string id);
    }
}
