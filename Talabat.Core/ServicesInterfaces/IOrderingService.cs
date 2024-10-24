using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.IdentityEntities;
using Talabat.Core.Entities.OrderEntities;

namespace Talabat.Core.ServicesInterfaces
{
    public interface IOrderingService
    {
        Task<IReadOnlyList<Order>> GetAllOrdersForUserAsync(string buyerEmail);
        Task<Order?> CreateOrderAsync(string basketId, string buyerEmail, int deliveryMethod, OrderAddress orderAddress);
        Task<Order> GetOrderForUserAsync(string buyerEmail, int orderId);
    }
}
