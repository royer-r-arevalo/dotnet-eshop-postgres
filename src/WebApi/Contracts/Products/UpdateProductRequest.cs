namespace WebApi.Contracts.Products;

public sealed record UpdateProductRequest(
    string Name,
    string Sku,
    string Currency,
    decimal Amount);
