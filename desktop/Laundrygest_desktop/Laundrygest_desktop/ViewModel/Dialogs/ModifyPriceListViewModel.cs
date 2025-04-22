using Laundrygest_desktop.Data;
using Laundrygest_desktop.Model;
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
    public class ModifyPriceListViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Pricelist> _pricelist;
        public PriceListRepository priceListRepository;
        private int collectionType;
        private bool? isUpdate;
        private Visibility _visibility;
        private Pricelist _selectedPricelist;
        private string _nameTextBox;
        private string _priceTextBox;
        private string _numPiecesTextBox;
        public Visibility visibility
        {
            get
            {
                return _visibility;
            }
            set
            {
                _visibility = value;
                OnPropertyChanged();
            }
        }
        public ModifyPriceListViewModel(bool isQuilts)
        {
            collectionType = isQuilts ? 2 : 1;
            visibility = Visibility.Hidden;
            isUpdate = null;
            priceListRepository = new PriceListRepository();
            RefreshList();
            AddPieceCommand = new DelegateCommand(ShowAddPiece);
            UpdatePieceCommand = new DelegateCommand(ShowUpdatePiece);
            DeletePieceCommand = new DelegateCommand(DeletePiece);
            ConfirmPieceCommand = new DelegateCommand(ConfirmPiece);
            CancelPieceCommand = new DelegateCommand(CancelPiece);
        }
        public string nameTextBox
        {
            get
            {
                return _nameTextBox;
            }
            set
            {
                _nameTextBox = value;
                OnPropertyChanged();
            }
        }

        public string priceTextBox
        {
            get
            {
                return _priceTextBox;
            }
            set
            {
                _priceTextBox = value;
                OnPropertyChanged();
            }
        }

        public string numPiecesTextBox
        {
            get
            {
                return _numPiecesTextBox;
            }
            set
            {
                _numPiecesTextBox = value;
                OnPropertyChanged();
            }
        }

        public Pricelist selectedPricelist
        {
            get
            {
                return _selectedPricelist;
            }
            set
            {
                _selectedPricelist = value;
                OnPropertyChanged();
            }
        }

        private async void ConfirmPiece()
        {
            if (isUpdate == true && selectedPricelist!=null)
            {                
                Pricelist pl = selectedPricelist;
                decimal.TryParse(priceTextBox, out decimal unitPrice);
                int.TryParse(numPiecesTextBox, out int numPieces);
                pl.Name = nameTextBox;
                pl.UnitPrice = unitPrice;
                pl.NumPieces = numPieces;
                bool result = await priceListRepository.PutPricelist(pl.Code, pl);
                if (result)
                {
                    finishTransaction();
                }
                else
                {
                    MessageBox.Show("No s'ha pogut modificar.");
                }
            }
            else if(isUpdate == false) {            
                Pricelist pl = new Pricelist();
                decimal.TryParse(priceTextBox, out decimal unitPrice);
                int.TryParse(numPiecesTextBox, out int numPieces);
                pl.Name = nameTextBox;
                pl.UnitPrice = unitPrice;
                pl.NumPieces = numPieces;
                pl.CollectionTypeCode = collectionType;
                Pricelist tempPl = await priceListRepository.PostPricelist(pl);
                if (tempPl != null)
                {
                    finishTransaction();
                }
                else
                {
                    MessageBox.Show("No s'ha pogut afegir.");
                }
            }
            RefreshList();
        }

        public void CancelPiece()
        {
            finishTransaction();
        }

        private void finishTransaction()
        {
            visibility = Visibility.Hidden;
            isUpdate = null;
            nameTextBox = string.Empty;
            priceTextBox = string.Empty;
            numPiecesTextBox = string.Empty;
        }

        public ObservableCollection<Pricelist> PriceList
        {
            get
            {
                return _pricelist;
            }
            set
            {
                _pricelist = value;
                OnPropertyChanged();
            }
        }

        public void RefreshList()
        {
            PriceList = priceListRepository.GetPricelists(collectionType).Result;
        }

        public ICommand AddPieceCommand { get; private set; }
        public ICommand UpdatePieceCommand { get; private set; }
        public ICommand DeletePieceCommand { get; private set; }
        public ICommand ConfirmPieceCommand { get; private set; }
        public ICommand CancelPieceCommand { get; private set; }

        public void ShowAddPiece()
        {
            visibility = Visibility.Visible;
            isUpdate = false;
        }
        public void ShowUpdatePiece()
        {
            if (selectedPricelist != null)
            {
                nameTextBox = selectedPricelist.Name;
                priceTextBox = selectedPricelist.UnitPrice.ToString();
                numPiecesTextBox = selectedPricelist.NumPieces.ToString();
                visibility = Visibility.Visible;
                isUpdate = true;
            }
            else
            {
                MessageBox.Show("Selecciona una fila per modificar-la");
            }
        }
        public async void DeletePiece()
        {
            if (selectedPricelist != null)
            {
                bool result = await priceListRepository.DeletePricelist(selectedPricelist.Code);
                if (result)
                {
                    RefreshList();
                }
                else
                {
                    MessageBox.Show("No s'ha pogut eliminar");
                }
            }
            else
            {
                MessageBox.Show("Selecciona una fila per poder eliminar-la");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
