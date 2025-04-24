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
        private readonly ClientRepository _clientRepository;

        private Collection collection;
        private CollectionItem _selectedCollectionItem;
        private int collectionType;
        private Client _collectionClient;
        public bool isDelivery;
        private Visibility _btnVisibility = Visibility.Collapsed;

        private string _btnContent1;
        private string _btnContent2;
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
        public CollectionDialogViewModel(bool isQuilts, Collection? deliveryCollection)
        {
            btnVisibility = new Visibility();
            collectionType = isQuilts ? 2 : 1;
            _repository = new CollectionRepository();
            _clientRepository = new ClientRepository();
            if (deliveryCollection == null)
            {
                isDelivery = false;
                collectionItems = new ObservableCollection<CollectionItem>();
                DueDatePicker = CreatedAtDatePicker.AddDays(7);
                Collection c = new Collection() { CreatedAt = CreatedAtDatePicker, CollectionTypeCode = collectionType, CollectionItems = collectionItems };
                collection = _repository.PostCollection(c).Result;
            }
            else
            {
                isDelivery = true;
                collection = deliveryCollection;
                _collectionClient = _clientRepository.GetClient((int)deliveryCollection.ClientCode).Result;
                // CLERK NOT SAVED                
            }
            setFormText();
            collectionItems.CollectionChanged += CollectionItem_PropertyChanged;
            setCommands();
        }

        private void setFormText()
        {
            if (isDelivery)
            {
                ClientFirstNameTextBox = _collectionClient.FirstName;
                ClientLastNameTextBox = _collectionClient.LastName;
                ClientTelephoneTextBox = _collectionClient.Telephone;
                ClientNifTextBox = _collectionClient.Nif;
                TotalPriceTextBox = collection.Total == null ? 0.0m : (decimal)collection.Total;
                TaxAmountTextBox = collection.TaxAmount == null ? 0.0m : (decimal)collection.TaxAmount;
                BasePriceTextBox = collection.TaxBase == null ? 0.0m : (decimal)collection.TaxBase;
                CreatedAtDatePicker = collection.CreatedAt;
                DueDatePicker = collection.DueDate == null ? CreatedAtDatePicker.AddDays(7) : (DateTime)collection.DueDate;
                collectionItems = new ObservableCollection<CollectionItem>(collection.CollectionItems);
                btnVisibility = Visibility.Collapsed;
                btnContent1 = "Entregar";
                btnContent2 = "Eliminar entregat";
            }
            else
            {
                btnVisibility = Visibility.Visible;
                btnContent1 = "Afegir prenda";
                btnContent2 = "Eliminar prenda";
                OpenAddPieceCommand = new DelegateCommand(OpenAddPiece);
                DeleteSelectedCommand = new DelegateCommand(DeleteSelectedPiece);
            }
        }

        private void setCommands()
        {
            SearchClientCommand = new DelegateCommand(OpenSearchClientDialog);
            SelectClerkCommand = new DelegateCommand(OpenClerkWindow);            
            PaymentDialogCommand = new DelegateCommand(OpenPaymentDialog);
            FinishCommand = new DelegateCommand(FinishCollection);
        }

        private void CollectionItem_PropertyChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            getTotalPrice();
        }
        public string btnContent1
        {
            get
            {
                return _btnContent1;
            }
            set
            {
                _btnContent1 = value;
                OnPropertyChanged();
            }
        }
        public string btnContent2
        {
            get { return _btnContent2; }
            set
            {
                _btnContent2 = value;
                OnPropertyChanged();
            }
        }

        public Visibility btnVisibility
        {
            get
            {
                return _btnVisibility;
            }
            set
            {
                _btnVisibility = value;
                OnPropertyChanged();
            }
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

        public ICommand SelectClerkCommand { get; private set; }
        public ICommand SearchClientCommand { get; private set; }
        public ICommand OpenAddPieceCommand { get; private set; }
        public ICommand DeleteSelectedCommand { get; private set; }
        public ICommand PaymentDialogCommand { get; private set; }
        public ICommand FinishCommand { get; private set; }
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
