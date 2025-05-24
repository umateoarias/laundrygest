using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Webservice_Laundrygest.Models;

namespace Webservice_Laundrygest.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    public class DeliveriesController : ControllerBase
    {
        private readonly LaundrygestContext _context;

        public DeliveriesController(LaundrygestContext context)
        {
            _context = context;
        }

        // GET: api/Deliveries
        [Route("api/deliveries")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Delivery>>> GetDeliveries()
        {
            return await _context.Deliveries.ToListAsync();
        }

        // GET: api/Deliveries/5
        [Route("api/delivery/{id}")]
        [HttpGet]
        public async Task<ActionResult<Delivery>> GetDelivery(int id)
        {
            var delivery = await _context.Deliveries.FindAsync(id);

            if (delivery == null)
            {
                return NotFound();
            }

            return delivery;
        }

        // PUT: api/Deliveries/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Route("api/delivery/{id}")]
        [HttpPut]
        public async Task<IActionResult> PutDelivery(int id, [FromBody] Delivery delivery)
        {
            if (id != delivery.Number)
            {
                return BadRequest();
            }
            _context.Entry(delivery).State = EntityState.Modified;

            foreach (var item in delivery.CollectionItems)
            {
                var existingCollectionItem = await _context.CollectionItems.FirstOrDefaultAsync(c => c.Id==item.Id);
                if (existingCollectionItem != null) existingCollectionItem.DeliveryNumber = delivery.Number;
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DeliveryExists(id))
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

        // POST: api/Deliveries
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Route("api/delivery")]
        [HttpPost]
        public async Task<ActionResult<Delivery>> PostDelivery([FromBody] Delivery delivery)
        {
            _context.Deliveries.Add(delivery);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDelivery", new { id = delivery.Number }, delivery);
        }

        // DELETE: api/Deliveries/5
        [Route("api/delivery/{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteDelivery(int id)
        {
            var delivery = await _context.Deliveries.FindAsync(id);
            if (delivery == null)
            {
                return NotFound();
            }

            _context.Deliveries.Remove(delivery);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DeliveryExists(int id)
        {
            return _context.Deliveries.Any(e => e.Number == id);
        }
    }
}
