using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laundrygest_desktop.Model
{
    public partial class Delivery
    {
        public int Number { get; set; }

        public DateTime? DeliveryDate { get; set; }
        public string? Clerk { get; set; }

        public virtual ICollection<CollectionItem> CollectionItems { get; set; } = new List<CollectionItem>();
    }
}
