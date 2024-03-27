using Domain.Customers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

internal sealed class CostumerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.ToTable("Customers");

        builder.HasKey(customer => customer.Id);

        builder.Property(customer => customer.Id)
            .HasConversion(
                customerId => customerId.Value,
                value => new CustomerId(value));

        builder.Property(customer => customer.Name)
            .HasMaxLength(100);

        builder.Property(customer => customer.Email)
            .HasMaxLength(255);

        builder.HasIndex(customer => customer.Email).IsUnique();
    }
}