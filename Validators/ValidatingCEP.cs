using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskIntegratorProject_V1_2.Validators
{
    public class ValidatingCEP
    {
        private static bool CEPValidator(string cep)
        {
            cep = cep.Trim();
            cep = cep.Replace(".", "").Replace("-", "").Replace("/", "");
            cep = cep.Substring(0, 5) + "-" + cep.Substring(5, 3);

            return System.Text.RegularExpressions.Regex.IsMatch(cep, ("[0-9]{5}-[0-9]{3}"));
        }

        public static string ExecuteValidation(string text)
        {
            string cep;

            do
            {
                Console.Write(text);
                cep = Console.ReadLine();

                if (CEPValidator(cep) == false)
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("Invalid CEP... Please type again.\n");
                    Console.ResetColor();
                }

            } while (CEPValidator(cep) == false);

            return cep;
        }
    }
}
