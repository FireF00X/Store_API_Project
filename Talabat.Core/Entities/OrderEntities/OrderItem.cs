using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entities.OrderEntities
{
    public class OrderItem : BaseModel
    {
        public OrderItem(ProductItemOrdered product, decimal price, int quentity)
        {
            Product = product;
            Price = price;
            Quentity = quentity;
        }
        public OrderItem()
        {
            
        }
        public ProductItemOrdered Product { get; set; }
        public decimal Price { get; set; }
        public int Quentity { get; set; }
    }
}
