using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using TaskIntegratorProject_V1_2.Validators;

namespace TaskIntegratorProject_V1_2.Manipulating_Clients_Files
{
    class PeopleBase : IPerson {

        public string Name { get; set; }
        public string CPF {  get; set; }
        public string Phone { get; set; }
        public string CNPJ { get; set; }
        public string eMail { get; set; }
        public string CEP { get; set; }

        public PeopleBase() { }

        protected string DirectoryPeopleFiles()
        {
            return ConfigurationManager.AppSettings["DirectoryPeopleFiles"];
        }

        protected string FileDirectory()
        {
            return ConfigurationManager.AppSettings[this.GetType().Name];
        }

        public virtual void ApresentationApp()
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("\n====================================================================================");
            Console.WriteLine($"\n\t\tWelcome to the {this.GetType().Name} Manager v1.2");
            Console.WriteLine("\n====================================================================================\n");
            Console.ResetColor();
        }

        public virtual int Menu()
        {
            Console.WriteLine("Options available to choose from are:\n" +
                        "\t0- To get out of program.\n" +
                        "\t1- Read some existing file.\n" +
                        "\t2- Make new recording.\n");
            Console.Write("Select Your choose: ");
            int answer = Convert.ToInt32(Console.ReadLine());
            return answer;
        }

        public virtual List<IPerson> Listate()
        {
            var peopleList = new List<IPerson>();

            if (File.Exists(FileDirectory()))
            {
                using (StreamReader filesReader = File.OpenText(FileDirectory())){

                    string fileLine;
                    int index = 0;

                    while((fileLine = filesReader.ReadLine()) != null)
                    {
                        if (index == 0)
                        {
                            index++;
                            continue;
                        }

                        var formatedFileLine = fileLine.Split(";");

                        var actualPerson = (IPerson) Activator.CreateInstance(this.GetType());
                        actualPerson.Name = formatedFileLine[0];
                        actualPerson.CPF = formatedFileLine[1];
                        actualPerson.Phone = formatedFileLine[2];
                        actualPerson.eMail = formatedFileLine[3];
                        actualPerson.CEP = formatedFileLine[4];

                        peopleList.Add(actualPerson);
                    }
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\n**** File not Found... ***\n");
                Console.ResetColor();
            }

            return peopleList;
        }

        public virtual void ShowAllPeople()
        {
            foreach (PeopleBase showList in Listate())
            {
                Console.WriteLine($"\nName: {showList.Name}\t CPF: {showList.CPF}\t Phone: {showList.Phone}\t e-Mail: {showList.eMail}\t CEP: {showList.CEP}");
            }
        }

        public virtual void AddNewInformations()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Type the Name: ");
            Name = Console.ReadLine();

            CPF = ValidatingCPF.ExecuteValidation("Type the CPF: ");

            Console.Write("Type the Phone: ");
            Phone = Console.ReadLine();

            eMail = ValidatingEmail.ExecutateValidation("Type the e-mail: ");

            CEP = ValidatingCEP.ExecuteValidation("Type the CEP: ");
            Console.ResetColor();
        }

        public virtual void Save()
        {
            var peopleList = Listate();

            AddNewInformations();
            peopleList.Add(this);

            if (!File.Exists(FileDirectory()))
            {
                Directory.CreateDirectory(DirectoryPeopleFiles());
                var newFile = File.CreateText(FileDirectory());
                newFile.Close();
            }

            StreamWriter streamWriter = new StreamWriter(FileDirectory());
            streamWriter.WriteLine("Name;CPF;Phone;e-Mail;CEP");

            foreach (var list in peopleList)
            {
                streamWriter.WriteLine($"{list.Name};{list.CPF};{list.Phone};{list.eMail};{list.CEP}");
            }

            streamWriter.Close();
        }

        public virtual string ValidatingExecutionLoop()
        {
            string answer;

            do
            {
                Console.Write("\nWould You like to run the App again (Y/N)? ");
                answer = Console.ReadLine();

            } while (answer.ToUpper() != "Y" && answer.ToUpper() != "N");

            Console.Clear();

            ApresentationApp();

            return answer.ToUpper();
        }

        public virtual void ExecuteApp()
        {
            try
            {
                int option;
                int repeat;

                ApresentationApp();

                do
                {
                    option = Menu();


                    if (option == 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine("Finishing program...\n");
                        Console.ResetColor();
                        break;
                    }

                    else if (option == 1)
                    {
                        ShowAllPeople();
                    }

                    else if (option == 2)
                    {
                        Save();
                    }

                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid value...\n");
                        Console.ResetColor();
                        repeat = 1;
                        continue;
                    }


                    if (ValidatingExecutionLoop() == "Y")
                    {
                        repeat = 1;
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Restarting the program\n\n");
                        Console.ResetColor();
                    }
                    else
                    {
                        repeat = 0;
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.WriteLine("\nEnding the program\n\n");
                        Console.ResetColor();
                    }

                } while (repeat == 1);
            }

            catch (Exception ex)
            {
                Console.BackgroundColor = ConsoleColor.Gray;
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("\n\n\t\t\t\t!!!! NOOOOOOOOOOOOOOOOOOOOOOOOOOO !!!!");
                Console.ResetColor();

                Console.BackgroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("\n\n\tCongratulations... Were you testing the application? Well, you did it... Closing the App...");
                Console.ResetColor();

                Console.BackgroundColor = ConsoleColor.Gray;
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("\t\t---- Here are the errors you wanted to see there, my friend... ----\n");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex);
                Console.ResetColor();
            }
        }
    }
}
