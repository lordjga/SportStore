using Catalog.Application.Commands;
using Catalog.Application.Mappers;
using Catalog.Application.ViewModels;
using Catalog.Core.Core;
using Catalog.Core.Entities;
using Catalog.Core.Repositories;
using MediatR;

namespace Catalog.Application.Handlers.Command;

public class CreateProductHandler : IRequestHandler<CreateProductCommand, ProductViewModel>
{
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateProductHandler(IProductRepository productRepository, IUnitOfWork unitOfWork)
    {
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
    }
    public async Task<ProductViewModel> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var productEntity = ProductMapper.Mapper.Map<Product>(request);
        if (productEntity is null)
        {
            throw new ApplicationException("There is an issue with mapping while creating new product");
        }

        //TODO think about this after the UI is completed
        //_productRepository.CreateProduct(productEntity);
        //await _unitOfWork.Commit();

        var newProduct = await _productRepository.Create(productEntity);
        var productResponse = ProductMapper.Mapper.Map<ProductViewModel>(newProduct);
        return productResponse;
    }
}