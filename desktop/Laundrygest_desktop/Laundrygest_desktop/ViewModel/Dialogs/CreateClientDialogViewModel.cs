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
        private ClientRepository clientRepository;
        private string _firstNameTextBox;
        private string _lastNameTextBox;
        private string _telephoneTextBox;
        private string _emailTextBox;
        private string _postalCodeTextBox;
        private string _addressTextBox;
        private string _localityTextBox;
        private string _nifTextBox;

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public CreateClientDialogViewModel()
        {
            clientRepository = new ClientRepository();
            CloseWindowCommand = new DelegateCommand<Window>(CloseWindow);
            CreateClientCommand = new DelegateCommand<Window>(CreateClient);
        }
        public string FirstNameTextBox
        {
            get
            {
                return _firstNameTextBox;
            }
            set
            {
                _firstNameTextBox = value;
                OnPropertyChanged();
            }
        }

        public string LastNameTextBox
        {
            get
            {
                return _lastNameTextBox;
            }
            set
            {
                _lastNameTextBox = value;
                OnPropertyChanged();
            }
        }
        public string TelephoneTextBox
        {
            get
            {
                return _telephoneTextBox;
            }
            set
            {
                _telephoneTextBox = value;
                OnPropertyChanged();
            }
        }

        public string EmailTextBox
        {
            get
            {
                return _emailTextBox;
            }
            set
            {
                _emailTextBox = value;
                OnPropertyChanged();
            }
        }

        public string AddressTextBox
        {
            get
            {
                return _addressTextBox;
            }
            set
            {
                _addressTextBox = value;
                OnPropertyChanged();
            }
        }

        public string PostalCodeTextBox
        {
            get
            {
                return _postalCodeTextBox;
            }
            set
            {
                _postalCodeTextBox = value;
                OnPropertyChanged();
            }
        }

        public string LocalityTextBox
        {
            get
            {
                return _localityTextBox;
            }
            set
            {
                _localityTextBox = value;
                OnPropertyChanged();
            }
        }

        public string NifTextBox
        {
            get
            {
                return _nifTextBox;
            }
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
            Client c = new Client();
            c.FirstName = FirstNameTextBox;
            c.LastName = LastNameTextBox;
            c.Email = EmailTextBox;            
            c.Telephone = TelephoneTextBox;
            c.Address = AddressTextBox;
            c.PostalCode = PostalCodeTextBox;
            c.Locality = LocalityTextBox;
            c.Nif = NifTextBox;
            Client tempClient = clientRepository.PostClient(c).Result;
            if (tempClient == null)
            {
                MessageBox.Show("No s'ha pogut afegir el client, torna a probar-ho","ERROR",MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                window?.Close();
            }
        }
    }
}
