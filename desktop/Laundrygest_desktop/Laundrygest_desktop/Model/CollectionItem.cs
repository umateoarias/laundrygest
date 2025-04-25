using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
#nullable enable
namespace Laundrygest_desktop.Model
{

    public partial class CollectionItem
    {
        public int Id { get; set; }

        public DateTime? CollectedAt { get; set; }
        private int _numPieces;

        public int NumPieces { get { return _numPieces; } set { _numPieces = value;OnPropertyChanged(); } }

        public string? Observation { get; set; }

        public string? StoreLocation { get; set; }

        public int CollectionNumber { get; set; }

        public int PricelistCode { get; set; }
        public int? DeliveryNumber { get; set; }


        [JsonIgnore]
        public virtual Collection CollectionNumberNavigation { get; set; } = null!;

        public virtual Pricelist PricelistCodeNavigation { get; set; } = null!;
        public virtual Delivery? DeliveryNumberNavigation { get; set; }


        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
