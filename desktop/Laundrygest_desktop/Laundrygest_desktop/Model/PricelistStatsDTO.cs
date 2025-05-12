using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laundrygest_desktop.Model
{
    public class PricelistStatsDTO
    {
        public string namePricelist { get; set; }
        public int numPieces { get; set; }
        public decimal totalAmount { get; set; }
    }
}
