using Application.Orders.Commands.Create;
using Application.Orders.Queries.GetOrderSummary;
using Carter;
using MediatR;
using Microsoft.AspNetCore.RateLimiting;
using WebApi.Contracts.Orders;

namespace WebApi.Endpoints;

[EnableRateLimiting("sliding")]
public class Orders : ICarterModule
{
    [DisableRateLimiting]
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("orders", async (CreateOrderRequest request, ISender sender) =>
        {
            var command = new CreateOrderCommand(request.CustomerId);
            await sender.Send(command);
            return Results.Ok();
        }).DisableRateLimiting();

        app.MapGet("orders/{id}/summary", async (Guid id, ISender sender) =>
        {
            var query = new GetOrderSummaryQuery(id);
            return Results.Ok(await sender.Send(query));
        }).RequireRateLimiting("fixed");
    }
}
