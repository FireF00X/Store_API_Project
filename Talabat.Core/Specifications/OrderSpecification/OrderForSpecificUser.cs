using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.OrderEntities;

namespace Talabat.Core.Specifications.OrderSpecification
{
    public class OrderForSpecificUser : BaseSpecifications<Order>
    {
        public OrderForSpecificUser(string buyerEmail):base(o=>o.BuyerEmail == buyerEmail)
        {
            Includes.Add(o => o.DeliveryMethod);
            Includes.Add(o => o.Items);
            AddOrderByAsc(o => o.OrderDate);
        }
        public OrderForSpecificUser(string buyerEmail,int orderId):base(o=>o.Id == orderId && o.BuyerEmail == buyerEmail)
        {
            Includes.Add(o => o.DeliveryMethod);
            Includes.Add(o => o.Items);
        }
    }
}
