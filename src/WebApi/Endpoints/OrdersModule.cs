using Application.Orders.Commands.Create;
using Application.Orders.Queries.GetOrderSummary;
using Carter;
using MediatR;
using WebApi.Contracts.Orders;

namespace WebApi.Endpoints;

public class OrdersModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("orders", async (CreateOrderRequest request, ISender sender) =>
        {
            var command = new CreateOrderCommand(request.CustomerId);
            await sender.Send(command);
            return Results.Ok();
        });

        app.MapGet("orders/{id}/summary", async (Guid id, ISender sender) =>
        {
            var query = new GetOrderSummaryQuery(id);
            return Results.Ok(await sender.Send(query));
        });
    }
}
