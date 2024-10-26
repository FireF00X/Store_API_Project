using Talabat.Core.Entities.OrderEntities;

namespace Talabat.API.DTOs.OrdersDtos
{
    public class OrderItemDto
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string PictureUrl { get; set; }
        public decimal Price { get; set; }
        public int Quentity { get; set; }
    }
}