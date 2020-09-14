using GalaSoft.MvvmLight.CommandWpf;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;

namespace WPF_Test.ViewModels
{
    public abstract class PersonCardViewModel : MyViewModel
    {
        public PersonCardViewModel(Window view) : base(view)
        {
            CloseCommand = new RelayCommand(Close);
        }

        private string _name;
        public string Name 
        {
            get => _name;
            set
            {
                _name = OnlyLetterString(value);
                RaisePropertyChanged(nameof(Name));
            }
        }

        private string _patronymic;
        public string Patronymic 
        {
            get => _patronymic;
            set
            {
                _patronymic = OnlyLetterString(value);
                RaisePropertyChanged(nameof(Patronymic));
            }
        }

        private string _surname;
        public string Surname 
        {
            get => _surname;
            set
            {
                _surname = OnlyLetterString(value);
                RaisePropertyChanged(nameof(Surname));
            }
        }

        public string Email { get; set; }

        public string Title { get; set; }
        public string OkButtonText { get; set; }

        public RelayCommand OkCommand { get; protected set; }
        public RelayCommand CloseCommand { get; private set; }

        private string OnlyLetterString(string str)
        {
            return new string(str.Where(char.IsLetter).ToArray());
        }

        protected bool IsLegit()
        {
            return !string.IsNullOrEmpty(Name) &&
                !string.IsNullOrEmpty(Surname) &&
                !string.IsNullOrEmpty(Email) &&
                _regex.IsMatch(Email);
        }
        private readonly Regex _regex = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
    }
}
