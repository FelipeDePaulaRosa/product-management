using Domain.Products.Contracts;
using Infrastructure.Contexts;
using Infrastructure.Repositories.Products;

namespace UnitTest.TestHelpers.InMemoryDatabaseHelpers
{
    public class InMemoryRepositoryFactory
    {
        public readonly ProductDbContext ProductDbContext;
        
        private InMemoryRepositoryFactory()
        {
            ProductDbContext = ProductDbContextInMemoryFactory.CreateDbContext();
        }
        
        public static InMemoryRepositoryFactory GetInstance() => new();
        
        public IProductRepository ProductRepository =>
            new ProductRepository(ProductDbContext);
    }
}