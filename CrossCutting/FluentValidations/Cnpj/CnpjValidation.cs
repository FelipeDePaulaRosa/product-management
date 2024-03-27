using System.Text.RegularExpressions;

namespace CrossCutting.FluentValidations.Cnpj
{
    public static class CnpjValidation
    {
        public static bool IsValid(string cnpj)
        {
            if (string.IsNullOrWhiteSpace(cnpj))
                return false;

            const string regex = @"\d{2}.\d{3}.\d{3}/\d{4}-\d{2}";
            return new Regex(regex).IsMatch(cnpj);
        }
    }
}