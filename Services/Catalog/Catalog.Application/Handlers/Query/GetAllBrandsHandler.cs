using AutoMapper;
using Catalog.Application.Mappers;
using Catalog.Application.Queries;
using Catalog.Application.ViewModels;
using Catalog.Core.Entities;
using Catalog.Core.Repositories;
using MediatR;

namespace Catalog.Application.Handlers.Query
{
    public class GetAllBrandsHandler : IRequestHandler<GetAllBrandsQuery, IList<ProductBrandViewModel>>
    {
        private readonly IBrandRepository _brandRepository;

        public GetAllBrandsHandler(IBrandRepository brandRepository)
        {
            _brandRepository = brandRepository;
        }

        public async Task<IList<ProductBrandViewModel>> Handle(GetAllBrandsQuery request, CancellationToken cancellationToken)
        {
            var brandList = await _brandRepository.GetAll();
            var brandResponseList = ProductMapper.Mapper.Map<IList<ProductBrandViewModel>>(brandList);
            return brandResponseList;
        }
    }
}
