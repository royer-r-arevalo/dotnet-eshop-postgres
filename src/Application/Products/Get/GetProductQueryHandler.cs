﻿using Application.Data;
using Domain.Products;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Products.Get;

internal sealed class GetProductQueryHandler(
    IApplicationDbContext context) : IRequestHandler<GetProductQuery, ProductResponse>
{
    private readonly IApplicationDbContext _context = context;

    public async Task<ProductResponse> Handle(
        GetProductQuery request, 
        CancellationToken cancellationToken)
    {
        var product = await _context
            .Products
            .Where(p => p.Id == request.ProductId)
            .Select(p => new ProductResponse(
                p.Id.Value,
                p.Name,
                p.Sku.Value,
                p.Price.Currency,
                p.Price.Amount))
            .FirstOrDefaultAsync(cancellationToken);

        if (product is null)
        {
            throw new ProductNotFoundException(request.ProductId);
        }

        return product;
    }
}