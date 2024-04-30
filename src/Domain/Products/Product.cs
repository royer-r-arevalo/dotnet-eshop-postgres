namespace Domain.Products;

public class Product
{
    public Product(
        ProductId productId,
        string name,
        Money price,
        Sku sku)
    {
        Id = productId;
        Name = name; 
        Price = price;
        Sku = sku;
    }

    public ProductId Id { get; private set; }

    public string Name { get; private set; } = string.Empty;

    public Money Price { get; private set; }

    public Sku Sku { get; private set; }

    public void Update(string name, Money price, Sku sku)
    {
        Name = name; 
        Price = price; 
        Sku = sku;
    }
}