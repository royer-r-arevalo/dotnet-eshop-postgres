using Application.Orders.Commands.Create;
using Carter;
using MediatR;
using WebApi.Contracts.Orders;

namespace WebApi.Endpoints;

public class OrdersModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/orders", async (CreateOrderRequest request, ISender sender) =>
        {
            var command = new CreateOrderCommand(request.CustomerId);
            await sender.Send(command);
            return Results.Ok();
        });
    }
}
