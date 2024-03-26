using System;
using NUnit.Framework;
using FluentAssertions;
using System.Threading.Tasks;
using Application.Products.CreateProduct;
using UnitTest.TestHelpers;
using UnitTest.TestHelpers.InMemoryDatabaseHelpers;

namespace UnitTest.Products.Application.CreateProduct
{
    public class CreateProductHandlerTest
    {
        [Test]
        public async Task Create_With_Success()
        {
            var mapper = AutoMapperBuilder.Build();
            var inMemoryRepositoryFactory = InMemoryRepositoryFactory.GetInstance();
            var productRepository = inMemoryRepositoryFactory.ProductRepository;
            
            var createProductHandler = new CreateProductHandler(mapper, productRepository);
            
            var request = new CreateProductRequest
            {
                Description = "Desc test",
                ManufactureDate = DateTime.UtcNow,
                DueDate = DateTime.UtcNow.AddDays(30),
                SupplierCode = "123",
                SupplierDescription = "Desc supplier",
                SupplierCnpj = "12345678901234"
            };
            
            var response = await createProductHandler.Handle(request, default);
            response.Should().NotBeNull();
            
            var createdProduct = await productRepository.GetByIdOrDefaultAsync(response.Id);
            createdProduct.Description.Should().Be(request.Description);
            createdProduct.ManufactureDate.Should().Be(request.ManufactureDate);
            createdProduct.DueDate.Should().Be(request.DueDate);
            createdProduct.Supplier.Code.Should().Be(request.SupplierCode);
            createdProduct.Supplier.Description.Should().Be(request.SupplierDescription);
            createdProduct.Supplier.Cnpj.Should().Be(request.SupplierCnpj);
        }
    }
}