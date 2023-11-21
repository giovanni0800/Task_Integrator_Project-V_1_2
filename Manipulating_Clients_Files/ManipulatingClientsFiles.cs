using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskIntegratorProject_V1_2.Manipulating_Clients_Files
{
    class ManipulatingClientsFiles
    {
        private int Menu()
        {
            int option;
            do
            {
                Console.WriteLine("Please choose some option:");
                Console.WriteLine("\t0 - To Get Out from app\n\t1 - To Work with Teachers file\n\t2 - To Work with Students file\n\t3 - To Work with Suppliers file");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("\nYour choose is: ");
                option = Convert.ToInt32(Console.ReadLine());
                Console.ResetColor();

            } while (option != 0 && option != 1 && option != 2 && option != 3);

            return option;
        }

        private int WorkingEnvironmentSelection()
        {
            int optionValue = 1;

            switch (Menu())
            {
                case 0:
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("Ok. Finishing App...");
                    Console.ResetColor();
                    optionValue = 0;
                    break;

                case 1:
                    var client = new Teacher();
                    client.ExecuteApp();
                break;

                case 2:
                    var user = new Student();
                    user.ExecuteApp();
                break;

                case 3:
                    var supplier = new Supplier();
                    supplier.ExecuteApp();
                break;

                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Value entered not accepted");
                    Console.ResetColor();
                break;
            }

            return optionValue;
        }

        private string ExecutateLoop()
        {
            string option;

            do
            {
                Console.Write("Would You like to access another file category (Y/N)? ");
                option = Console.ReadLine();

                if(option.ToUpper() != "Y" && option.ToUpper() != "N")
                {
                    Console.ForegroundColor= ConsoleColor.Red;
                    Console.WriteLine("!!! Invalid Value !!!");
                    Console.ResetColor();
                
                } else if (option.ToUpper() == "N")
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("\nFinishing the APP. Thanks for using!");
                    Console.ResetColor();
                }

            } while (option.ToUpper() != "Y" && option.ToUpper() != "N");

            return option.ToUpper();
        }

        public void ExecuteEnvironment()
        {
            do
            {
                int shouldAppStop = WorkingEnvironmentSelection();

                if (shouldAppStop == 0) break;

            } while(ExecutateLoop() == "Y");
        }
    }
}
