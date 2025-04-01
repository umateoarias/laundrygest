﻿using System;
using System.Collections.Generic;

namespace Webservice_Laundrygest.Models;

public partial class Collection
{
    public int Number { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime DueDate { get; set; }

    public decimal TaxBase { get; set; }

    public decimal TaxAmount { get; set; }

    public decimal Total { get; set; }

    public int ClientCode { get; set; }

    public int CollectionTypeCode { get; set; }

    public int InvoiceId { get; set; }

    public virtual Client ClientCodeNavigation { get; set; } = null!;

    public virtual ICollection<CollectionItem> CollectionItems { get; set; } = new List<CollectionItem>();

    public virtual CollectionType CollectionTypeCodeNavigation { get; set; } = null!;

    public virtual Invoice Invoice { get; set; } = null!;
}
