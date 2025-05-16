using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Laundrygest_desktop.Model
{

    public partial class Invoice
    {
        public int Id { get; set; }

        public int? Number { get; set; }

        public decimal? TotalBase { get; set; }

        public decimal? TaxAmount { get; set; }

        public decimal? TaxBase { get; set; }

        public int? ClientCode { get; set; }
        public DateTime InvoiceDate { get; set; }
        [JsonIgnore]
        public virtual Client? ClientCodeNavigation { get; set; } = null!;

        public virtual ICollection<Collection> Collections { get; set; } = new List<Collection>();
    }
}
