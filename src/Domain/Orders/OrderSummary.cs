namespace Domain.Orders;

public sealed record OrderSummary(
    Guid Id,
    Guid CustomerId,
    decimal TotalPrice);
