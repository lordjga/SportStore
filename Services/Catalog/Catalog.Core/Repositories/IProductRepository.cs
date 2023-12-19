using Catalog.Core.Entities;
using Catalog.Core.Repositories.Base;
using Catalog.Core.Specifications;

namespace Catalog.Core.Repositories
{
    public interface IProductRepository : ICrudRepository<Product>
    {
        Task<Pagination<Product>> GetProductsWithFilter(CatalogSpecsParams catalogSpecsParams);


        Task<IEnumerable<Product>> GetProductByName(string name);

        Task<IEnumerable<Product>> GetProductByBrand(string name);
    }
}
