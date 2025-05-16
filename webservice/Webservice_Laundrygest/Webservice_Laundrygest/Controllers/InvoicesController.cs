using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Webservice_Laundrygest.Models;

namespace Webservice_Laundrygest.Controllers
{

    public class InvoicesController : ControllerBase
    {
        private readonly LaundrygestContext _context;

        public InvoicesController(LaundrygestContext context)
        {
            _context = context;
        }

        // GET: api/Invoices
        [Route("api/invoices")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Invoice>>> GetInvoices()
        {
            return await _context.Invoices.ToListAsync();
        }

        // GET: api/Invoices/5
        [Route("api/invoice/{id}")]
        [HttpGet]
        public async Task<ActionResult<Invoice>> GetInvoice(int id)
        {
            var invoice = await _context.Invoices.Where(x=>x.Id==id).Include(x=>x.ClientCodeNavigation).Include(x=>x.Collections).ThenInclude(z=>z.CollectionItems).ThenInclude(y=>y.PricelistCodeNavigation).FirstOrDefaultAsync();

            if (invoice == null)
            {
                return NotFound();
            }

            return invoice;
        }

        // PUT: api/Invoices/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Route("api/invoice/{id}")]
        [HttpPut]
        public async Task<IActionResult> PutInvoice(int id,[FromBody] Invoice invoice)
        {
            if (id != invoice.Id)
            {
                return BadRequest();
            }

            _context.Entry(invoice).State = EntityState.Modified;

            try
            {
                GetNextInvoiceNumber(invoice);
                foreach (var c in invoice.Collections)
                {
                    var collection = await _context.Collections.Where(x => x.Number == c.Number).FirstOrDefaultAsync();
                    collection.InvoiceId = invoice.Id;
                    collection.Invoice = invoice;
                }
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InvoiceExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Invoices
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Route("api/invoice")]
        [HttpPost]
        public async Task<ActionResult<Invoice>> PostInvoice([FromBody]Invoice invoice)
        {
            _context.Invoices.Add(invoice);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetInvoice", new { id = invoice.Id }, invoice);
        }

        // DELETE: api/Invoices/5
        [Route("api/invoice/{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteInvoice(int id)
        {
            var invoice = await _context.Invoices.FindAsync(id);
            if (invoice == null)
            {
                return NotFound();
            }

            _context.Invoices.Remove(invoice);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool InvoiceExists(int id)
        {
            return _context.Invoices.Any(e => e.Id == id);
        }

        private void GetNextInvoiceNumber(Invoice invoice)
        {
            var next = (!_context.Invoices.Any() ? 1 : _context.Invoices.Max(i => i.Number) + 1) ?? 1;
            invoice.Number = next;
        }
    }
}
