using Laundrygest_desktop.Data;
using Laundrygest_desktop.Data.Repositories;
using Laundrygest_desktop.Model;
using Laundrygest_desktop.Views;
using Laundrygest_desktop.Views.Dialogs;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Laundrygest_desktop.ViewModel
{
    public class CollectionDialogViewModel : INotifyPropertyChanged
    {
        private readonly CollectionRepository _repository;
        private Collection collection;
        private CollectionItem _selectedCollectionItem;
        private int collectionType;
        private Client _collectionClient;
        private string _clerkTextBox;
        private string _clientFirstNameTextBox;
        private string _clientLastNameTextBox;
        private string _clientTelephoneTextBox;
        private string _clientNifTextBox;
        private decimal _basePriceTextBox;
        private decimal _taxAmountTextBox;
        private decimal _totalPriceTextBox;
        private DateTime _createdAtDatePicker = DateTime.Now;
        private DateTime _dueDatePicker;
        private ObservableCollection<CollectionItem> _collectionItems;
        public Action CloseAction { get; set; }
        public CollectionDialogViewModel(bool isQuilts)
        {
            collectionType = isQuilts ? 2 : 1;
            _repository = new CollectionRepository();
            collectionItems = new ObservableCollection<CollectionItem>();
            DueDatePicker = CreatedAtDatePicker.AddDays(7);
            Collection c = new Collection() { CreatedAt = CreatedAtDatePicker, CollectionTypeCode = collectionType, CollectionItems = collectionItems };
            collection = _repository.PostCollection(c).Result;
            collectionItems.CollectionChanged += CollectionItem_PropertyChanged;
            SearchClientCommand = new DelegateCommand(OpenSearchClientDialog);
            SelectClerkCommand = new DelegateCommand(OpenClerkWindow);
            OpenAddPieceCommand = new DelegateCommand(OpenAddPiece);
            DeleteSelectedCommand = new DelegateCommand(DeleteSelectedPiece);
            PaymentDialogCommand = new DelegateCommand(OpenPaymentDialog);
            FinishCommand = new DelegateCommand(FinishCollection);
        }

        private void CollectionItem_PropertyChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            getTotalPrice();
        }

        public DateTime CreatedAtDatePicker
        {
            get { return _createdAtDatePicker; }
            set
            {
                _createdAtDatePicker = value;
                OnPropertyChanged();
            }
        }

        public DateTime DueDatePicker
        {
            get { return _dueDatePicker; }
            set
            {
                _dueDatePicker = value;
                OnPropertyChanged();
            }
        }

        public CollectionItem SelectedCollectionItem
        {
            get
            {
                return _selectedCollectionItem;
            }
            set
            {
                _selectedCollectionItem = value;
                OnPropertyChanged();
                OnPropertyChanged();
            }
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
        public decimal BasePriceTextBox
        {
            get { return _basePriceTextBox; }
            set
            {
                _basePriceTextBox = value;
                OnPropertyChanged();
            }
        }
        public decimal TaxAmountTextBox
        {
            get { return _taxAmountTextBox; }
            set
            {
                _taxAmountTextBox = value;
                OnPropertyChanged();
            }
        }
        public decimal TotalPriceTextBox
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
        public void getTotalPrice()
        {
            decimal total = 0;
            foreach (var item in _collectionItems)
            {
                var pl = item.PricelistCodeNavigation;
                var num = item.NumPieces;
                if (pl != null && num > 0)
                {
                    total += pl.UnitPrice * num;
                }
            }
            TotalPriceTextBox = total;
            BasePriceTextBox = total * 0.79m;
            TaxAmountTextBox = total * 0.21m;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ICommand SelectClerkCommand { get; }
        public ICommand SearchClientCommand { get; }
        public ICommand OpenAddPieceCommand { get; }
        public ICommand DeleteSelectedCommand { get; }
        public ICommand PaymentDialogCommand { get; }
        public ICommand FinishCommand { get; }
        public void OpenSearchClientDialog()
        {
            var dialog = new SearchClientDialog();
            dialog.WindowState = System.Windows.WindowState.Maximized;
            bool? result = dialog.ShowDialog();

            if (result == true)
            {
                _collectionClient = dialog.SelectedOption;
                ClientFirstNameTextBox = _collectionClient.FirstName;
                ClientLastNameTextBox = _collectionClient.LastName;
                ClientTelephoneTextBox = _collectionClient.Telephone;
                ClientNifTextBox = _collectionClient.Nif;
            }

        }

        public void OpenAddPiece()
        {
            var dialog = new SelectPieceDialog(collectionType);
            bool? result = dialog.ShowDialog();

            if (result == true)
            {
                CollectionItem ci = new CollectionItem();
                ci.NumPieces = 1;
                ci.PricelistCodeNavigation = dialog.SelectedOption;
                ci.PricelistCodeNavigation.PropertyChanged += PricelistCodeNavigation_PropertyChanged;
                ci.PricelistCode = dialog.SelectedOption.Code;
                ci.CollectionNumberNavigation = collection;
                ci.CollectionNumber = collection.Number;
                ci.PropertyChanged += PricelistCodeNavigation_PropertyChanged;
                collectionItems.Add(ci);
            }
        }

        public void OpenPaymentDialog()
        {
            var dialog = new PaymentDialog(TotalPriceTextBox);
            bool? result = dialog.ShowDialog();

            if (result == true)
            {
                collection.DueTotal = dialog.RemainingAmount;
                collection.PaymentMode = dialog.PaymentMode;
            }
        }

        public void FinishCollection()
        {
            collection.CollectionItems = collectionItems;
            collection.Total = TotalPriceTextBox;
            collection.DueDate = DueDatePicker;
            collection.CreatedAt = CreatedAtDatePicker;
            collection.TaxAmount = TaxAmountTextBox;
            collection.TaxBase = BasePriceTextBox;
            collection.ClientCodeNavigation = _collectionClient;
            collection.ClientCode = _collectionClient.Code;
            var result = MessageBox.Show("Vols guardar aquesta recollida?", "Finalitzar", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes && _repository.PutCollection(collection.Number, collection).Result)
            {
                CloseAction?.Invoke();
            }
        }

        private void PricelistCodeNavigation_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            getTotalPrice();
        }

        public void DeleteSelectedPiece()
        {
            if (SelectedCollectionItem != null)
            {
                collectionItems.Remove(SelectedCollectionItem);
            }
        }

        public void OpenClerkWindow()
        {
            var dialog = new SelectOptionButtonsDialog(new List<string>() { "Joan", "Maria" });
            bool? result = dialog.ShowDialog();

            if (result == true)
            {
                ClerkTextBox = dialog.SelectedOption;
            }
        }

    }
}
