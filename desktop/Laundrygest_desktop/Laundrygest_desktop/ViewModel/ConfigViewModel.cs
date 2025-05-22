using Laundrygest_desktop.Data;
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
using Laundrygest_desktop.Model;
using Laundrygest_desktop.ViewModel.Dialogs;

namespace Laundrygest_desktop
{
    public class ConfigViewModel : INotifyPropertyChanged
    {
        private string _urlApiTextBox;
        private readonly Settings _settings;
        private int _daysDelayTextBox;
        private string _companyNameTextBox;
        private string _ownerNameTextBox;
        private string _ownerLastNameTextBox;
        private string _telephoneTextBox;
        private string _addressTextBox;
        private string _nifTextBox;
        private string _postalCodeTextBox;
        private List<string> _clerkList;
        public ConfigViewModel()
        {
            _settings = ConfigController.GetSettings();
            LoadSettings();

            ConnectCommand = new DelegateCommand(TryConnect);
            SaveSettings = new DelegateCommand(SaveConfig);
            ClerkSettings = new DelegateCommand(OpenClerkSettings);
        }

        private void LoadSettings()
        {
            UrlApiTextBox = _settings.ApiUrl ?? "";
            DaysDelayTextBox = _settings.DaysDelay;
            CompanyNameTextBox = _settings.Company.CompanyName ?? "";
            OwnerNameTextBox = _settings.Company.OwnerName ?? "";
            OwnerLastNameTextBox = _settings.Company.OwnerLastName ?? "";
            TelephoneTextBox = _settings.Company.Telephone ?? "";
            AddressTextBox = _settings.Company.Address ?? "";
            PostalCodeTextBox = _settings.Company.PostalCode ?? "";
            NifTextBox = _settings.Company.Nif ?? "";
            ClerkList = _settings.Clerks ?? new List<string>();
        }

        public List<string> ClerkList
        {
            get => _clerkList;
            set
            {
                _clerkList = value;
                OnPropertyChanged();
            }
        }

        public string PostalCodeTextBox
        {
            get => _postalCodeTextBox;
            set
            {
                _postalCodeTextBox = value;
                OnPropertyChanged(nameof(PostalCodeTextBox));
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

        public string AddressTextBox
        {
            get => _addressTextBox;
            set
            {
                _addressTextBox = value;
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

        public string OwnerLastNameTextBox
        {
            get => _ownerLastNameTextBox;
            set
            {
                _ownerLastNameTextBox = value;
                OnPropertyChanged();
            }
        }
        public string OwnerNameTextBox
        {
            get => _ownerNameTextBox;
            set
            {
                _ownerNameTextBox = value;
                OnPropertyChanged();
            }
        }

        public string CompanyNameTextBox
        {
            get => _companyNameTextBox;
            set
            {
                _companyNameTextBox = value;
                OnPropertyChanged();
            }
        }

        public int DaysDelayTextBox
        {
            get => _daysDelayTextBox;
            set
            {
                _daysDelayTextBox = value;
                OnPropertyChanged();
            }
        }
        public string UrlApiTextBox
        {
            get => _urlApiTextBox;
            set { _urlApiTextBox = value; OnPropertyChanged(); }
        }

        public ICommand ConnectCommand { get; }
        public ICommand SaveSettings { get; }
        public ICommand ClerkSettings { get; }

        public void OpenClerkSettings()
        {
            var vm = new ClerkCrudViewModel();
            vm.Items = new ObservableCollection<string>(ClerkList);
            var dialog = new ClerkCRUD_Dialog(vm);
            var result = dialog.ShowDialog();

            if (result==true)
            {
                ClerkList = vm.Items.ToList();
            }
        }

        public void SaveConfig()
        {
            _settings.ApiUrl = UrlApiTextBox;
            _settings.DaysDelay = DaysDelayTextBox;
            _settings.Company.CompanyName = CompanyNameTextBox;
            _settings.Company.OwnerName = OwnerNameTextBox;
            _settings.Company.OwnerLastName = OwnerLastNameTextBox;
            _settings.Company.Telephone = TelephoneTextBox;
            _settings.Company.Nif = NifTextBox;
            _settings.Company.Address = AddressTextBox;
            _settings.Company.PostalCode = PostalCodeTextBox;
            _settings.Clerks = ClerkList;
            ConfigController.SaveSettings(_settings);
        }

        public async void TryConnect()
        {
            var result = await BaseRepository.ConnectAsync(UrlApiTextBox);
            if (result)
            {
                BaseRepository.urlApi = UrlApiTextBox;
                MessageBox.Show("S'ha connectat a l'API correctament", "", MessageBoxButton.OK);
            }
            else
            {
                MessageBox.Show("No s'ha pogut connectar a l'API", "", MessageBoxButton.OK);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
