using Catalog.Core.Core;
using Catalog.Core.Entities;
using Catalog.Core.Repositories;
using Catalog.Infrastructure.Repositories.Base;

namespace Catalog.Infrastructure.Repositories
{
    public class TypeRepository : ReadRepository<ProductType>, ITypeRepository
    {
        public TypeRepository(ICatalogContext catalogContext) : base(catalogContext)
        {
        }
    }
}
