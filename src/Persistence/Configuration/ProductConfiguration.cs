using Domain.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

internal sealed class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(product => product.Id);

        builder.Property(product => product.Id)
            .HasConversion(
                productId => productId.Value,
                value => new ProductId(value));

        builder.Property(product => product.Sku)
            .HasConversion(
                sku => sku.Value,
                value => Sku.Create(value)!);

        builder.OwnsOne(product => product.Price, priceBuilder =>
        {
            priceBuilder.Property(money => money.Currency)
                .HasMaxLength(3);
        });
    }
}