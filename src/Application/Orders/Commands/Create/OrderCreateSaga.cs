﻿using Rebus.Bus;
using Rebus.Handlers;
using Rebus.Sagas;
using System.Data;

namespace Application.Orders.Commands.Create;

public class OrderCreateSaga (
    IBus bus): Saga<OrderCreateSagaData>,
    IAmInitiatedBy<OrderCreatedEvent>,
    IHandleMessages<OrderConfirmationEmailSent>,
    IHandleMessages<OrderPaymentRequestSent>
{
    private readonly IBus _bus = bus;

    protected override void CorrelateMessages(ICorrelationConfig<OrderCreateSagaData> config)
    {
        config.Correlate<OrderCreatedEvent>(m => m.OrderId, s => s.OrderId);
        config.Correlate<OrderConfirmationEmailSent>(m => m.OrderId, s => s.OrderId);
        config.Correlate<OrderPaymentRequestSent>(m => m.OrderId, s => s.OrderId);
    }

    public async Task Handle(OrderCreatedEvent message)
    {
        if (!IsNew)
        {
            return;
        }
        await _bus.Send(new SendOrderConfirmationEmail(message.OrderId));
    }

    public async Task Handle(OrderConfirmationEmailSent message)
    {
        Data.ConfirmationEmailSent = true;
        await _bus.Send(new CreateOrderPaymentRequest(message.OrderId));
    }

    public Task Handle(OrderPaymentRequestSent message)
    {
        Data.PaymentRequestSent = true;
        MarkAsComplete();
        return Task.CompletedTask;
    }
}
