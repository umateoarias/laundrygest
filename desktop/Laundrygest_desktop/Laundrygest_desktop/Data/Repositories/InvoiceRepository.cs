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
                invoice = await MakeRequest<Invoice>("invoice", "POST", i);
            }
            catch { }
            return invoice;
        }

        public async Task<bool> PutClient(int invoice_id, Invoice invoice)
        {
            try
            {
                await MakeRequest<Invoice>("invoice/" + invoice_id, "PUT", invoice);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<Invoice> GetInvoice(int id)
        {
            Invoice invoice = null;
            try
            {
                invoice = await MakeRequest<Invoice>("invoice/" + id, "GET", null);
            }
            catch { }
            if (invoice == null)
            {
                invoice = new Invoice();
            }
            return invoice;
        }
    }
}
