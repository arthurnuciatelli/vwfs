using System.Text.RegularExpressions;

namespace VWFS.Customers.Application.Handlers
{
    public static class DocumentValidator
    {
        public static bool IsCpf(string cpf)
        {
            // Validação básica (apenas formato)
            return Regex.IsMatch(cpf, @"^\d{11}$");
        }

        public static bool IsCnpj(string cnpj)
        {
            // Validação básica (apenas formato)
            return Regex.IsMatch(cnpj, @"^\d{14}$");
        }
    }
}
