using Laundrygest_desktop.Data;
using Laundrygest_desktop.Model;
using Laundrygest_desktop.Views;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace Laundrygest_desktop.ViewModel
{

    public class SearchDialogViewModel : INotifyPropertyChanged
    {
        private ClientRepository clientRepository;
        private string _filterText;
        private ObservableCollection<Client> _clientList;
        public Action<Client> OnOptionSelected { get; set; }
        public SearchDialogViewModel()
        {
            clientRepository = new ClientRepository();
            SearchClient();
            CreateClientCommand = new DelegateCommand(OpenCreateClientDialog);
            SearchClientCommand = new DelegateCommand(SearchClient);
            SelectClientButtonCommand = new DelegateCommand<object>(ChooseClient);
        }

        public string filterText
        {
            get { return _filterText; }
            set
            {
                _filterText = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Client> clientList
        {
            get
            {
                return _clientList;
            }
            set
            {
                _clientList = value;
                OnPropertyChanged();
            }
        }             
        public ICommand CreateClientCommand { get; }

        public ICommand SelectClientButtonCommand { get; }

        public void OpenCreateClientDialog()
        {
            var dialog = new CreateClientDialog();
            dialog.ShowDialog();
            filterText = "";
            SearchClient();
        }       

        private void ChooseClient(object parameter)
        {
            var fila = parameter as Client;
            if (fila != null)
            {
                SelectClient(fila);
            }
        }

        public ICommand SearchClientCommand { get; }

        private void SearchClient()
        {
            clientList = clientRepository.GetClients(filterText).Result;
        }
        private void SelectClient(Client client)
        {
            OnOptionSelected?.Invoke(client);
        }        

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
