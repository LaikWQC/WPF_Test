﻿using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Windows;
using WPF_Test.Models;

namespace WPF_Test.ViewModels
{
    public class EditViewModel : PersonCardViewModel
    {
        private readonly Action<Person> _afterEdition;

        public EditViewModel(Window view, Person person, Action<Person> afterEdition) : base(view)
        {
            _afterEdition = afterEdition;

            Setup(person);

            OkCommand = new RelayCommand(Edit);

            Title = "Редактирование";
            OkButtonText = "Применить";
        }

        private void Setup(Person person)
        {
            Name = person.Name;
            Surname = person.Surname;
            Patronymic = person.Patronymic;
            Email = person.Email;
        }

        private void Edit()
        {
            var person = new Person()
            {
                Name = Name,
                Patronymic = Patronymic,
                Surname = Surname,
                Email = Email
            };

            Close();
            _afterEdition(person);
        }
    }
}