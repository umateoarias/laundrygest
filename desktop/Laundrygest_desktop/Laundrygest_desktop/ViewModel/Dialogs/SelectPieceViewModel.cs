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
using System.Windows.Input;

namespace Laundrygest_desktop.ViewModel.Dialogs
{
    public class SelectPieceViewModel : INotifyPropertyChanged
    {
        private readonly int collectionType;
        private readonly PriceListRepository priceListRepository;
        private ObservableCollection<Pricelist> _pricelists;
        private Pricelist _selectedPricelist;
        public Action<Pricelist> OnOptionSelected { get; set; }
        public SelectPieceViewModel(int collectionType)
        {
            this.collectionType = collectionType;
            priceListRepository = new PriceListRepository();
            pricelists = priceListRepository.GetPricelists(collectionType,"").Result;
            SelectPieceCommand = new DelegateCommand(SelectPiece);
        }
        public ObservableCollection<Pricelist> pricelists
        {
            get { return _pricelists; }
            set
            {
                _pricelists = value;
                OnPropertyChanged();
            }
        }

        public Pricelist SelectedPricelist
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

        public ICommand SelectPieceCommand { get; }

        public void SelectPiece()
        {
            if (_selectedPricelist != null)
            {
                OnOptionSelected?.Invoke(SelectedPricelist);
            }        

        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
