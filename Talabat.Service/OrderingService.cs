using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Entities.IdentityEntities;
using Talabat.Core.Entities.OrderEntities;
using Talabat.Core.RepositoryInterfaces;
using Talabat.Core.ServicesInterfaces;
using Talabat.Core.Specifications.ProductSpecification;

namespace Talabat.Service
{
    public class OrderingService : IOrderingService
    {
        private readonly IRedisRepository _customerBasket;
        private readonly IUnitOfWork _repo;

        public OrderingService(IRedisRepository customerBasket, IUnitOfWork repo)
        {
            _customerBasket = customerBasket;
            _repo = repo;
        }
        public async Task<Order?> CreateOrderAsync(string basketId, string buyerEmail, int deliveryMethodId, OrderAddress orderAddress)
        {
            // 1. Get Basket From BasketRepo
            var customerBasket = await _customerBasket.GetByIdAsync(basketId);

            // 2. Get Selected Items At Basket From Product Repo
            var orderItems = new List<OrderItem>();
            foreach (var item in customerBasket.Items)
            {
                var product = await _repo.CreateRepo<Product>().GetByIdAsync(item.Id);
                var productOrderedItem = new ProductItemOrdered(item.Id, product.Name, product.PictureUrl);
                var orderedItem = new OrderItem(productOrderedItem, product.Price, item.Quentity);
                orderItems.Add(orderedItem);
            }

            // 3. Calculate SubTotal
            var subTotal = orderItems.Sum(o => o.Price * o.Quentity);

            // 4. Get DeliveryMethod Cost
            var deliveryMethodChosen = await _repo.CreateRepo<DeliveryMethod>().GetByIdAsync(deliveryMethodId);

            // 5. Create Order and add to db
            var order = new Order(buyerEmail,orderAddress,deliveryMethodChosen,orderItems,subTotal);
            await _repo.CreateRepo<Order>().AddAsync(order);

            // 6. Save Changes 
            var result = await _repo.CompleteAsync();
            if (result <= 0) return null;
            return order;
        }

        public Task<IReadOnlyList<Order>> GetAllOrdersForUserAsync(string buyerEmail)
        {
            throw new NotImplementedException();
        }

        public Task<Order> GetOrderForUserAsync(string buyerEmail, int orderId)
        {
            throw new NotImplementedException();
        }
    }
}
