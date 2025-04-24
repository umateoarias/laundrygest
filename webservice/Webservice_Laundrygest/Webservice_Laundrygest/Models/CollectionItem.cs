using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Webservice_Laundrygest.Models;

public partial class CollectionItem
{
    public int Id { get; set; }

    public DateTime? CollectedAt { get; set; }

    public int NumPieces { get; set; }

    public string? Observation { get; set; }

    public string? StoreLocation { get; set; }
    
    public int CollectionNumber { get; set; }

    public int PricelistCode { get; set; }

    public int? DeliveryNumber { get; set; }

    [JsonIgnore]
    public virtual Collection CollectionNumberNavigation { get; set; } = null!;

    public virtual Delivery? DeliveryNumberNavigation { get; set; }
    public virtual Pricelist PricelistCodeNavigation { get; set; } = null!;
}
