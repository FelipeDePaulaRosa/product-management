using System;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace UnitTest.TestHelpers.InMemoryDatabaseHelpers
{
    public class ProductDbContextInMemoryFactory
    {
        public static ProductDbContext CreateDbContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ProductDbContext>()
                .UseInMemoryDatabase($"InMemoryDb{Guid.NewGuid()}");
            
            return new ProductDbContext(optionsBuilder.Options);
        }
    }
}