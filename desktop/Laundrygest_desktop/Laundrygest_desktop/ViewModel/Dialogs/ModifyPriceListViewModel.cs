using Laundrygest_desktop.Data;
using Laundrygest_desktop.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Laundrygest_desktop.ViewModel
{
    public class ModifyPriceListViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Pricelist> _pricelist;
        public PriceListRepository priceListRepository;
        public int collectionType;
        public ModifyPriceListViewModel(bool isQuilts)
        {
            collectionType = isQuilts ? 1 : 2;
            priceListRepository = new PriceListRepository();
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

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
