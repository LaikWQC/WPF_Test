using System;
using System.Collections.Generic;
using WPF_Test.Data.Helpers;
using WPF_Test.Data.Models;

namespace WPF_Test.Data.Services
{
    public class PersonService
    {
        public IEnumerable<Person> GetPersons()
        {
            yield return new Person() { Name = "Лёха" };
            yield return new Person() { Name = "Санёк" };
        }

        public void SavePersons(string fileName, IEnumerable<Person> persons, Action<Exception> afterSave)
        {
            Exception ex = null;
            try
            {
                XlmHelper.Save(fileName, persons);
            }
            catch(Exception e)
            {
                ex = e;
            }
            finally
            {
                afterSave(ex);
            }
        }

        public void LoadPersons(string fileName, Action<IEnumerable<Person>, Exception> afterLoad)
        {
            Exception ex = null;
            IEnumerable<Person> persons = null;
            try
            {
                persons = XlmHelper.Load(fileName);
            }
            catch (Exception e)
            {
                ex = e;
            }
            finally
            {
                afterLoad(persons, ex);
            }
        }
    }
}
