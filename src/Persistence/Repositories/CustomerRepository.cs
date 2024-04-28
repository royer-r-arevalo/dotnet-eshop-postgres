using Domain.Customers;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;

public sealed class CustomerRepository(
    ApplicationDbContext context) : ICustomerRepository
{
    private readonly ApplicationDbContext _context = context;

    public async Task<Customer?> GetByIdAsync(CustomerId id)
    {
        return await _context.Customers
            .SingleOrDefaultAsync(
                customer => customer.Id == id);
    }
}