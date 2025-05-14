using Laundrygest_desktop.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laundrygest_desktop.Data.Repositories
{
    public class InvoiceRepository : BaseRepository
    {
        public async Task<Invoice?> PostInvoice(Invoice i)
        {
            Invoice? invoice = null;
            try
            {
                invoice = await MakeRequest<Invoice>("invoice/", "POST", i);
            }
            catch { }
            return invoice;
        }
    }
}
