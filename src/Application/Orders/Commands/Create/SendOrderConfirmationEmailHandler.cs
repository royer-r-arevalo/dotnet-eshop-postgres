using Microsoft.Extensions.Logging;
using Rebus.Bus;
using Rebus.Handlers;

namespace Application.Orders.Commands.Create;

internal sealed class SendOrderConfirmationEmailHandler(
    ILogger<SendOrderConfirmationEmailHandler> logger,
    IBus bus): IHandleMessages<SendOrderConfirmationEmail>
{
    private readonly ILogger<SendOrderConfirmationEmailHandler> _logger = logger;
    private readonly IBus _bus = bus;

    public async Task Handle(SendOrderConfirmationEmail message)
    {
        _logger.LogInformation("Sending order confirmation {@OrderId}", message.OrderId);
        await Task.Delay(2000);
        _logger.LogInformation("Order confirmation sent {@OrderId}", message.OrderId);
        await _bus.Send(new OrderConfirmationEmailSent(message.OrderId));
    }
}
