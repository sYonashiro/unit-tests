using Flunt.Notifications;
using Flunt.Validations;
using System.Collections.Generic;
using System.Linq;
using UnitTestsProject.Domain.Enums;

namespace UnitTestsProject.Domain.ValueObjects
{
    public class Document : Notifiable
    {
        public Document(string number, EDocType type)
        {
            Number = number;
            Type = type;

            AddNotifications(new Contract()
                .IsFalse(Type == EDocType.CNPJ && !IsValidCnpj(), $"{nameof(Document)}.{nameof(Number)}", "CNPJ inválido.")
                .IsFalse(Type == EDocType.CPF && !IsValidCpf(), $"{nameof(Document)}.{nameof(Number)}", "CPF inválido."));
        }

        public string Number { get; private set; }
        public EDocType Type { get; private set; }

        private bool IsValidCnpj()
        {
            var multiplier1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            var multiplier2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            var sum = 0;
            var rest = 0;
            var digit = string.Empty;

            var tempCnpj = string.Empty;
            tempCnpj = Number.Trim();
            tempCnpj = tempCnpj.Replace(".", "").Replace("-", "").Replace("/", "");

            if (tempCnpj.Length != 14)
                return false;

            // Valida se todas as informações não são iguais.
            if (ValidateAllNumbersAreEqual(tempCnpj))
                return false;

            tempCnpj = tempCnpj.Substring(0, 12);
            sum = 0;

            for (int i = 0; i < 12; i++)
                sum += int.Parse(tempCnpj[i].ToString()) * multiplier1[i];

            rest = (sum % 11);

            if (rest < 2)
                rest = 0;
            else
                rest = 11 - rest;

            digit = rest.ToString();
            tempCnpj = tempCnpj + digit;
            sum = 0;

            for (int i = 0; i < 13; i++)
                sum += int.Parse(tempCnpj[i].ToString()) * multiplier2[i];

            rest = (sum % 11);

            if (rest < 2)
                rest = 0;
            else
                rest = 11 - rest;

            digit = digit + rest.ToString();

            return Number.EndsWith(digit);
        }

        private bool ValidateAllNumbersAreEqual(string documentOnlyNumbers)
        {
            List<string> list = new List<string>();

            for (int iC = 0; iC < documentOnlyNumbers.Length; iC++)
                list.Add(documentOnlyNumbers.Substring(iC, 1));

            if (list.GroupBy(x => x).Count() == 1)
                return true;
            else
                return false;
        }

        private bool IsValidCpf()
        {
            int[] multiplier1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplier2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf;
            string digit;
            int sum;
            int rest;

            tempCpf = Number.Trim();
            tempCpf = tempCpf.Replace(".", "").Replace("-", "");

            if (tempCpf.Length != 11)
                return false;

            tempCpf = tempCpf.Substring(0, 9);
            sum = 0;

            for (int i = 0; i < 9; i++)
                sum += int.Parse(tempCpf[i].ToString()) * multiplier1[i];

            rest = sum % 11;

            if (rest < 2)
                rest = 0;
            else
                rest = 11 - rest;

            digit = rest.ToString();
            tempCpf = tempCpf + digit;
            sum = 0;

            for (int i = 0; i < 10; i++)
                sum += int.Parse(tempCpf[i].ToString()) * multiplier2[i];

            rest = sum % 11;

            if (rest < 2)
                rest = 0;
            else
                rest = 11 - rest;

            digit = digit + rest.ToString();

            return Number.EndsWith(digit);
        }
    }
}
