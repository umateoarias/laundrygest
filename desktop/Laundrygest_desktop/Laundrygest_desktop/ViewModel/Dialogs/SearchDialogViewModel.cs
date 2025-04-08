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
using System.Windows.Input;

namespace Laundrygest_desktop.ViewModel
{

    public class SearchDialogViewModel : INotifyPropertyChanged
    {
        private ClientRepository clientRepository;
        private int _mode;
        private Visibility _buttonVisibility;
        private TaskCompletionSource<Client> _tcs;
        private string _filterText;
        private List<Client> _clientList;

        public SearchDialogViewModel() { }

        public SearchDialogViewModel(int mode)
        {
            _mode = mode;
            clientRepository = new ClientRepository();
            CreateClientCommand = new DelegateCommand(OpenCreateClientDialog);
            SelectClientCommand = new DelegateCommand<Client>(SelectClient);
            SelectClientButtonCommand = new DelegateCommand<object>(ChooseClient);
        }

        public string filterText
        {
            get { return _filterText; }
            set
            {
                _filterText = value;
                OnPropertyChanged();
                clientList = clientRepository.GetClients(_filterText).Result;
            }
        }

        public List<Client> clientList
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
        public int mode
        {
            get { return _mode; }
            set
            {
                _mode = value;
                OnPropertyChanged();
            }
        }

        public Visibility buttonVisibility
        {
            get { return _buttonVisibility; }
            set { _buttonVisibility = value; OnPropertyChanged(); }
        }


        public ICommand CreateClientCommand { get; }

        public ICommand SelectClientButtonCommand { get; }

        public void OpenCreateClientDialog()
        {
            var dialog = new CreateClientDialog();
            dialog.ShowDialog();
            filterText = "";
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

        private void ChooseClient(object parameter)
        {
            var fila = parameter as Client;
            if (fila != null)
            {
                MessageBox.Show("Confirmar seleccio", "Confirm", MessageBoxButton.YesNo);
            }
        }

        public ICommand SelectClientCommand { get; }
        private void SelectClient(Client client)
        {
            _tcs.TrySetResult(client);
        }
        public Task<Client> BuscarAsync()
        {
            _tcs = new TaskCompletionSource<Client>();
            return _tcs.Task;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
