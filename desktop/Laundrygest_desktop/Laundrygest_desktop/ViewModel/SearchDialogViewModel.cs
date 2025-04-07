using Laundrygest_desktop.Views;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Laundrygest_desktop.ViewModel
{
    public class SearchDialogViewModel
    {
        private int _mode;
        public int mode
        {
            get { return _mode; }
            set
            {
                _mode = value;
                OnPropertyChanged();
            }
        }        

        private Visibility _buttonVisibility;
        public Visibility buttonVisibility
        {
            get { return _buttonVisibility; }
            set { _buttonVisibility = value; OnPropertyChanged(); }
        }
        public SearchDialogViewModel()
        {            
            CreateClientCommand = new DelegateCommand(OpenCreateClientDialog);
        }

        public SearchDialogViewModel(int mode)
        {
            _mode = mode;
        }

        public ICommand CreateClientCommand { get; }

        public void OpenCreateClientDialog()
        {
            var dialog = new CreateClientDialog();
            dialog.Show();
        }

        public void UpdateButtonVisibility()
        {
            if (mode == 1)
            {
                buttonVisibility = Visibility.Visible;
            }
            else
            {
                buttonVisibility = Visibility.Collapsed;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
