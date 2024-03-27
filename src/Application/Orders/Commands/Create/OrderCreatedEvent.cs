using Domain.Orders;

namespace Application.Orders.Commands.Create;

public sealed record OrderCreatedEvent(Guid OrderId);

public sealed record OrderConfirmationEmailSent(Guid OrderId);

public sealed record OrderPaymentRequestSent(Guid OrderId);

public sealed record SendOrderConfirmationEmail(Guid OrderId);

public sealed record CreateOrderPaymentRequest(Guid OrderId);
