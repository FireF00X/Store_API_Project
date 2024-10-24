using AutoMapper;
using Talabat.API.DTOs;
using Talabat.API.DTOs.OrdersDtos;
using Talabat.Core.Entities;
using Talabat.Core.Entities.BasketEntities;
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
            CreateMap<BasketItemsDto, BasketItems>();
            CreateMap<CustomerBasketDto, CustomerBasket>();
            CreateMap<OrderAddressDto,OrderAddress>();
        }
    }
}
