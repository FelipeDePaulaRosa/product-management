using System;
using Domain.Contracts;
using Domain.Products.Entities;

namespace Domain.Products.Contracts
{
    public interface IProductRepository : IRepository<Product, Guid>
    {
        
    }
}