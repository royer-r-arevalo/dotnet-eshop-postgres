using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Orders.Commands.Create;

internal sealed class SendOrderConfirmationEventHandler(
    ILogger<SendOrderConfirmationEventHandler> logger): INotificationHandler<OrderCreatedEvent>
{
    private readonly ILogger<SendOrderConfirmationEventHandler> _logger = logger;

    public async Task Handle(
        OrderCreatedEvent notification,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Sending order confirmation {@OrderId}", notification.OrderId);
        await Task.Delay(2000, cancellationToken);
        _logger.LogInformation("Order confirmation sent {@OrderId}", notification.OrderId);

    }
}
