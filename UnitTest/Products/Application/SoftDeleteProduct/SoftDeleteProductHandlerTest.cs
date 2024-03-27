using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Products.SoftDeleteProduct;
using Domain.Exceptions;
using Domain.Products.Entities;
using FluentAssertions;
using NUnit.Framework;
using UnitTest.TestHelpers.InMemoryDatabaseHelpers;

namespace UnitTest.Products.Application.SoftDeleteProduct
{
    public class SoftDeleteProductHandlerTest
    {
        [Test]
        public async Task SoftDeleteProductTest()
        {
            var inMemoryRepositoryFactory = InMemoryRepositoryFactory.GetInstance();
            var productRepository = inMemoryRepositoryFactory.ProductRepository;

            var product = new Product("Description",
                DateTime.Now.Date,
                DateTime.Now.AddMonths(1).Date,
                "001",
                "Main Supplier",
                "68.159.892/0001-01");
            
            await productRepository.CreateAsync(product);
            
            var handler = new SoftDeleteProductHandler(productRepository);
            var request = new SoftDeleteProductRequest(product.Id);

            await handler.Handle(request, new CancellationToken());
            
            var productDeleted = await productRepository.GetByIdOrDefaultAsync(request.Id);
            productDeleted.IsActive.Should().BeFalse();
            productDeleted.Id.Should().Be(product.Id);
            productDeleted.Code.Should().Be(product.Code);
            productDeleted.Description.Should().Be(product.Description);
            productDeleted.ManufactureDate.Should().Be(product.ManufactureDate);
            productDeleted.DueDate.Should().Be(product.DueDate);
            productDeleted.Supplier.Code.Should().Be(product.Supplier.Code);
            productDeleted.Supplier.Description.Should().Be(product.Supplier.Description);
            productDeleted.Supplier.Cnpj.Should().Be(product.Supplier.Cnpj);
        }
        
        [Test]
        public async Task SoftDeleteProductNotFoundTest()
        {
            var inMemoryRepositoryFactory = InMemoryRepositoryFactory.GetInstance();
            var productRepository = inMemoryRepositoryFactory.ProductRepository;
            
            var handler = new SoftDeleteProductHandler(productRepository);
            var request = new SoftDeleteProductRequest(Guid.NewGuid());

            Func<Task> act = async () => await handler.Handle(request, new CancellationToken());
            await act.Should().ThrowAsync<ApiNotFoundException>();
        }
    }
}