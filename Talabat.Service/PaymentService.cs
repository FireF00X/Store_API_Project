using Microsoft.Extensions.Configuration;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Entities.BasketEntities;
using Talabat.Core.Entities.OrderEntities;
using Talabat.Core.RepositoryInterfaces;
using Talabat.Core.ServicesInterfaces;
using Product = Talabat.Core.Entities.Product;

namespace Talabat.Service
{
    public class PaymentService : IPaymentService
    {
        private readonly IConfiguration _configuration;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRedisRepository _redisRepository;

        public PaymentService(IConfiguration configuration,IUnitOfWork unitOfWork,IRedisRepository redisRepository)
        {
            _configuration = configuration;
            _unitOfWork = unitOfWork;
            _redisRepository = redisRepository;
        }
        public async Task<CustomerBasket?> CreateOrUpdatePaymentIntentAsync(string basaketId)
        {
            StripeConfiguration.ApiKey = _configuration["StripService:Secretkey"];
            var basket = await _redisRepository.GetByIdAsync(basaketId);
            if (basket == null) return null;

            var deliveryCost = 0M;
            if(basket.DeliveryMethodId.HasValue)
            {
                var deliveryMethodId = await _unitOfWork.CreateRepo<DeliveryMethod>().GetByIdAsync(basket.DeliveryMethodId.Value);
                deliveryCost = deliveryMethodId.Cost;
            }
            // Get Real Price 
            if(basket.Items.Count > 0)
            {
                foreach (var item in basket.Items)
                {
                    var product = await _unitOfWork.CreateRepo<Product>().GetByIdAsync(item.Id);
                    if (item.Price != product.Price)
                        item.Price = product.Price;
                }
            }
            var subTotal = basket.Items.Sum(i => i.Price * i.Quentity);

            PaymentIntent paymentIntent;
            var service = new PaymentIntentService();

            if(basket.PaymentIntentId == null)
            {
                var options = new PaymentIntentCreateOptions()
                {
                    Amount = (long)subTotal*100 + (long)deliveryCost *100,
                    Currency ="usd",
                    PaymentMethodTypes = new List<string>() {"card"}
                };
                paymentIntent = await service.CreateAsync(options);
                basket.PaymentIntentId = paymentIntent.Id;
                basket.ClintSecret = paymentIntent.ClientSecret;
            }
            else
            {
                var options = new PaymentIntentUpdateOptions()
                {
                    Amount = (long)subTotal * 100 + (long)deliveryCost * 100
                };
                paymentIntent = await service.UpdateAsync(basket.PaymentIntentId ,options);
                basket.PaymentIntentId = paymentIntent.Id;
                basket.ClintSecret = paymentIntent.ClientSecret;
            }
            await _redisRepository.UpdateBasketAsync(basket);
            return basket;
        }
    }
}
