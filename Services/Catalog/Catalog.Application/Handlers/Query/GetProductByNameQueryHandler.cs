using Catalog.Application.Mappers;
using Catalog.Application.Queries;
using Catalog.Application.ViewModels;
using Catalog.Core.Repositories;
using MediatR;

namespace Catalog.Application.Handlers.Query;

public class GetProductByNameQueryHandler : IRequestHandler<GetProductByNameQuery, IList<ProductViewModel>>
{
    private readonly IProductRepository _productRepository;

    public GetProductByNameQueryHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }
    public async Task<IList<ProductViewModel>> Handle(GetProductByNameQuery request, CancellationToken cancellationToken)
    {
        var productList = await _productRepository.GetProductByName(request.Name);
        var productResponseList = ProductMapper.Mapper.Map<IList<ProductViewModel>>(productList);
        return productResponseList;
    }
}