using Catalog.Application.Mappers;
using Catalog.Application.Queries;
using Catalog.Application.ViewModels;
using Catalog.Core.Repositories;
using MediatR;

namespace Catalog.Application.Handlers.Query;

public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductViewModel>
{
    private readonly IProductRepository _productRepository;

    public GetProductByIdQueryHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }
    public async Task<ProductViewModel> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetById(request.Id);
        var productResponse = ProductMapper.Mapper.Map<ProductViewModel>(product);
        return productResponse;
    }
}