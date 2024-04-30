using MediatR;

namespace Application.Products.Create;

public sealed record CreateProductCommand(
    string Name,
    string Sku,
    string Currency,
    decimal Amount) : IRequest;