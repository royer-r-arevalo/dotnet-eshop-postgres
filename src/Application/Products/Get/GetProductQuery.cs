using Domain.Products;
using MediatR;

namespace Application.Products.Get;

public sealed record GetProductQuery(
    ProductId ProductId) : IRequest<ProductResponse>;

public sealed record ProductResponse(
    Guid Id,
    string Name,
    string Sku,
    string Currency,
    decimal Amount);