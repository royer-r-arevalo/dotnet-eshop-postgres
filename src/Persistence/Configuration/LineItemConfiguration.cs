using Domain.Orders;
using Domain.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

internal sealed class LineItemConfiguration : IEntityTypeConfiguration<LineItem>
{
    public void Configure(EntityTypeBuilder<LineItem> builder)
    {
        builder.HasKey(lineItem => lineItem.Id);

        builder.Property(lineItem => lineItem.Id)
            .HasConversion(
                lineItemId => lineItemId.Value,
                value => new LineItemId(value));

        builder.HasOne<Product>()
            .WithMany()
            .HasForeignKey(lineItem => lineItem.ProductId);

        builder.OwnsOne(lineItem => lineItem.Price);
    }
}