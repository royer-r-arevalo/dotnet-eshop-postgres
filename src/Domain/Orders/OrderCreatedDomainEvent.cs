using Domain.Primitives;

namespace Domain.Orders;

public sealed record OrderCreatedDomainEvent(
    Guid Id,
    OrderId OrderId) : DomainEvent(Id);
