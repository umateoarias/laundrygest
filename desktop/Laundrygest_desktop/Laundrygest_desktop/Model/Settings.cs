using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laundrygest_desktop.Model
{
    public class Settings
    {
        public string ApiUrl { get; set; }
        public int DaysDelay { get; set; }
        public Company Company { get; set; }
        public List<string> Clerks { get; set; }
    }

    public class Company
    {
        public string CompanyName { get; set; }
        public string OwnerName { get; set; }
        public string OwnerLastName { get; set; }
        public string Telephone { get; set; }
        public string Nif { get; set; }
        public string Address { get; set; }
        public string PostalCode { get; set; }
    }
}
