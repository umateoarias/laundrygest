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
    public class CollectionsController : ControllerBase
    {
        private readonly LaundrygestContext _context;

        public CollectionsController(LaundrygestContext context)
        {
            _context = context;
        }        

        // GET: api/Collections
        [Route("api/collections")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Collection>>> GetCollections()
        {
            return await _context.Collections.ToListAsync();
        }

        // GET: api/Collections/5
        [Route("api/collection/{number}")]
        [HttpGet]
        public async Task<ActionResult<Collection>> GetCollection(int number)
        {
            var collection = await _context.Collections.FindAsync(number);

            if (collection == null)
            {
                return NotFound();
            }

            return collection;
        }

        [Route("api/collections/{code}")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Collection>>> GetCollectionsClient(int code)
        {
            var collections = await _context.Collections.Where(x=>x.ClientCode==code).Include(x=>x.CollectionItems).ThenInclude(x=>x.PricelistCodeNavigation).ToListAsync();            

            if (collections == null)
            {
                return NotFound();
            }            
            collections = collections.Where(x => x.CollectionItems.Any(y=>y.DeliveryNumber==null)).ToList();

            return collections;
        }

        // PUT: api/Collections/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Route("api/collections/{number}")]
        [HttpPut]
        public async Task<IActionResult> PutCollection(int number, [FromBody] Collection collection)
        {
            if (number != collection.Number)
            {
                return BadRequest();
            }

            _context.Entry(collection).State = EntityState.Modified;

            foreach (var item in collection.CollectionItems)
            {
                if (item.PricelistCodeNavigation != null)
                {
                    var existingPriceList = await _context.Pricelists
                        .FirstOrDefaultAsync(p => p.Code == item.PricelistCodeNavigation.Code);

                    if (existingPriceList != null)
                    {
                        _context.Entry(existingPriceList).State = EntityState.Unchanged;
                        item.PricelistCodeNavigation = existingPriceList;
                    }
                    else
                    {
                        throw new Exception("PriceList no encontrado");
                    }
                }
                _context.CollectionItems.Add(item);
            }
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CollectionExists(number))
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

        // POST: api/Collections
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Route("api/collections")]
        [HttpPost]
        public async Task<ActionResult<Collection>> PostCollection([FromBody] Collection collection)
        {
            _context.Collections.Add(collection);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCollection", new { number = collection.Number }, collection);
        }

        // DELETE: api/Collections/5
        [Route("api/collections/{number}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteCollection(int number)
        {
            var collection = await _context.Collections.FindAsync(number);
            if (collection == null)
            {
                return NotFound();
            }

            _context.Collections.Remove(collection);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CollectionExists(int number)
        {
            return _context.Collections.Any(e => e.Number == number);
        }
    }
}
