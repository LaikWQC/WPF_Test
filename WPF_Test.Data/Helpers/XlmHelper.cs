using System;
using System.Collections.Generic;
using System.Xml;
using WPF_Test.Data.Models;

namespace WPF_Test.Data.Helpers
{
    public static class XlmHelper
    {
        public static void Save(string fileName, IEnumerable<Person> persons)
        {
            XmlDocument xDoc = new XmlDocument();
            XmlElement xRoot = xDoc.CreateElement("persons");
            xDoc.AppendChild(xRoot);

            foreach (var person in persons)
            {
                XmlElement userElem = xDoc.CreateElement("person");
                XmlAttribute nameAttr = xDoc.CreateAttribute("name");
                XmlElement surnameElem = xDoc.CreateElement("surname");
                XmlElement patronymicElem = xDoc.CreateElement("patronymic");
                XmlElement emailElem = xDoc.CreateElement("email");

                XmlText nameText = xDoc.CreateTextNode(person.Name);
                XmlText surnameText = xDoc.CreateTextNode(person.Surname);
                XmlText patronymicText = xDoc.CreateTextNode(person.Patronymic);
                XmlText emailText = xDoc.CreateTextNode(person.Email);
                
                nameAttr.AppendChild(nameText);
                surnameElem.AppendChild(surnameText);
                patronymicElem.AppendChild(patronymicText);
                emailElem.AppendChild(emailText);
                userElem.Attributes.Append(nameAttr);
                userElem.AppendChild(surnameElem);
                userElem.AppendChild(patronymicElem);
                userElem.AppendChild(emailElem);
                xRoot.AppendChild(userElem);
            }
            xDoc.Save(fileName);
        }

        public static IEnumerable<Person> Load(string fileName)
        {
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(fileName);
            XmlElement xRoot = xDoc.DocumentElement;
            foreach (XmlElement xnode in xRoot)
            {
                Person person = new Person();
                XmlNode attr = xnode.Attributes.GetNamedItem("name");
                if (attr != null)
                    person.Name = attr.Value;

                foreach (XmlNode childnode in xnode.ChildNodes)
                {
                    switch(childnode.Name)
                    {
                        case "surname":
                            person.Surname = childnode.InnerText;
                            break;
                        case "patronymic":
                            person.Patronymic = childnode.InnerText;
                            break;
                        case "email":
                            person.Email = childnode.InnerText;
                            break;
                    }
                }
                yield return person;
            }
        }
    }
}
