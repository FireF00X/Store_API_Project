using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.OrderEntities;

namespace Talabat.Repository.Data.EntityConfiguration.OrderEntitiesConfigurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.Property(o=>o.Status)
                    .HasConversion(s=>s.ToString()
                    ,s=> (OrderStatus) Enum.Parse(typeof(OrderStatus),s));

            builder.OwnsOne(o => o.ShippingAddress, a => a.WithOwner());

            builder.Property(o => o.SubTotal)
                    .HasColumnType("decimal(18,2)");

            builder.HasOne(o => o.DeliveryMethod)
                    .WithMany()
                    .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
