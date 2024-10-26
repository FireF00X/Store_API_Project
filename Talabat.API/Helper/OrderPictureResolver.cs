using AutoMapper;
using Talabat.API.DTOs.OrdersDtos;
using Talabat.Core.Entities.OrderEntities;

namespace Talabat.API.Helper
{
    public class OrderPictureResolver : IValueResolver<OrderItem, OrderItemDto, string>
    {
        private readonly IConfiguration _config;

        public OrderPictureResolver(IConfiguration config)
        {
            _config = config;
        }
        public string Resolve(OrderItem source, OrderItemDto destination, string destMember, ResolutionContext context)
        {
            if(source.Product.PictureUrl is not null)
            {
                return $"{_config.GetSection("ApiBaseUrl").Value}/{source.Product.PictureUrl}";
            }
            return string.Empty ;
        }
    }
}
