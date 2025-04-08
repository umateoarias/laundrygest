using System;
using System.Collections.Generic;

namespace Laundrygest_desktop.Model
{

    public partial class Pricelist
    {
        public int Code { get; set; }

        public string Name { get; set; } = null!;

        public decimal UnitPrice { get; set; }

        public int CollectionTypeCode { get; set; }

        public virtual ICollection<CollectionItem> CollectionItems { get; set; } = new List<CollectionItem>();

        public virtual CollectionType CollectionTypeCodeNavigation { get; set; } = null!;
    }
}
