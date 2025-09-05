using System.Text.RegularExpressions;

namespace VWFS.Customers.Domain.Validators
{
    public static class CpfValidator
    {
        public static bool IsValid(string cpf)
        {
            if (string.IsNullOrWhiteSpace(cpf))
                return false;

            cpf = Regex.Replace(cpf, "[^0-9]", ""); // Remove caracteres não numéricos

            if (cpf.Length != 11)
                return false;

            // Checa sequências repetidas
            if (new string(cpf[0], cpf.Length) == cpf)
                return false;

            // Validação dos dígitos verificadores
            var numbers = cpf.Substring(0, 9);
            var digits = cpf.Substring(9, 2);

            int sum = 0;
            for (int i = 0; i < 9; i++)
                sum += int.Parse(numbers[i].ToString()) * (10 - i);
            int firstCheck = sum % 11;
            firstCheck = firstCheck < 2 ? 0 : 11 - firstCheck;

            if (firstCheck != int.Parse(digits[0].ToString()))
                return false;

            sum = 0;
            numbers += firstCheck.ToString();
            for (int i = 0; i < 10; i++)
                sum += int.Parse(numbers[i].ToString()) * (11 - i);
            int secondCheck = sum % 11;
            secondCheck = secondCheck < 2 ? 0 : 11 - secondCheck;

            return secondCheck == int.Parse(digits[1].ToString());
        }
    }
}
