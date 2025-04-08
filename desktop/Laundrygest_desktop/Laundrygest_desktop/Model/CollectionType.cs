using System;
using System.Collections.Generic;

namespace Laundrygest_desktop.Model
{

    public partial class CollectionType
    {
        public int Code { get; set; }

        public string Name { get; set; } = null!;

        public virtual ICollection<Collection> Collections { get; set; } = new List<Collection>();

        public virtual ICollection<Pricelist> Pricelists { get; set; } = new List<Pricelist>();
    }
}