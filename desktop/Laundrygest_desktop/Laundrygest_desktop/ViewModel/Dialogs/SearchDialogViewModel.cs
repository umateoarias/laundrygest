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
        private readonly ClientRepository _clientRepository;
        private string _filterText;
        private ObservableCollection<Client> _clientList;
        public Action<Client> OnOptionSelected { get; set; }
        public SearchDialogViewModel()
        {
            _clientRepository = new ClientRepository();
            SearchClient();
            CreateClientCommand = new DelegateCommand(OpenCreateClientDialog);
            SearchClientCommand = new DelegateCommand(SearchClient);
            SelectClientButtonCommand = new DelegateCommand<object>(ChooseClient);
            EnterPressedCommand = new DelegateCommand(TextBox_KeyUp);
        }

        public string FilterText
        {
            get => _filterText;
            set
            {
                _filterText = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Client> ClientList
        {
            get => _clientList;
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
            var dialog = new CreateClientDialog(null);
            dialog.ShowDialog();
            FilterText = "";
            SearchClient();
        }

        private void ChooseClient(object parameter)
        {
            if (parameter is Client fila)
            {
                SelectClient(fila);
            }
        }
        public ICommand EnterPressedCommand { get; }
        public ICommand SearchClientCommand { get; }

        private void SearchClient()
        {
            ClientList = _clientRepository.GetClients(FilterText).Result;
        }
        private void SelectClient(Client client)
        {
            OnOptionSelected?.Invoke(client);
        }

        private void TextBox_KeyUp()
        {
            SearchClient();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
