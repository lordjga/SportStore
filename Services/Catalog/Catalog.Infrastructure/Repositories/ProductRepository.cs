using Catalog.Core.Core;
using Catalog.Core.Entities;
using Catalog.Core.Repositories;
using Catalog.Core.Specifications;
using Catalog.Infrastructure.Repositories.Base;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Catalog.Infrastructure.Repositories
{
    public class ProductRepository : GrudRepository<Product>, IProductRepository
    {
        public ProductRepository(ICatalogContext catalogContext): base(catalogContext)
        {
        }

        public async Task<Pagination<Product>> GetProductsWithFilter(CatalogSpecsParams catalogSpecsParams)
        {
            var builder = Builders<Product>.Filter;
            var filter = builder.Empty;
            if (!string.IsNullOrEmpty(catalogSpecsParams.Search))
            {
                var searchFilter = builder.Regex(x => x.Name, new BsonRegularExpression(catalogSpecsParams.Search));
                filter &= searchFilter;
            }
            if (!string.IsNullOrEmpty(catalogSpecsParams.BrandId))
            {
                var brandFilter = builder.Eq(x => x.Brands.Id, catalogSpecsParams.BrandId);
                filter &= brandFilter;
            }
            if (!string.IsNullOrEmpty(catalogSpecsParams.TypeId))
            {
                var typeFilter = builder.Eq(x => x.Types.Id, catalogSpecsParams.TypeId);
                filter &= typeFilter;
            }

            return new Pagination<Product>
            {
                PageSize = catalogSpecsParams.PageSize,
                PageIndex = catalogSpecsParams.PageIndex,
                Data = await DataFilter(catalogSpecsParams, filter),
                Count = await collection.CountDocumentsAsync(p =>
                    true) //TODO: Need to check while applying with UI
            };
        }

        public async Task<IEnumerable<Product>> GetProductByName(string name)
        {
            return await collection
                .Find(Builders<Product>.Filter.Eq(p => p.Name, name))
                .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductByBrand(string name)
        {
            return await collection
                .Find(Builders<Product>.Filter.Eq(p => p.Brands.Name, name))
                .ToListAsync();
        }

        private async Task<IReadOnlyList<Product>> DataFilter(CatalogSpecsParams catalogSpecParams, FilterDefinition<Product> filter)
        {
            switch (catalogSpecParams.Sort)
            {
                case "priceAsc":
                    return await collection
                        .Find(filter)
                        .Sort(Builders<Product>.Sort.Ascending("Price"))
                        .Skip(catalogSpecParams.PageSize * (catalogSpecParams.PageIndex - 1))
                        .Limit(catalogSpecParams.PageSize)
                        .ToListAsync();
                case "priceDesc":
                    return await collection
                        .Find(filter)
                        .Sort(Builders<Product>.Sort.Descending("Price"))
                        .Skip(catalogSpecParams.PageSize * (catalogSpecParams.PageIndex - 1))
                        .Limit(catalogSpecParams.PageSize)
                        .ToListAsync();
                default:
                    return await collection
                        .Find(filter)
                        .Sort(Builders<Product>.Sort.Ascending("Name"))
                        .Skip(catalogSpecParams.PageSize * (catalogSpecParams.PageIndex - 1))
                        .Limit(catalogSpecParams.PageSize)
                        .ToListAsync();
            }
        }
    }
}
