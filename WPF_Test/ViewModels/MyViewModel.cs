using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WPF_Test.ViewModels
{
    public class MyViewModel : ViewModelBase
    {
        public Window View { get; private set; }
        public MyViewModel(Window view)
        {            
            view.DataContext = this;
            View = view;
        }

        protected void Close()
        {
            View.Close();
        }
    }
}
