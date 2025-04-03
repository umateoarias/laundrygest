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
    [Route("api/[controller]")]
    [ApiController]
    public class CollectionItemsController : ControllerBase
    {
        private readonly LaundrygestContext _context;

        public CollectionItemsController(LaundrygestContext context)
        {
            _context = context;
        }

        // GET: api/CollectionItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CollectionItem>>> GetCollectionItems()
        {
            return await _context.CollectionItems.ToListAsync();
        }

        // GET: api/CollectionItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CollectionItem>> GetCollectionItem(int id)
        {
            var collectionItem = await _context.CollectionItems.FindAsync(id);

            if (collectionItem == null)
            {
                return NotFound();
            }

            return collectionItem;
        }

        // PUT: api/CollectionItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCollectionItem(int id, CollectionItem collectionItem)
        {
            if (id != collectionItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(collectionItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CollectionItemExists(id))
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

        // POST: api/CollectionItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CollectionItem>> PostCollectionItem(CollectionItem collectionItem)
        {
            _context.CollectionItems.Add(collectionItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCollectionItem", new { id = collectionItem.Id }, collectionItem);
        }

        // DELETE: api/CollectionItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCollectionItem(int id)
        {
            var collectionItem = await _context.CollectionItems.FindAsync(id);
            if (collectionItem == null)
            {
                return NotFound();
            }

            _context.CollectionItems.Remove(collectionItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CollectionItemExists(int id)
        {
            return _context.CollectionItems.Any(e => e.Id == id);
        }
    }
}
