using MediatR;

namespace Application.Products.Delete;

public record DeleteProductCommand(Guid ProductId) : IRequest;