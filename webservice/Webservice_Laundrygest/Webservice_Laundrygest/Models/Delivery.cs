using System;
using System.Collections.Generic;

namespace Webservice_Laundrygest.Models;

public partial class Delivery
{
    public int Number { get; set; }

    public DateTime? DeliveryDate { get; set; }

    public string? Clerk { get; set; }

    public virtual ICollection<CollectionItem> CollectionItems { get; set; } = new List<CollectionItem>();
}
