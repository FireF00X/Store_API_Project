using System.ComponentModel.DataAnnotations;

namespace Talabat.API.DTOs
{
    public class BasketItemsDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string ProductName { get; set; }
        [Required]
        public string PictureUrl { get; set; }
        [Range(.01d,double.MaxValue,ErrorMessage ="The Price Must be more than 1$")]
        public decimal Price { get; set; }
        [Required]
        public string Brand { get; set; }
        [Required]
        public string Category { get; set; }
        [ Range(1, int.MaxValue, ErrorMessage = "The Quentity Must be one Or more")]
        public int Quentity { get; set; }
    }
}