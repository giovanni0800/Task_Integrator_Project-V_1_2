using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskIntegratorProject_V1_2.Validators;

namespace TaskIntegratorProject_V1_2.Manipulating_Clients_Files
{
    class Supplier : PeopleBase
    {
        public Supplier() { }

        public override List<IPerson> Listate()
        {
            var peopleList = new List<IPerson>();

            if (File.Exists(FileDirectory()))
            {
                using (StreamReader filesReader = File.OpenText(FileDirectory()))
                {

                    string fileLine;
                    int index = 0;

                    while ((fileLine = filesReader.ReadLine()) != null)
                    {
                        if(index == 0)
                        {
                            index++;
                            continue;
                        }

                        var formatedFileLine = fileLine.Split(";");

                        var actualPerson = (IPerson)Activator.CreateInstance(this.GetType());
                        actualPerson.Name = formatedFileLine[0];
                        actualPerson.CPF = formatedFileLine[1];
                        actualPerson.Phone = formatedFileLine[2];
                        actualPerson.CNPJ = formatedFileLine[3];
                        actualPerson.eMail = formatedFileLine[4];
                        actualPerson.CEP = formatedFileLine[5];

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

        public override void ShowAllPeople()
        {
            foreach (PeopleBase showList in Listate())
            {
                Console.WriteLine($"\nName: {showList.Name}\t CPF: {showList.CPF}\t Phone: {showList.Phone}\t CNPJ: {showList.CNPJ}\t e-Mail: {showList.eMail}\t CEP: {showList.CEP}");
            }
        }

        public override void AddNewInformations()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Type the Name: ");
            Name = Console.ReadLine();

            CPF = ValidatingCPF.ExecuteValidation("Type the CPF: ");

            Console.Write("Type the Phone: ");
            Phone = Console.ReadLine();

            CNPJ = ValidatingCNPJ.ExecutateValidation("Type the CNPJ: ");

            eMail = ValidatingEmail.ExecutateValidation("Type the e-mail: ");

            CEP = ValidatingCEP.ExecuteValidation("Type the CEP: ");
            Console.ResetColor();
        }

        public override void Save()
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
            streamWriter.WriteLine("Name;CPF;Phone;CNPJ;e-mail;CEP");

            foreach (var list in peopleList)
            {
                streamWriter.WriteLine($"{list.Name};{list.CPF};{list.Phone};{list.CNPJ};{list.eMail};{list.CEP}");
            }

            streamWriter.Close();
        }
    }
}
