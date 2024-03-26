using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Domain.Products.Contracts;
using Domain.Products.Entities;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Products
{
    public class ProductRepository : Repository<Product, Guid>, IProductRepository
    {
        public ProductRepository(ProductDbContext context) : base(context)
        {
        }
        
        public async Task<long> GetNextCode()
        {
            return await DbSetNt
                .OrderBy(x => x.Code)
                .Select(x => x.Code)
                .LastOrDefaultAsync() + 1;
        }

        public async Task<Product> GetByIdOrDefaultAsync(Guid id)
        {
            return await DbSet.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}