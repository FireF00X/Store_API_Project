using Talabat.Core.Entities.OrderEntities;

namespace Talabat.API.DTOs.OrdersDtos
{
    public class OrderDto
    {
        public string BuyerEmail { get; set; }
        public int DeliveryMethodId { get; set; }
        public string BasketId { get; set; }
        public OrderAddressDto ShippingAddress { get; set; }
    }
}
