using Catalog.Application.ViewModels;
using Catalog.Core.Specifications;
using MediatR;

namespace Catalog.Application.Queries
{
    public class GetProductsWithFilterQuery: IRequest<Pagination<ProductViewModel>>
    {
        public CatalogSpecsParams CatalogSpecParams { get; set; }

        public GetProductsWithFilterQuery(CatalogSpecsParams catalogSpecParams)
        {
            CatalogSpecParams = catalogSpecParams;
        }
    }
}
