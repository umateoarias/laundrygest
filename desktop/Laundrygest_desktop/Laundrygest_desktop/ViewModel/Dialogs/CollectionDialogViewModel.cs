using Laundrygest_desktop.Data;
using Laundrygest_desktop.Data.Repositories;
using Laundrygest_desktop.Model;
using Laundrygest_desktop.ViewModel.Dialogs;
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
        private readonly CollectionRepository _collectionRepository;
        private readonly DeliveryRepository _deliveryRepository;

        private readonly Delivery _delivery;
        private readonly Collection _collection;
        private CollectionItemViewModel _selectedCollectionItem;
        private readonly int _collectionType;
        private Client _collectionClient;
        private readonly bool _isDelivery;
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
        private ObservableCollection<CollectionItemViewModel> _collectionItems;
        public Action CloseAction { get; set; }
        public CollectionDialogViewModel(bool isQuilts, Collection? deliveryCollection)
        {
            BtnVisibility = new Visibility();
            _collectionType = isQuilts ? 2 : 1;
            _collectionRepository = new CollectionRepository();
            var clientRepository = new ClientRepository();
            _deliveryRepository = new DeliveryRepository();
            if (deliveryCollection == null)
            {
                _isDelivery = false;
                CollectionItems = new ObservableCollection<CollectionItemViewModel>();
                DueDatePicker = CreatedAtDatePicker.AddDays(7);
                var newCollectionItems = CollectionItems.Select(x => x.Model).ToList();
                Collection c = new Collection() { CreatedAt = CreatedAtDatePicker, CollectionTypeCode = _collectionType, CollectionItems = newCollectionItems };
                _collection = _collectionRepository.PostCollection(c).Result;
            }
            else
            {
                _isDelivery = true;
                _collection = deliveryCollection;
                if (deliveryCollection.ClientCode != null)
                    _collectionClient = clientRepository.GetClient((int)deliveryCollection.ClientCode).Result;
                Delivery d = new Delivery() { DeliveryDate = DateTime.Now };
                _delivery = _deliveryRepository.PostDelivery(d).Result;
                // CLERK NOT SAVED                
            }
            SetFormText();
            CollectionItems.CollectionChanged += CollectionItem_PropertyChanged;
            SetCommands();
        }

        private void SetFormText()
        {
            if (_isDelivery)
            {
                ClientFirstNameTextBox = _collectionClient.FirstName;
                ClientLastNameTextBox = _collectionClient.LastName;
                ClientTelephoneTextBox = _collectionClient.Telephone;
                ClientNifTextBox = _collectionClient.Nif;
                TotalPriceTextBox = _collection.Total == null ? 0.0m : (decimal)_collection.Total;
                TaxAmountTextBox = _collection.TaxAmount == null ? 0.0m : (decimal)_collection.TaxAmount;
                BasePriceTextBox = _collection.TaxBase == null ? 0.0m : (decimal)_collection.TaxBase;
                CreatedAtDatePicker = _collection.CreatedAt;
                DueDatePicker = _collection.DueDate == null ? CreatedAtDatePicker.AddDays(7) : (DateTime)_collection.DueDate;
                CollectionItems = new ObservableCollection<CollectionItemViewModel>(_collection.CollectionItems.Select(x => new CollectionItemViewModel(x)));
                BtnVisibility = Visibility.Collapsed;
                BtnContent1 = "Entregar";
                BtnContent2 = "Eliminar entregat";
                OpenAddPieceCommand = new DelegateCommand(MarkCollectionItem);
                DeleteSelectedCommand = new DelegateCommand(UnmarkCollectionItem);
                FinishCommand = new DelegateCommand(CloseDelivery);
            }
            else
            {
                BtnVisibility = Visibility.Visible;
                BtnContent1 = "Afegir prenda";
                BtnContent2 = "Eliminar prenda";
                OpenAddPieceCommand = new DelegateCommand(OpenAddPiece);
                DeleteSelectedCommand = new DelegateCommand(DeleteSelectedPiece);
                FinishCommand = new DelegateCommand(FinishCollection);
            }
        }

        private void CloseDelivery()
        {
            if (_collection.DueTotal == 0)
            {
                _delivery.CollectionItems = CollectionItems.Where(x => x.IsMarked).Select(x => x.Model).ToList();
                var result = MessageBox.Show("Vols guardar aquest lliurament?", "Finalitzar", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    if (_deliveryRepository.PutDelivery(_delivery.Number, _delivery).Result)
                    {
                        CloseAction?.Invoke();
                    }
                    else
                    {
                        MessageBox.Show("Error guardant el lliurament", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Pagament pendent", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void UnmarkCollectionItem()
        {
            if (SelectedCollectionItem != null && !SelectedCollectionItem.HasDeliveryNumber && SelectedCollectionItem.IsMarked)
            {
                SelectedCollectionItem.IsMarked = false;
            }
        }

        private void MarkCollectionItem()
        {
            if (SelectedCollectionItem != null && !SelectedCollectionItem.HasDeliveryNumber && !SelectedCollectionItem.IsMarked)
            {
                SelectedCollectionItem.IsMarked = true;
            }
        }

        private void SetCommands()
        {
            SearchClientCommand = new DelegateCommand(OpenSearchClientDialog);
            SelectClerkCommand = new DelegateCommand(OpenClerkWindow);
            PaymentDialogCommand = new DelegateCommand(OpenPaymentDialog);
        }

        private void CollectionItem_PropertyChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            GetTotalPrice();
        }
        public string BtnContent1
        {
            get => _btnContent1;
            set
            {
                _btnContent1 = value;
                OnPropertyChanged();
            }
        }
        public string BtnContent2
        {
            get => _btnContent2;
            set
            {
                _btnContent2 = value;
                OnPropertyChanged();
            }
        }

        public Visibility BtnVisibility
        {
            get => _btnVisibility;
            set
            {
                _btnVisibility = value;
                OnPropertyChanged();
            }
        }


        public DateTime CreatedAtDatePicker
        {
            get => _createdAtDatePicker;
            set
            {
                _createdAtDatePicker = value;
                OnPropertyChanged();
            }
        }

        public DateTime DueDatePicker
        {
            get => _dueDatePicker;
            set
            {
                _dueDatePicker = value;
                OnPropertyChanged();
            }
        }

        public CollectionItemViewModel SelectedCollectionItem
        {
            get => _selectedCollectionItem;
            set
            {
                _selectedCollectionItem = value;
                OnPropertyChanged();
                OnPropertyChanged();
            }
        }

        public string ClerkTextBox
        {
            get => _clerkTextBox;
            set
            {
                _clerkTextBox = value;
                OnPropertyChanged();
            }
        }
        public string ClientFirstNameTextBox
        {
            get => _clientFirstNameTextBox;
            set
            {
                _clientFirstNameTextBox = value;
                OnPropertyChanged();
            }
        }

        public string ClientLastNameTextBox
        {
            get => _clientLastNameTextBox;
            set
            {
                _clientLastNameTextBox = value;
                OnPropertyChanged();
            }
        }
        public string ClientTelephoneTextBox
        {
            get => _clientTelephoneTextBox;
            set
            {
                _clientTelephoneTextBox = value;
                OnPropertyChanged();
            }
        }
        public string ClientNifTextBox
        {
            get => _clientNifTextBox;
            set
            {
                _clientNifTextBox = value;
                OnPropertyChanged();
            }
        }
        public decimal BasePriceTextBox
        {
            get => _basePriceTextBox;
            set
            {
                _basePriceTextBox = value;
                OnPropertyChanged();
            }
        }
        public decimal TaxAmountTextBox
        {
            get => _taxAmountTextBox;
            set
            {
                _taxAmountTextBox = value;
                OnPropertyChanged();
            }
        }
        public decimal TotalPriceTextBox
        {
            get => _totalPriceTextBox;
            set
            {
                _totalPriceTextBox = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<CollectionItemViewModel> CollectionItems
        {
            get { return _collectionItems; }
            set
            {
                _collectionItems = value;
                OnPropertyChanged();
            }
        }

        private void GetTotalPrice()
        {
            decimal total = 0;
            foreach (var item in CollectionItems.Select(x => x.Model))
            {
                var pl = item.PricelistCodeNavigation;
                var num = item.NumPieces;
                if (num > 0)
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

        private void OpenSearchClientDialog()
        {
            var dialog = new SearchClientDialog();
            dialog.WindowState = System.Windows.WindowState.Maximized;
            var result = dialog.ShowDialog();

            if (result != true) return;
            _collectionClient = dialog.SelectedOption;
            ClientFirstNameTextBox = _collectionClient.FirstName;
            ClientLastNameTextBox = _collectionClient.LastName;
            ClientTelephoneTextBox = _collectionClient.Telephone;
            ClientNifTextBox = _collectionClient.Nif;

        }

        private void OpenAddPiece()
        {
            var dialog = new SelectPieceDialog(_collectionType);
            var result = dialog.ShowDialog();

            if (result != true) return;
            var ci = new CollectionItem
            {
                NumPieces = 1,
                PricelistCodeNavigation = dialog.SelectedOption,
                PricelistCode = dialog.SelectedOption.Code,
                CollectionNumber = _collection.Number,
                CollectionNumberNavigation = _collection
            };
            ci.PricelistCodeNavigation.PropertyChanged += PricelistCodeNavigation_PropertyChanged;
            ci.PropertyChanged += PricelistCodeNavigation_PropertyChanged;
            var vm = new CollectionItemViewModel(ci);
            CollectionItems.Add(vm);
        }

        private void OpenPaymentDialog()
        {
            if (!_collection.DueTotal.HasValue) { _collection.DueTotal = TotalPriceTextBox; }
            var dialog = new PaymentDialog(TotalPriceTextBox, _collection.DueTotal.Value);
            var result = dialog.ShowDialog();

            if (result != true) return;
            _collection.DueTotal = dialog.RemainingAmount;
            _collection.PaymentMode = dialog.PaymentMode;
        }

        private void FinishCollection()
        {
            _collection.CollectionItems = CollectionItems.Select(x => x.Model).ToList();
            _collection.Total = TotalPriceTextBox;
            _collection.DueDate = DueDatePicker;
            _collection.CreatedAt = CreatedAtDatePicker;
            _collection.TaxAmount = TaxAmountTextBox;
            _collection.TaxBase = BasePriceTextBox;
            _collection.ClientCodeNavigation = _collectionClient;
            _collection.ClientCode = _collectionClient.Code;
            var result = MessageBox.Show("Vols guardar aquesta recollida?", "Finalitzar", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result != MessageBoxResult.Yes) return;
            if (_collectionRepository.PutCollection(_collection.Number, _collection).Result)
            {
                CloseAction?.Invoke();
            }
            else
            {
                MessageBox.Show("Error guardant la recollida", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void PricelistCodeNavigation_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            GetTotalPrice();
        }

        private void DeleteSelectedPiece()
        {
            if (SelectedCollectionItem != null)
            {
                CollectionItems.Remove(SelectedCollectionItem);
            }
        }

        private void OpenClerkWindow()
        {
            var dialog = new SelectOptionButtonsDialog(new List<string>() { "Joan", "Maria" });
            var result = dialog.ShowDialog();

            if (result == true)
            {
                ClerkTextBox = dialog.SelectedOption;
            }
        }

    }
}
