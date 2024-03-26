using FluentValidation;

namespace CrossCutting.FluentValidations.Cnpj
{
    public static class CnpjRuleBuilder
    {
        public static IRuleBuilderOptions<T, string> IsValidCnpj<T>(
            this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.SetValidator(new CnpjFluentValidation<T, string>());
        }
    }
}