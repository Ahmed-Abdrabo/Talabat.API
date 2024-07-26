using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Order_Aggregate;

namespace Talabat.Repository.Identity.Config
{
    internal class OrderItemConfigurations : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.OwnsOne(O => O.Product, Product => Product.WithOwner());
            builder.HasOne(oi => oi.Order)
            .WithMany(o => o.Items)
               .HasForeignKey(oi => oi.OrderId)
            .OnDelete(DeleteBehavior.SetNull);
            builder.Property(O => O.Price)
                .HasColumnType("decimal(18, 2)");    
        }
    }
}
