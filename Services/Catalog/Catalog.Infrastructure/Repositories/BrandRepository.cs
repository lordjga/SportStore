using Catalog.Core.Core;
using Catalog.Core.Entities;
using Catalog.Core.Repositories;
using Catalog.Infrastructure.Repositories.Base;

namespace Catalog.Infrastructure.Repositories
{
    public class BrandRepository : BaseRepository<ProductBrand>, IBrandRepository
    {
        public BrandRepository(ICatalogContext catalogContext) : base(catalogContext)
        {
        }
    }
}
