using MediatR;
using Microsoft.Extensions.Logging;
using Rebus.Bus;
using Rebus.Handlers;
using System.Threading;

namespace Application.Orders.Commands.Create;

internal sealed class CreatePaymentRequestHandler(
    ILogger<CreatePaymentRequestHandler> logger,
    IBus bus) : IHandleMessages<CreateOrderPaymentRequest>
{
    private readonly ILogger<CreatePaymentRequestHandler> _logger = logger;
    private readonly IBus _bus = bus;
  
    public async Task Handle(CreateOrderPaymentRequest message)
    {
        _logger.LogInformation("Starting payment request {@OrderId}", message.OrderId);
        await Task.Delay(2000);
        _logger.LogInformation("Payment request started {@OrderId}", message.OrderId);
        await _bus.Send(new OrderPaymentRequestSent(message.OrderId));
    }
}
