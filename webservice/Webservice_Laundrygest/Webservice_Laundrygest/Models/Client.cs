using System;
using System.Collections.Generic;

namespace Webservice_Laundrygest.Models;

public partial class Client
{
    public int Code { get; set; }

    public string FirstName { get; set; } = null!;

    public string? LastName { get; set; }

    public string? Email { get; set; }

    public int Telephone { get; set; }

    public string? Nif { get; set; }

    public string? UsernameApp { get; set; }

    public string? PasswordApp { get; set; }

    public virtual ICollection<Collection> Collections { get; set; } = new List<Collection>();

    public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();
}
