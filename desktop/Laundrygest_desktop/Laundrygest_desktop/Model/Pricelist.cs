using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Laundrygest_desktop.Model
{

    public partial class Pricelist
    {
        public int Code { get; set; }

        public string Name { get; set; } = null!;

        private decimal _unitPrice;
        public decimal UnitPrice { get { return _unitPrice; } set { _unitPrice = value;OnPropertyChanged(); } }

        public int CollectionTypeCode { get; set; }

        public int NumPieces { get; set; }

        public virtual ICollection<CollectionItem> CollectionItems { get; set; } = new List<CollectionItem>();

        public virtual CollectionType CollectionTypeCodeNavigation { get; set; } = null!;

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
