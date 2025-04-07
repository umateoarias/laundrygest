using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Laundrygest_desktop.ViewModel
{
    public class CreateClientDialogViewModel
    {
        public CreateClientDialogViewModel()
        {
            CloseWindowCommand = new DelegateCommand<Window>(CloseWindow);
        }

        public ICommand CloseWindowCommand { get; }

        public void CloseWindow(Window window) {
            window?.Close();
        }
    }
}
