using Catalog.Application.Mappers;
using Catalog.Application.Queries;
using Catalog.Application.ViewModels;
using Catalog.Core.Core;
using Catalog.Core.Repositories;
using MediatR;

namespace Catalog.Application.Handlers.Query
{
    public class GetAllProductsHandler : IRequestHandler<GetAllProductsQuery, IList<ProductViewModel>>
    {
        private readonly IProductRepository _productRepository;

        public GetAllProductsHandler(IProductRepository productRepository, IBrandRepository brandRepository)
        {
            _productRepository = productRepository;
        }


        public async Task<IList<ProductViewModel>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var productList = await _productRepository.GetAll();
            var productResponseList = ProductMapper.Mapper.Map<IList<ProductViewModel>>(productList);
            return productResponseList;
        }
    }
}
