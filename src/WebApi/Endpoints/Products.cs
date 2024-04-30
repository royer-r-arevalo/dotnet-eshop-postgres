﻿using Application.Products.Create;
using Application.Products.Delete;
using Application.Products.Get;
using Application.Products.Update;
using Carter;
using Domain.Products;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.Contracts.Products;

namespace WebApi.Endpoints;

public class Products : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("products/{id:guid}", async (Guid id, ISender sender) =>
        {
            try
            {
                return Results.Ok(
                    await sender.Send(new GetProductQuery(
                        new ProductId(id))));
            }
            catch (ProductNotFoundException e)
            {
                return Results.NotFound(e.Message);
            }
        });

        app.MapPost("products", async (CreateProductCommand command, ISender sender) =>
        {
            await sender.Send(command);
            return Results.Created();
        });

        app.MapPut("products/{id:guid}", async (Guid id, [FromBody] UpdateProductRequest request, ISender sender) =>
        {
            var command = new UpdateProductCommand(
                new ProductId(id),
                request.Name,
                request.Sku,
                request.Currency,
                request.Amount);

            await sender.Send(command);

            return Results.NoContent();
        });

        app.MapDelete("products/{id:guid}", async (Guid id, ISender sender) =>
        {
            try
            {
                await sender.Send(new DeleteProductCommand(id));
                return Results.NoContent();
            }
            catch (ProductNotFoundException e)
            {
                return Results.NotFound(e.Message);
            }
        });
    }
}