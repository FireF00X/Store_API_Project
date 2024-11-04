﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entities.OrderEntities
{
    public class Order : BaseModel
    {
        public Order(string buyerEmail,  OrderAddress shippingAddress, DeliveryMethod deliveryMethod, ICollection<OrderItem> items, decimal subTotal,string paymentIntentId)
        {
            BuyerEmail = buyerEmail;
            ShippingAddress = shippingAddress;
            DeliveryMethod = deliveryMethod;
            Items = items;
            SubTotal = subTotal;
            PaymentIntentId = paymentIntentId;
        }
        public Order()
        {

        }

        public string BuyerEmail { get; set; }
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.UtcNow;
        public OrderStatus Status { get; set; }
        public OrderAddress ShippingAddress{ get; set; }
        public int? DeliveryMethodId { get; set; }
        public DeliveryMethod? DeliveryMethod { get; set; }
        public ICollection<OrderItem> Items { get; set; } = new HashSet<OrderItem>();
        public decimal SubTotal { get; set; }
        public decimal GetTotal()
            => SubTotal+DeliveryMethod.Cost;
        public string PaymentIntentId { get; set; } = string.Empty;

    }
}
