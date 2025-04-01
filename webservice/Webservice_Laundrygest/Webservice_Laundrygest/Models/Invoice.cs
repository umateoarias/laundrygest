using System;
using System.Collections.Generic;

namespace Webservice_Laundrygest.Models;

public partial class Invoice
{
    public int Id { get; set; }

    public string Number { get; set; } = null!;

    public decimal TotalBase { get; set; }

    public decimal TaxAmount { get; set; }

    public decimal TaxBase { get; set; }

    public int ClientCode { get; set; }

    public virtual Client ClientCodeNavigation { get; set; } = null!;

    public virtual ICollection<Collection> Collections { get; set; } = new List<Collection>();
}
