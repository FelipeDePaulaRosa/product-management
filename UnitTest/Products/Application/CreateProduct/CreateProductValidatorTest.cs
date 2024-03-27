using System;
using NUnit.Framework;
using System.Threading.Tasks;
using Domain.Products.Entities;
using FluentValidation.TestHelper;
using Application.Products.CreateProduct;
using UnitTest.TestHelpers.InMemoryDatabaseHelpers;

namespace UnitTest.Products.Application.CreateProduct
{
    public class CreateProductValidatorTest
    {
        [Test]
        public async Task Create_Product_Request_Is_Valid()
        {
            var inMemoryRepository = InMemoryRepositoryFactory.GetInstance();
            var productRepository = inMemoryRepository.ProductRepository;

            var validator = new CreateProductValidator(productRepository);
            var request = new CreateProductRequest()
            {
                Description = "Descrição",
                ManufactureDate = DateTime.UtcNow.Date,
                DueDate = DateTime.UtcNow.AddDays(30).Date,
                SupplierCnpj = "64.255.066/0001-05",
                SupplierDescription = "Descrição do fornecedor",
                SupplierCode = "001"
            };

            var result = await validator.TestValidateAsync(request);
            result.ShouldNotHaveAnyValidationErrors();
        }
        
        [Test]
        public async Task Fail_When_Already_Has_Product_With_Same_Description()
        {
            var inMemoryRepository = InMemoryRepositoryFactory.GetInstance();
            var productRepository = inMemoryRepository.ProductRepository;
            
            var product = new Product("Description",
                DateTime.Now.Date,
                DateTime.Now.AddMonths(1).Date,
                "001",
                "Main Supplier",
                "68.159.892/0001-01");
            
            await productRepository.CreateAsync(product);

            var validator = new CreateProductValidator(productRepository);
            var request = new CreateProductRequest()
            {
                Description = product.Description,
                ManufactureDate = DateTime.UtcNow.AddDays(30).Date,
                DueDate = DateTime.UtcNow.Date,
                SupplierCnpj = string.Empty,
                SupplierDescription = string.Empty,
                SupplierCode = string.Empty
            };

            var result = await validator.TestValidateAsync(request);
            
            result.ShouldHaveValidationErrorFor(x => x)
                .WithErrorMessage("Já existe um produto cadastrado com essa descrição");
            
            result.ShouldHaveValidationErrorFor(x => x)
                .WithErrorMessage("A data de fabricação deve ser menor que a data de vencimento");

            result.ShouldHaveValidationErrorFor(x => x.SupplierCnpj)
                .WithErrorMessage("O CNPJ informado é inválido");
            
            result.ShouldHaveValidationErrorFor(x => x.SupplierCode)
                .WithErrorMessage("O campo Código do fornecedor é obrigatório");
            
            result.ShouldHaveValidationErrorFor(x => x.SupplierDescription)
                .WithErrorMessage("O campo Descrição do fornecedor é obrigatório");
        }
    }
}