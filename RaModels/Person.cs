using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaModels
{
    public class Person
    {        
        public string UUID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string CompanyName { get; set; }
        public List<ContactInfo> ContactInfos { get; set; }
    }
}
