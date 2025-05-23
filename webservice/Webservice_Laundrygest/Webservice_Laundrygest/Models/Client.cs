﻿using System;
using System.Collections.Generic;

namespace Webservice_Laundrygest.Models;

public partial class Client
{
    public int Code { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? Email { get; set; }

    public string Telephone { get; set; } = null!;

    public string? Nif { get; set; }

    public string? UsernameApp { get; set; }

    public string? PasswordApp { get; set; }

    public string? Address { get; set; }

    public string? PostalCode { get; set; }

    public string? Locality { get; set; }
    public DateTime? LastLoginMobile { get; set; }

    public virtual ICollection<Collection> Collections { get; set; } = new List<Collection>();

    public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();
}
