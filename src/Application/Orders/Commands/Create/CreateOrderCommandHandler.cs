using Application.Data;
using Domain.Customers;
using Domain.Orders;
using MediatR;
using Rebus.Bus;

namespace Application.Orders.Commands.Create;

internal sealed class CreateOrderCommandHandler(
    ICustomerRepository customerRepository,
    IOrderRepository orderRepository,
    IOrderSummaryRepository orderSummaryRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<CreateOrderCommand>
{
    private readonly ICustomerRepository _customerRepository = customerRepository;
    private readonly IOrderRepository _orderRepository = orderRepository;
    private readonly IOrderSummaryRepository _orderSummaryRepository = orderSummaryRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task Handle(
        CreateOrderCommand request,
        CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.GetByIdAsync(
            new CustomerId(request.CustomerId));

        if (customer is null)
        {
            return;
        }

        var order = Order.Create(customer.Id);

        _orderRepository.Add(order);
        _orderSummaryRepository.Add(new OrderSummary(
            order.Id.Value,
            order.CustomerId.Value,
            0));

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
