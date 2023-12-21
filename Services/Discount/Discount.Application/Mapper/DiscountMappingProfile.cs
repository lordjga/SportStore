using AutoMapper;
using Discount.Application.Commands;
using Discount.Core.Entities;
using Discount.Grpc.Protos;

namespace Discount.Application.Mapper
{
    public class DiscountMappingProfile: Profile
    {
        public DiscountMappingProfile()
        {
            CreateMap<Coupon, CouponModel>().ReverseMap();
            CreateMap<Coupon, CreateDiscountCommand>().ReverseMap();
            CreateMap<Coupon, UpdateDiscountCommand>().ReverseMap();

            CreateMap<CreateDiscountCommand, CreateDiscountRequest>().ForMember(x => x.Coupon, y => y.MapFrom(z => z)).ReverseMap();
            CreateMap<CreateDiscountCommand, CouponModel>().ReverseMap();

            CreateMap<UpdateDiscountCommand, UpdateDiscountRequest>().ForMember(x => x.Coupon, y => y.MapFrom(z => z)).ReverseMap();
            CreateMap<UpdateDiscountCommand, CouponModel>().ReverseMap();
        }
    }
}
