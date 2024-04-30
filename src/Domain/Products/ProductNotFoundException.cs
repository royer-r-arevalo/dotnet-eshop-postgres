namespace Domain.Products;

public sealed class ProductNotFoundException(ProductId id)
    : Exception($"The product with the ID = {id.Value} was not found");