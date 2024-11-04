using System.ComponentModel.DataAnnotations;

namespace Talabat.API.DTOs
{
    public class CustomerBasketDto
    {
        [Required]
        public string Id { get; set; }
        [Required]
        public List<BasketItemsDto> Items { get; set; }
        public string? PaymentIntentId { get; set; }
        public string? ClintSecret { get; set; }
        public int? DeliveryMethodId { get; set; }
    }
}
