using FluentValidation;

namespace Application.Products.CreateProduct
{
    public class CreateProductValidator: AbstractValidator<CreateProductRequest>
    {
        public CreateProductValidator()
        {
            RuleFor(x => x.Description)
                .NotEmpty()
                .WithMessage("O campo Descrição é obrigatório");
        }
    }
}