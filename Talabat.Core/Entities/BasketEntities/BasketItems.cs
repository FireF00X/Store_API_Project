namespace Talabat.Core.Entities.BasketEntities
{
    public class BasketItems
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string PictureUrl { get; set; }
        public decimal Price { get; set; }
        public string Brand {  get; set; }
        public string Category { get; set; }
        public int Quentity {  get; set; }
    }
}