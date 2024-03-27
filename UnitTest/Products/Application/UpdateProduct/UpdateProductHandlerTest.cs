using System;
using NUnit.Framework;
using System.Threading;
using FluentAssertions;
using Domain.Exceptions;
using UnitTest.TestHelpers;
using System.Threading.Tasks;
using Domain.Products.Entities;
using Application.Products.UpdateProduct;
using UnitTest.TestHelpers.InMemoryDatabaseHelpers;

namespace UnitTest.Products.Application.UpdateProduct
{
    public class UpdateProductHandlerTest
    {
        [Test]
        public async Task Update_Product_With_Success()
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
            
            var mapper = AutoMapperBuilder.Build();
            var handler = new UpdateProductHandler(productRepository, mapper);
            
            var request = new UpdateProductRequest
            {
                Id = product.Id,
                Description = "Product Description",
                ManufactureDate = DateTime.Now.Date,
                DueDate = DateTime.Now.AddMonths(1).Date,
                SupplierCode = "Supplier Code",
                SupplierDescription = "Supplier Description",
                SupplierCnpj = "Supplier CNPJ"
            };

            var response = await handler.Handle(request, new CancellationToken());
            
            var updatedProduct = await productRepository.GetByIdOrDefaultAsync(request.Id);
            updatedProduct.Id.Should().Be(product.Id);
            updatedProduct.Code.Should().Be(product.Code);
            updatedProduct.Should().NotBeNull();
            updatedProduct.Description.Should().Be(request.Description);
            updatedProduct.ManufactureDate.Should().Be(request.ManufactureDate);
            updatedProduct.DueDate.Should().Be(request.DueDate);
            updatedProduct.Supplier.Code.Should().Be(request.SupplierCode);
            updatedProduct.Supplier.Description.Should().Be(request.SupplierDescription);
            updatedProduct.Supplier.Cnpj.Should().Be(request.SupplierCnpj);
            
            response.Should().NotBeNull();
        }
        
        [Test]
        public void Update_Product_With_Failure_Product_Not_Found()
        {
            var inMemoryRepositoryFactory = InMemoryRepositoryFactory.GetInstance();
            var productRepository = inMemoryRepositoryFactory.ProductRepository;

            var mapper = AutoMapperBuilder.Build();
            var handler = new UpdateProductHandler(productRepository, mapper);
            
            var request = new UpdateProductRequest
            {
                Id = Guid.NewGuid(),
                Description = "Product Description",
                ManufactureDate = DateTime.Now.Date,
                DueDate = DateTime.Now.AddMonths(1).Date,
                SupplierCode = "Supplier Code",
                SupplierDescription = "Supplier Description",
                SupplierCnpj = "Supplier CNPJ"
            };

            Assert.ThrowsAsync<ApiNotFoundException>(() => handler.Handle(request, new CancellationToken()),
                $"Product of id:{request.Id} not found");
        }
    }
}