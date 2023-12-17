using AutoMapper;
using Basket.Application.ViewModels;
using Basket.Core.Entities;

namespace Basket.Application.Mappers
{
    public class BasketMapperProfile : Profile
    {
        public BasketMapperProfile()
        {
            CreateMap<ShoppingCart, ShoppingCartResponse>();
            CreateMap<ShoppingCartItem, ShoppingCartItemResponse>();
        }
    }
}
