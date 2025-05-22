using Prism.Commands;
using QuestPDF.Infrastructure;
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
            QuestPDF.Settings.License = LicenseType.Community;
            NavigateCommand = new DelegateCommand<string>(Navigate);
            SelectedView = new ClothesViewModel();
        }

        private void Navigate(string destination)
        {
            SelectedView = destination switch
            {
                "clothes" => new ClothesViewModel(),
                "quilts" => new QuiltsViewModel(),
                "stats" => new StatsViewModel(),
                "app" => new AppViewModel(),
                "config" => new ConfigViewModel(),
                _ => SelectedView
            };
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
