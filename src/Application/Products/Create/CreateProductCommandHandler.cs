using Application.Data;
using Domain.Products;
using MediatR;

namespace Application.Products.Create;

internal sealed class CreateProductCommandHandler(
    IProductRepository productRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<CreateProductCommand>
{
    private readonly IProductRepository _productRepository = productRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task Handle(
        CreateProductCommand request, 
        CancellationToken cancellationToken)
    {
        var product = new Product(
            new ProductId(Guid.NewGuid()),
            request.Name,
            new Money(request.Currency, request.Amount),
            Sku.Create(request.Sku)!);

        _productRepository.Add(product);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}