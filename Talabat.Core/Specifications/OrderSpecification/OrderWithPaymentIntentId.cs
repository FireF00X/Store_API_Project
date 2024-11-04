using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.OrderEntities;

namespace Talabat.Core.Specifications.OrderSpecification
{
    public class OrderWithPaymentIntentId : BaseSpecifications<Order>
    {
        public OrderWithPaymentIntentId(string paymentIntentId) : base(o => o.PaymentIntentId == paymentIntentId)
        {

        }
    }
}
