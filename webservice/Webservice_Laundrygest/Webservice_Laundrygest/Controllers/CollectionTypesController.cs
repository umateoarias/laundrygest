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
    public class CollectionTypesController : ControllerBase
    {
        private readonly LaundrygestContext _context;

        public CollectionTypesController(LaundrygestContext context)
        {
            _context = context;
        }

        // GET: api/CollectionTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CollectionType>>> GetCollectionTypes()
        {
            return await _context.CollectionTypes.ToListAsync();
        }

        // GET: api/CollectionTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CollectionType>> GetCollectionType(int id)
        {
            var collectionType = await _context.CollectionTypes.FindAsync(id);

            if (collectionType == null)
            {
                return NotFound();
            }

            return collectionType;
        }

        // PUT: api/CollectionTypes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCollectionType(int id, CollectionType collectionType)
        {
            if (id != collectionType.Code)
            {
                return BadRequest();
            }

            _context.Entry(collectionType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CollectionTypeExists(id))
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

        // POST: api/CollectionTypes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CollectionType>> PostCollectionType(CollectionType collectionType)
        {
            _context.CollectionTypes.Add(collectionType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCollectionType", new { id = collectionType.Code }, collectionType);
        }

        // DELETE: api/CollectionTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCollectionType(int id)
        {
            var collectionType = await _context.CollectionTypes.FindAsync(id);
            if (collectionType == null)
            {
                return NotFound();
            }

            _context.CollectionTypes.Remove(collectionType);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CollectionTypeExists(int id)
        {
            return _context.CollectionTypes.Any(e => e.Code == id);
        }
    }
}
