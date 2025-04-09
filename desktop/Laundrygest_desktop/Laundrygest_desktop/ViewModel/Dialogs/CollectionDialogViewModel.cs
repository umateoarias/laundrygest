using Laundrygest_desktop.Model;
using Laundrygest_desktop.Views;
using Laundrygest_desktop.Views.Dialogs;
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

namespace Laundrygest_desktop.ViewModel
{
    public class CollectionDialogViewModel : INotifyPropertyChanged
    {
        private Client _collectionClient;
        private string _clerkTextBox;
        private string _clientFirstNameTextBox;
        private string _clientLastNameTextBox;
        private string _clientTelephoneTextBox;
        private string _clientNifTextBox;
        private string _basePriceTextBox;
        private string _taxAmountTextBox;
        private string _totalPriceTextBox;
        private ObservableCollection<CollectionItem> _collectionItems;
        public CollectionDialogViewModel()
        {
            SearchClientCommand = new DelegateCommand(OpenSearchClientDialog);
            SelectClerkCommand = new DelegateCommand(OpenClerkWindow);
        }

        public string ClerkTextBox
        {
            get { return _clerkTextBox; }
            set
            {
                _clerkTextBox = value;
                OnPropertyChanged();
            }
        }
        public string ClientFirstNameTextBox
        {
            get { return _clientFirstNameTextBox; }
            set
            {
                _clientFirstNameTextBox = value;
                OnPropertyChanged();
            }
        }

        public string ClientLastNameTextBox
        {
            get { return _clientLastNameTextBox; }
            set
            {
                _clientLastNameTextBox = value;
                OnPropertyChanged();
            }
        }
        public string ClientTelephoneTextBox
        {
            get { return _clientTelephoneTextBox; }
            set
            {
                _clientTelephoneTextBox = value;
                OnPropertyChanged();
            }
        }
        public string ClientNifTextBox
        {
            get { return _clientNifTextBox; }
            set
            {
                _clientNifTextBox = value;
                OnPropertyChanged();
            }
        }
        public string BasePriceTextBox
        {
            get { return _basePriceTextBox; }
            set
            {
                _basePriceTextBox = value;
                OnPropertyChanged();
            }
        }
        public string TaxAmountTextBox
        {
            get { return _taxAmountTextBox; }
            set
            {
                _taxAmountTextBox = value;
                OnPropertyChanged();
            }
        }
        public string TotalPriceTextBox
        {
            get { return _totalPriceTextBox; }
            set
            {
                _totalPriceTextBox = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<CollectionItem> collectionItems
        {
            get { return _collectionItems; }
            set
            {
                _collectionItems = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ICommand SelectClerkCommand { get; }
        public ICommand SearchClientCommand { get; }

        public void OpenSearchClientDialog()
        {
            var dialog = new SearchClientDialog();
            dialog.WindowState = System.Windows.WindowState.Maximized;
            bool? result = dialog.ShowDialog();

            if (result == true) {
                _collectionClient = dialog.SelectedOption;
                ClientFirstNameTextBox = _collectionClient.FirstName;
                ClientLastNameTextBox = _collectionClient.LastName;
                ClientTelephoneTextBox = _collectionClient.Telephone;
                ClientNifTextBox = _collectionClient.Nif;
            }
            
        }

        public void OpenClerkWindow()
        {
            var dialog = new SelectOptionButtonsDialog(new List<string>() { "Joan", "Maria" });
            bool? result = dialog.ShowDialog();

            if(result == true)
            {
                ClerkTextBox = dialog.SelectedOption;
            }
        }

    }
}
