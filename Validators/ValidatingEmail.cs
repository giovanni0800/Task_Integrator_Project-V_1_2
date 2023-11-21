using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace TaskIntegratorProject_V1_2.Validators
{
    public class ValidatingEmail
    {
        private static bool eMailValidator(string emailToBeValidated)
        {
            string eMail;
            bool isEmailValid;

            eMail = emailToBeValidated;

            try
            {
                MailAddress mailAdress = new MailAddress(eMail);
                isEmailValid = true;
            }
            catch
            {
                isEmailValid = false;
            }

            return isEmailValid;
        }

        public static string ExecutateValidation(string text)
        {
            string eMail;

            do
            {
                Console.Write(text);
                eMail = Console.ReadLine();

                if (eMailValidator(eMail) == false)
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("Invalid eMail... Please type again.\n");
                    Console.ResetColor();
                }

            } while (eMailValidator(eMail) == false);

            return eMail;
        }
    }
}
