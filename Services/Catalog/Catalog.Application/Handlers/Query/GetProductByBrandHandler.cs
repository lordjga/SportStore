using Catalog.Application.Mappers;
using Catalog.Application.Queries;
using Catalog.Application.ViewModels;
using Catalog.Core.Repositories;
using MediatR;

namespace Catalog.Application.Handlers.Query;

public class GetProductByBrandHandler : IRequestHandler<GetProductByBrandQuery, IList<ProductViewModel>>
{
    private readonly IProductRepository _productRepository;

    public GetProductByBrandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }
    public async Task<IList<ProductViewModel>> Handle(GetProductByBrandQuery request, CancellationToken cancellationToken)
    {
        var productList = await _productRepository.GetProductByBrand(request.Brandname);
        var productResponseList = ProductMapper.Mapper.Map<IList<ProductViewModel>>(productList);
        return productResponseList;
    }
}