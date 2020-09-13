using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_Test.Data.Models;

namespace WPF_Test.Data.Helpers
{
    public class BinHelper
    {
        public static void Save(string fileName, IEnumerable<Person> persons)
        {
            BinaryWriter writer = new BinaryWriter(File.Open(fileName, FileMode.Create));
            foreach (var person in persons)
            {
                writer.Write(person.Name ?? "");
                writer.Write(person.Surname ?? "");
                writer.Write(person.Patronymic ?? "");
                writer.Write(person.Email ?? "");
            }
            writer.Close();
        }

        public static IEnumerable<Person> Load(string fileName)
        {
            var persons = new List<Person>();
            BinaryReader reader = new BinaryReader(File.Open(fileName, FileMode.Open));
            while (reader.BaseStream.Position != reader.BaseStream.Length)
            {
                var person = new Person();
                person.Name = reader.ReadString();
                person.Surname = reader.ReadString();
                person.Patronymic = reader.ReadString();
                person.Email = reader.ReadString();

                persons.Add(person);
            }
            reader.Close();
            return persons;
        }
    }
}
