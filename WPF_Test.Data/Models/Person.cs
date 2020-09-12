using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Test.Data.Models
{
    public class Person
    {
        public string Name { get; set; }
        public string Patronymic { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }

        public string FullName => string.Format("{0} {1} {2}", Surname, Name, Patronymic);
    }
}
