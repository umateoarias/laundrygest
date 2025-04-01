using System;
using System.Collections.Generic;

namespace Webservice_Laundrygest.Models;

public partial class CollectionType
{
    public int Code { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Collection> Collections { get; set; } = new List<Collection>();

    public virtual ICollection<Pricelist> Pricelists { get; set; } = new List<Pricelist>();
}
