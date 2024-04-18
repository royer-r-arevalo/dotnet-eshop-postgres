using Domain.Primitives;

namespace Domain.Orders;

public sealed record LineItemRemovedDomainEvent(
    Guid Id,
    OrderId OrderId,
    LineItemId LineItemId) : DomainEvent(Id);
