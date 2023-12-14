using Catalog.Core.Core;
using Catalog.Core.Entities;
using Catalog.Core.Repositories.Base;

namespace Catalog.Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ICatalogContext _context;

        public UnitOfWork(ICatalogContext context)
        {
            _context = context;
        }

        public async Task<bool> Commit()
        {
            var changeAmount = await _context.SaveChanges();

            return changeAmount > 0;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
