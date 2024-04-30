using Domain.Products;
using MediatR;

namespace Application.Products.Update;

public sealed record UpdateProductCommand(
    ProductId ProductId,
    string Name,
    string Sku,
    string Currency,
    decimal Amount) : IRequest;

