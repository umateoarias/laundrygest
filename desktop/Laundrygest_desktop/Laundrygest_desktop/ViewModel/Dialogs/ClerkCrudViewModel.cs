using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Prism.Commands;

namespace Laundrygest_desktop.ViewModel.Dialogs
{
    public class ClerkCrudViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<string> Items { get; set; }
            = new ObservableCollection<string>();

        private string _newItem;
        public string NewItem
        {
            get => _newItem;
            set { _newItem = value; OnPropertyChanged(); }
        }

        private string _selectedItem;
        public string SelectedItem
        {
            get => _selectedItem;
            set { _selectedItem = value; OnPropertyChanged(); }
        }

        public ICommand AddCommand { get; }
        public ICommand RemoveCommand { get; }
        public ICommand OkCommand { get; }

        public event Action<bool> RequestClose;

        public ClerkCrudViewModel()
        {
            AddCommand = new DelegateCommand(AddItem);
            RemoveCommand = new DelegateCommand(RemoveItem);
            OkCommand = new DelegateCommand(CloseWindow);
        }

        private void CloseWindow()
        {
            RequestClose?.Invoke(true);
        }


        private void AddItem()
        {
            Items.Add(NewItem.Trim());
            NewItem = string.Empty;
        }

        private void RemoveItem()
        {
            if (SelectedItem != null)
                Items.Remove(SelectedItem);
        } 

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
