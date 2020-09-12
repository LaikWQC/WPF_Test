using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System.Linq;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using WPF_Test.Models;
using WPF_Test.Services;
using WPF_Test.Views;
using System;

namespace WPF_Test.ViewModels
{
    public class MainViewModel : MyViewModel
    {
        public MainViewModel(Window view) : base(view)
        {
            var service = new PersonService();
            _persons = new ObservableCollection<Person>(service.GetPersons());

            CreateCommand = new RelayCommand(CreateAction);
            EditCommand = new RelayCommand(EditAction, CanEdit);
            DeleteCommand = new RelayCommand(DeleteAction, CanEdit);
        }

        public const string PersonsPropertyName = nameof(Persons);
        private ObservableCollection<Person> _persons;
        public ObservableCollection<Person> Persons
        {
            get => _persons;
            set
            {
                if (_persons == value) return;
                _persons = value;
                RaisePropertyChanged(PersonsPropertyName);
            }
        }

        public const string SelectedPersonPropertyName = nameof(SelectedPerson);
        private Person _selectedPerson;
        public Person SelectedPerson
        {
            get => _selectedPerson;
            set
            {
                if (_selectedPerson == value) return;
                _selectedPerson = value;
                RaisePropertyChanged(SelectedPersonPropertyName);
                EditCommand.RaiseCanExecuteChanged();
            }
        }

        public RelayCommand CreateCommand { get; private set; }
        public RelayCommand EditCommand { get; private set; }
        public RelayCommand DeleteCommand { get; private set; }

        private void CreateAction()
        {
            var viewModel = new CreateViewModel(new PersonCardView(), (p)=> Persons.Add(p));
            SetupWindowAndShow(viewModel.View);
        }

        private void EditAction()
        {
            var viewModel = new EditViewModel(new PersonCardView(), SelectedPerson, AfterEdition);
            SetupWindowAndShow(viewModel.View);
        }

        private void AfterEdition(Person person)
        {
            var index = Persons.IndexOf(SelectedPerson);
            Persons[index] = person;
            SelectedPerson = Persons[index];
        }

        private bool CanEdit()
        {
            return SelectedPerson != null;
        }
        
        private void DeleteAction()
        {
            Persons.Remove(SelectedPerson);
        }

        private void SetupWindowAndShow(Window window)
        {
            window.Owner = View;
            window.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            window.ShowDialog();
        }
    }
}