using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laundrygest_desktop.Model
{
    public class MonthlyStatsDTO
    {
        public string dateName { get; set; }
        public decimal totalAmount { get; set; }
        public decimal taxAmount { get; set; }
        public decimal baseAmount { get; set; }
    }
}
