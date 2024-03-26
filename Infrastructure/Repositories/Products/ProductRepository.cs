using System;
using Domain.Products.Contracts;
using Domain.Products.Entities;
using Infrastructure.Contexts;

namespace Infrastructure.Repositories.Products
{
    public class ProductRepository : Repository<Product, Guid>, IProductRepository
    {
        public ProductRepository(ProductDbContext context) : base(context)
        {
        }
    }
}