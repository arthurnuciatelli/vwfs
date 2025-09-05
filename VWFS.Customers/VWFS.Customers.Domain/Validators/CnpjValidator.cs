using System.Text.RegularExpressions;

namespace VWFS.Customers.Domain.Validators
{
    public static class CnpjValidator
    {
        public static bool IsValid(string cnpj)
        {
            if (string.IsNullOrWhiteSpace(cnpj))
                return false;

            cnpj = Regex.Replace(cnpj, "[^0-9]", "");

            if (cnpj.Length != 14)
                return false;

            // Checa sequências repetidas
            if (new string(cnpj[0], cnpj.Length) == cnpj)
                return false;

            int[] multipliers1 = { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multipliers2 = { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

            string tempCnpj = cnpj.Substring(0, 12);
            int sum = 0;

            for (int i = 0; i < 12; i++)
                sum += int.Parse(tempCnpj[i].ToString()) * multipliers1[i];

            int firstCheck = sum % 11;
            firstCheck = firstCheck < 2 ? 0 : 11 - firstCheck;

            if (firstCheck != int.Parse(cnpj[12].ToString()))
                return false;

            tempCnpj += firstCheck.ToString();
            sum = 0;

            for (int i = 0; i < 13; i++)
                sum += int.Parse(tempCnpj[i].ToString()) * multipliers2[i];

            int secondCheck = sum % 11;
            secondCheck = secondCheck < 2 ? 0 : 11 - secondCheck;

            return secondCheck == int.Parse(cnpj[13].ToString());
        }
    }
}
