using Laundrygest_desktop.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Laundrygest_desktop.ViewModel.Dialogs
{
    public class CollectionItemViewModel : INotifyPropertyChanged
    {
        public CollectionItem Model { get; }

        public CollectionItemViewModel(CollectionItem model)
        {
            Model = model;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string prop = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));

        public string PricelistName => Model.PricelistCodeNavigation?.Name;
        public int NumPieces
        {
            get => Model.NumPieces;
            set
            {
                if (Model.NumPieces != value)
                {
                    Model.NumPieces = value;
                    OnPropertyChanged();
                }
            }
        }
        public decimal? Price => Model.PricelistCodeNavigation?.UnitPrice;

        private bool _isMarked;
        public bool IsMarked
        {
            get => _isMarked;
            set
            {
                if (_isMarked != value)
                {
                    _isMarked = value;
                    OnPropertyChanged();
                }
            }
        }
        public bool HasDeliveryNumber => Model.DeliveryNumber != null;
    }

}
