using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskIntegratorProject_V1_2.Manipulating_Clients_Files;
using TaskIntegratorProject_V1_2.Sending_eMails;

namespace TaskIntegratorProject_V1_2.CentralApp_File
{
    public class ExecuteWholeApp
    {
        private static void Apresentation()
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("\n\t\t=================================================================================");
            Console.WriteLine("\t\t\t===============================================================");
            Console.WriteLine("\t\t\t========= WELCOME TO THE TASK INTEGRATOR PROJECT V1.2 =========");
            Console.WriteLine("\t\t\t===============================================================");
            Console.WriteLine("\t\t=================================================================================\n\n");
            Console.ResetColor();
        }

        private static int Menu()
        {
            int option;

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\t\t**** Please, choose some operation to execute. ****");
            Console.ResetColor();

            do
            {
                Console.WriteLine("\n\t0 - Get out from App\n\t1 - Access the Platform Customer File Control (PCFC)\n\t2 - Send eMail.");

                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("\nAnswer: ");
                option = Convert.ToInt32(Console.ReadLine());
                Console.ResetColor();

                Console.Clear();

                if (option != 0 && option != 1 && option != 2)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid Value!!!");
                    Console.ResetColor();
                }

            }while(option != 0 && option != 1 && option != 2);

            return option;
        }

        private static void ExecutingMenuSelection()
        {
            switch (Menu())
            {
                case 0:
                    break;
                case 1:
                    var manipuleClientsFiles = new ManipulatingClientsFiles();
                    manipuleClientsFiles.ExecuteEnvironment();
                    break;
                case 2:
                    eMailer.Instance.SendEmail();
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.BackgroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("INVALID VALUE!!");
                    Console.ResetColor();
                    ExecutingMenuSelection();
                    break;
            }
        }

        public static void ExecuteApplication()
        {
            string option;
            Apresentation();
            do
            {
                ExecutingMenuSelection();
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write("\n\tWould You like to execute the App again (Y/N)? ");
                Console.ResetColor();
                option = Console.ReadLine();

                if(option.ToUpper() != "Y" && option.ToUpper() != "N")
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n\t\tOption selected is not valid!! Please, select a valid value!!\n");
                    Console.ResetColor();
                    option = "UNDEFINED";
                
                } else if(option.ToUpper() == "Y")
                {
                    Console.Clear();
                }

            } while (option.ToUpper() == "Y" || option.ToUpper() == "UNDEFINED");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n\tNow press any key to end the application, please...");
            Console.ResetColor();

            Console.ReadKey();
        }
    }
}
