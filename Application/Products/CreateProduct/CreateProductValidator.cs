using System.Linq;
using FluentValidation;
using Domain.Products.Contracts;
using Microsoft.EntityFrameworkCore;
using CrossCutting.FluentValidations.Cnpj;

namespace Application.Products.CreateProduct
{
    public class CreateProductValidator: AbstractValidator<CreateProductRequest>
    {
        private readonly IProductRepository _productRepository;
        
        public CreateProductValidator(IProductRepository productRepository)
        {
            _productRepository = productRepository;
            
            RuleFor(x => x.Description)
                .NotEmpty()
                .WithMessage("O campo Descrição é obrigatório");
            
            RuleFor(x => x)
                .Must(ProductDescriptionIsNotDuplicated)
                .WithMessage("Já existe um producto cadastrado com essa descrição");

            RuleFor(x => x)
                .Must(x => x.ManufactureDate < x.DueDate)
                .WithMessage("A data de fabricação deve ser menor que a data de vencimento");

            RuleFor(x => x.SupplierCnpj)
                .IsValidCnpj()
                .WithMessage("O CNPJ informado é inválido");
            
            RuleFor(x => x.SupplierCode)
                .NotEmpty()
                .WithMessage("O campo Código do fornecedor é obrigatório");
            
            RuleFor(x => x.SupplierDescription)
                .NotEmpty()
                .WithMessage("O campo Descrição do fornecedor é obrigatório");
        }
        
        private bool ProductDescriptionIsNotDuplicated(CreateProductRequest request)
        {
            var descriptionIsDuplicated = _productRepository
                .GetQueryable()
                .AsNoTracking()
                .Any(x => x.Description.Equals(request.Description));
            
            return !descriptionIsDuplicated;
        }
    }
}