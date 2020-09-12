using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_Test.Models;

namespace WPF_Test.Services
{
    public class PersonService
    {
        public IEnumerable<Person> GetPersons()
        {
            yield return new Person() { Name = "Лёха" };
            yield return new Person() { Name = "Санёк" };
        }
    }
}
