using Domain.Customers;
using MediatR;

namespace Application.Orders.Commands.Create;

public sealed record CreateOrderCommand(
    Guid CustomerId) : IRequest;
