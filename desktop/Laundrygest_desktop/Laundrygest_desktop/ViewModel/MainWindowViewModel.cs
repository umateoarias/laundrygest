using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Laundrygest_desktop
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private object _selectedView;
        public object SelectedView
        {
            get => _selectedView;
            set
            {
                _selectedView = value;
                OnPropertyChanged();
            }
        }

        public ICommand NavigateCommand { get; }

        public MainWindowViewModel()
        {
            NavigateCommand = new RelayCommand<string>(Navigate);
            SelectedView = new ClothesViewModel();
        }

        private void Navigate(string destination)
        {
            switch (destination)
            {
                case "0":
                    SelectedView = new ClothesViewModel();
                    break;
                case "1":
                    SelectedView = new QuiltsViewModel();
                    break;
                case "2":
                    SelectedView = new StatsViewModel();
                    break;
                case "3":
                    SelectedView = new AppViewModel();
                    break;
                case "4":
                    SelectedView = new ConfigViewModel();
                    break;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
