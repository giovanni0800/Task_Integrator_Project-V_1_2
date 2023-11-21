using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskIntegratorProject_V1_2.Manipulating_Clients_Files
{
    interface IPerson
    {
        public string Name { get; set; }
        public string CPF { get; set; }
        public string Phone {  get; set; }
        public string CNPJ { get; set; }
        public string eMail {  get; set; }
        public string CEP {  get; set; }
        public List<IPerson> Listate();
        public void Save();
    }
}
