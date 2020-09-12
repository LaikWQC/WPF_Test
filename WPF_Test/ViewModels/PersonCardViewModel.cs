using GalaSoft.MvvmLight.CommandWpf;
using System.Windows;

namespace WPF_Test.ViewModels
{
    public abstract class PersonCardViewModel : MyViewModel
    {
        public PersonCardViewModel(Window view) : base(view)
        {
            CloseCommand = new RelayCommand(Close);
        }

        public string Name { get; set; }
        public string Patronymic { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }

        public string Title { get; set; }
        public string OkButtonText { get; set; }

        public RelayCommand OkCommand { get; protected set; }
        public RelayCommand CloseCommand { get; private set; }
    }
}
