using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Windows;
using WPF_Test.Data.Models;

namespace WPF_Test.ViewModels
{
    public class CreateViewModel : PersonCardViewModel
    {
        private readonly Action<Person> _afterCreation;

        public CreateViewModel(Window view, Action<Person> afterCreation) : base(view)
        {
            _afterCreation = afterCreation;

            OkCommand = new RelayCommand(Create);

            Title = "Добавление";
            OkButtonText = "Добавить";
        }

        private void Create()
        {
            var person = new Person() { 
                Name = Name, 
                Patronymic = Patronymic, 
                Surname = Surname, 
                Email = Email };

            Close();
            _afterCreation(person);
        }
    }
}
