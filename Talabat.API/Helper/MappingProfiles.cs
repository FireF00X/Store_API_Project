using AutoMapper;
using Talabat.API.DTOs;
using Talabat.API.DTOs.AccountUsersDto;
using Talabat.API.DTOs.OrdersDtos;
using Talabat.Core.Entities;
using Talabat.Core.Entities.BasketEntities;
using Talabat.Core.Entities.IdentityEntities;
using Talabat.Core.Entities.OrderEntities;

namespace Talabat.API.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product,ProductDto>()
                .ForMember(d=>d.ProductBrand,o=>o.MapFrom(s=>s.ProductBrand.Name))
                .ForMember(d=>d.ProductType,o=>o.MapFrom(s=>s.ProductType.Name))
                .ForMember(d=>d.PictureUrl,o=>o.MapFrom<ProductPictureUrlResolver>());
            CreateMap<BasketItemsDto, BasketItems>().ReverseMap();
            CreateMap<CustomerBasketDto, CustomerBasket>().ReverseMap();
            CreateMap<OrderAddressDto,OrderAddress>();

            CreateMap<Order, OrderToReturnDto>()
                .ForMember(d=>d.DeliveryMethodCost,o=>o.MapFrom(s=>s.DeliveryMethod.Cost))
                .ForMember(d=>d.DeliveryMethod,o=>o.MapFrom(s=>s.DeliveryMethod.ShortName));

            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(d => d.ProductId, o => o.MapFrom(s => s.Product.ProductId))
                .ForMember(d => d.ProductName, o => o.MapFrom(s => s.Product.ProductName))
                .ForMember(d => d.PictureUrl, o => o.MapFrom(s => s.Product.PictureUrl))
                .ForMember(d => d.PictureUrl, o => o.MapFrom<OrderPictureResolver>());

            CreateMap<AddressDto,Address>().ReverseMap();
        }
    }
}
