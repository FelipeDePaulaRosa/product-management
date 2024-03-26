using FluentValidation;
using FluentValidation.Validators;

namespace CrossCutting.FluentValidations.Cnpj
{
    public class CnpjFluentValidation<T, TProperty> : PropertyValidator<T, TProperty>
    {
        public override bool IsValid(ValidationContext<T> context, TProperty value)
        {
            var cnpj = value as string;
            return CnpjValidation.IsValid(cnpj);
        }

        public override string Name => nameof(CnpjValidation);
    }
}