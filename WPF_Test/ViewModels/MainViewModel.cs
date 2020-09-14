using GalaSoft.MvvmLight.CommandWpf;
using System.Collections.ObjectModel;
using System.Windows;
using WPF_Test.Data.Models;
using WPF_Test.Data.Services;
using WPF_Test.Views;
using System;
using Microsoft.Win32;
using System.Collections.Generic;

namespace WPF_Test.ViewModels
{
    public class MainViewModel : MyViewModel
    {
        private readonly PersonService _service;

        public MainViewModel(Window view) : base(view)
        {
            _service = new PersonService();
            _persons = new ObservableCollection<Person>();

            CreateCommand = new RelayCommand(CreateAction);
            EditCommand = new RelayCommand(EditAction, CanEdit);
            DeleteCommand = new RelayCommand(DeleteAction, CanEdit);
            SaveCommand = new RelayCommand(SaveAction);
            LoadCommand = new RelayCommand(LoadAction);
        }

        #region Properties
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
        #endregion

        #region Commands
        public RelayCommand CreateCommand { get; private set; }
        public RelayCommand EditCommand { get; private set; }
        public RelayCommand DeleteCommand { get; private set; }
        public RelayCommand SaveCommand { get; private set; }
        public RelayCommand LoadCommand { get; private set; }
        #endregion

        #region Crud Actions
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
        #endregion

        #region Save&Load
        private void SaveAction()
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.Filter = "XML-File | *.xml|Binary File (*.bin)|*.bin";
            saveFileDialog1.RestoreDirectory = true;

            if (saveFileDialog1.ShowDialog() == true)
            {
                _service.SavePersonsAsync(saveFileDialog1.FileName, _persons, ShowSaveFileMessage);
            }
        }

        private void ShowSaveFileMessage(Exception ex)
        {
            string text = ex == null ? "Данные успешно сохранены" : $"Произошла ошибка:\n {ex.Message}";
            MessageBox.Show(text);
        }

        private void LoadAction()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Filter = "XML-File | *.xml|Binary File (*.bin)|*.bin";
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == true)
            {
                IsEnable = false;
                _service.LoadPersonsAsync(openFileDialog.FileName, LoadPersonsCollection);
            }
        }

        private void LoadPersonsCollection(IEnumerable<Person> persons, Exception ex)
        {
            if(ex!=null) MessageBox.Show($"Произошла ошибка:\n {ex.Message}");
            if (persons == null) return;
            Persons = new ObservableCollection<Person>(persons);
            IsEnable = true;
        }
        #endregion
    }
}