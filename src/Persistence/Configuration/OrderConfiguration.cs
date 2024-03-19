using Domain.Customers;
using Domain.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

internal sealed class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(order => order.Id);

        builder.Property(order => order.Id)
            .HasConversion(
                orderId => orderId.Value,
                value => new OrderId(value));

        builder.HasOne<Customer>()
            .WithMany()
            .HasForeignKey(order => order.CustomerId)
            .IsRequired();

        builder.HasMany(order => order.LineItems)
            .WithOne()
            .HasForeignKey(lineItem => lineItem.OrderId);
    }
}