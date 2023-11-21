using System;
using System.Net;
using System.Net.Mail;
using System.Configuration;
using System.Collections.Generic;
using System.Text;

namespace TaskIntegratorProject_V1_2.Sending_eMails
{
    public class eMailer
    {
        private eMailer() { }

        private string emailOrigin = EmailOrigin();
        private string nameOrigin;
        private string password = Password();
        private string destiny;
        private string title;
        private string messageBody;

        private static eMailer instance = new eMailer();

        public static eMailer Instance
        {
            get { return instance; }
        }

        private static string EmailOrigin()
        {
            return ConfigurationManager.AppSettings["EmailOrigin"];
        }

        private static string Password()
        {
            return ConfigurationManager.AppSettings["Password"];
        }

        private void eMailInformations()
        {
            Console.Write("Enter the name you want to appear when reading your email: ");
            nameOrigin = Console.ReadLine();

            Console.Write("Enter the e-mail destiny: ");
            destiny = Console.ReadLine();

            Console.Write("Enter the e-mail title: ");
            title = Console.ReadLine();

            Console.Write("Enter the message body: ");
            messageBody = Console.ReadLine();
        }

        public void SendEmail()
        {
            MailMessage emailSender = new MailMessage();
            eMailInformations();

            try
            {
                var smtpServerConfig = new SmtpClient("smtp.gmail.com", 587);

                smtpServerConfig.EnableSsl = true;
                smtpServerConfig.Timeout = 140 * 140;
                smtpServerConfig.UseDefaultCredentials = false;
                smtpServerConfig.Credentials = new NetworkCredential(emailOrigin, password);

                emailSender.From = new MailAddress(emailOrigin, nameOrigin);
                emailSender.Subject = title;
                emailSender.Body = messageBody;
                emailSender.IsBodyHtml = true;
                emailSender.Priority = MailPriority.Normal;
                emailSender.To.Add(destiny);

                smtpServerConfig.Send(emailSender);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\n**** e-Mail sended!! ****");
                Console.ResetColor();

            } catch(Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Erro ao enviar o e-mail...\n");
                Console.ResetColor();

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Tivemos o seguinte problema:\n{ex}");
                Console.ResetColor();
                return;
            }
        }
    }
}
