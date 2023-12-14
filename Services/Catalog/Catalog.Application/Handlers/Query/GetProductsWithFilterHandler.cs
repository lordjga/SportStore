using Catalog.Application.Mappers;
using Catalog.Application.Queries;
using Catalog.Application.ViewModels;
using Catalog.Core.Repositories;
using Catalog.Core.Specifications;
using MediatR;

namespace Catalog.Application.Handlers.Query
{
    public class GetProductsWithFilterHandler : IRequestHandler<GetProductsWithFilterQuery, Pagination<ProductViewModel>>
    {
        private readonly IProductRepository _productRepository;

        public GetProductsWithFilterHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<Pagination<ProductViewModel>> Handle(GetProductsWithFilterQuery request, CancellationToken cancellationToken)
        {
            var productPage = await _productRepository.GetProductsWithFilter(request.CatalogSpecParams);
            var response = ProductMapper.Mapper.Map<Pagination<ProductViewModel>>(productPage);
            return response;
        }
    }
}
