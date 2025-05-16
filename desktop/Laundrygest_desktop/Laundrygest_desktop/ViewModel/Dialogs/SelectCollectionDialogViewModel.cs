using Laundrygest_desktop.Data.Repositories;
using Laundrygest_desktop.Model;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Laundrygest_desktop.ViewModel.Dialogs
{
    public class SelectCollectionDialogViewModel : INotifyPropertyChanged
    {
        private readonly CollectionRepository _repository;
        private Collection _selectedCollection;
        private ObservableCollection<Collection> _collections;
        public Action<Collection> OnCollectionSelected { get; set; }
        public SelectCollectionDialogViewModel(Client selectedClient)
        {
            _repository = new CollectionRepository();
            Collections = _repository.GetCollections(selectedClient).Result;
            SelectCollectionButtonCommand = new DelegateCommand<object>(ChooseCollection);
        }
        public Collection selectedCollection
        {
            get => _selectedCollection;
            set
            {
                _selectedCollection = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<Collection> Collections
        {
            get => _collections;
            set
            {
                _collections = value;
                OnPropertyChanged();
            }
        }

        private void ChooseCollection(object parameter)
        {
            if (parameter is Collection fila)
            {
                CollectionSelected(fila);
            }
        }

        public void CollectionSelected(Collection collection)
        {
            OnCollectionSelected?.Invoke(collection);
        }

        public ICommand SelectCollectionButtonCommand { get; }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
