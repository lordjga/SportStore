using AutoMapper;
using Catalog.Application.Commands;
using Catalog.Application.ViewModels;
using Catalog.Core.Entities;
using Catalog.Core.Specifications;

namespace Catalog.Application.Mappers
{
    public class ProductMappingProfile : Profile
    {
        public ProductMappingProfile()
        {
            CreateMap<ProductBrand, ProductBrandViewModel>().ReverseMap();
            CreateMap<Product, ProductViewModel>().ReverseMap();
            CreateMap<ProductType, ProductTypeViewModel>().ReverseMap();
            CreateMap<Product, CreateProductCommand>().ReverseMap();
            CreateMap<Pagination<Product>, Pagination<ProductViewModel>>().ReverseMap();
        }
    }
}
