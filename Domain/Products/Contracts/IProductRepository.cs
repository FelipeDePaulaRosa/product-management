using System;
using Domain.Contracts;
using System.Threading.Tasks;
using Domain.Products.Entities;

namespace Domain.Products.Contracts
{
    public interface IProductRepository : IRepository<Product, Guid>
    {
        Task<long> GetNextCode();
    }
}