#nullable enable
using Laundrygest_desktop.Data;
using Laundrygest_desktop.Model;
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
    public class CreateClientDialogViewModel : INotifyPropertyChanged
    {
        private readonly Client _client;
        private readonly ClientRepository _clientRepository;
        private string _firstNameTextBox;
        private string _lastNameTextBox;
        private string _telephoneTextBox;
        private string _emailTextBox;
        private string _postalCodeTextBox;
        private string _addressTextBox;
        private string _localityTextBox;
        private string _nifTextBox;
        private bool? _checkBoxState;
        private string _confirmButtonContent;

        private readonly bool _isUpdate = false;

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public CreateClientDialogViewModel(Client? client)
        {
            ConfirmButtonContent = "Crear";
            CheckBoxState = false;
            _clientRepository = new ClientRepository();
            CloseWindowCommand = new DelegateCommand<Window>(CloseWindow);
            CreateClientCommand = new DelegateCommand<Window>(CreateClient);

            if (client == null) return;
            _client = client;
            _isUpdate = true;
            ConfirmButtonContent = "Actualitzar";
            FirstNameTextBox = client.FirstName;
            LastNameTextBox = client.LastName;
            TelephoneTextBox = client.Telephone;
            EmailTextBox = client.Email;
            PostalCodeTextBox = client.PostalCode;
            AddressTextBox = client.Address;
            LocalityTextBox = client.Locality;

            if (client.Nif == null) return;
            CheckBoxState = true;
            NifTextBox = client.Nif;
        }

        public string ConfirmButtonContent
        {
            get => _confirmButtonContent;
            set
            {
                _confirmButtonContent = value;
                OnPropertyChanged();
            }
        }

        public bool? CheckBoxState
        {
            get => _checkBoxState;
            set
            {
                _checkBoxState = value;
                OnPropertyChanged();
            }
        }
        public string FirstNameTextBox
        {
            get => _firstNameTextBox;
            set
            {
                _firstNameTextBox = value;
                OnPropertyChanged();
            }
        }

        public string LastNameTextBox
        {
            get => _lastNameTextBox;
            set
            {
                _lastNameTextBox = value;
                OnPropertyChanged();
            }
        }
        public string TelephoneTextBox
        {
            get => _telephoneTextBox;
            set
            {
                _telephoneTextBox = value;
                OnPropertyChanged();
            }
        }

        public string EmailTextBox
        {
            get => _emailTextBox;
            set
            {
                _emailTextBox = value;
                OnPropertyChanged();
            }
        }

        public string AddressTextBox
        {
            get => _addressTextBox;
            set
            {
                _addressTextBox = value;
                OnPropertyChanged();
            }
        }

        public string PostalCodeTextBox
        {
            get => _postalCodeTextBox;
            set
            {
                _postalCodeTextBox = value;
                OnPropertyChanged();
            }
        }

        public string LocalityTextBox
        {
            get => _localityTextBox;
            set
            {
                _localityTextBox = value;
                OnPropertyChanged();
            }
        }

        public string NifTextBox
        {
            get => _nifTextBox;
            set
            {
                _nifTextBox = value;
                OnPropertyChanged();
            }
        }       


        public ICommand CreateClientCommand { get; }
        public ICommand CloseWindowCommand { get; }
        public void CloseWindow(Window window)
        {
            window?.Close();
        }

        public void CreateClient(Window window)
        {
            var c = _isUpdate ? _client : new Client();
            c.FirstName = FirstNameTextBox;
            c.LastName = LastNameTextBox;
            c.Email = EmailTextBox;            
            c.Telephone = TelephoneTextBox;
            c.Address = AddressTextBox;
            c.PostalCode = PostalCodeTextBox;
            c.Locality = LocalityTextBox;
            c.Nif = NifTextBox;
            if (_isUpdate)
            {
                var result = _clientRepository.PutClient(c.Code, c).Result;
                if (result)
                {
                    var response = MessageBox.Show("S'ha actualitzat correctament", "Operació completada", MessageBoxButton.OK);
                    if (response == MessageBoxResult.OK)
                    {
                        window?.Close();
                    }
                }
                else
                {
                    MessageBox.Show("No s'ha pogut actualitzar el client", "ERROR", MessageBoxButton.OK,
                        MessageBoxImage.Error);
                }
            }
            else
            {
                Client tempClient = _clientRepository.PostClient(c).Result;
                if (tempClient == null)
                {
                    MessageBox.Show("No s'ha pogut afegir el client, torna a probar-ho", "ERROR", MessageBoxButton.OK,
                        MessageBoxImage.Error);
                }
                else
                {
                    window?.Close();
                }
            }
        }
    }
}
