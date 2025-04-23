using System;
using System.Collections.Generic;

namespace Webservice_Laundrygest.Models;

public partial class Pricelist
{
    public int Code { get; set; }

    public string Name { get; set; } = null!;

    public decimal UnitPrice { get; set; }

    public int CollectionTypeCode { get; set; }

    public int? NumPieces { get; set; }

    public virtual ICollection<CollectionItem> CollectionItems { get; set; } = new List<CollectionItem>();

    public virtual CollectionType CollectionTypeCodeNavigation { get; set; } = null!;
}
