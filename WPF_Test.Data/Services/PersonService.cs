using System;
using System.Collections.Generic;
using System.IO;
using WPF_Test.Data.Helpers;
using WPF_Test.Data.Models;

namespace WPF_Test.Data.Services
{
    public class PersonService
    {
        public void SavePersons(string fileName, IEnumerable<Person> persons, Action<Exception> afterSave)
        {
            Exception ex = null;
            try
            {
                switch(Path.GetExtension(fileName))
                {
                    case ".xml":
                        XmlHelper.Save(fileName, persons);
                        break;
                    case ".bin":
                        BinHelper.Save(fileName, persons);
                        break;
                    default:
                        throw new Exception("Неверный формат файла");
                }
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
                switch (Path.GetExtension(fileName))
                {
                    case ".xml":
                        persons = XmlHelper.Load(fileName);
                        break;
                    case ".bin":
                        persons = BinHelper.Load(fileName);
                        break;
                    default:
                        throw new Exception("Неверный формат файла");
                }                
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
