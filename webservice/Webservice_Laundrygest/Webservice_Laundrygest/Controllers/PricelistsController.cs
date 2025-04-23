using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Webservice_Laundrygest.Models;

namespace Webservice_Laundrygest.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    public class PricelistsController : ControllerBase
    {
        private readonly LaundrygestContext _context;

        public PricelistsController(LaundrygestContext context)
        {
            _context = context;
        }

        // GET: api/Pricelists
        [Route("api/pricelists/all")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pricelist>>> GetPricelists()
        {
            return await _context.Pricelists.ToListAsync();
        }

        // GET: api/Pricelists/5
        [Route("api/pricelists")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pricelist>>> GetPricelist([FromQuery] int collectionType, [FromQuery] string filter)
        {
            var pricelist = await _context.Pricelists.Where(x => x.CollectionTypeCode == collectionType).ToListAsync();

            if (!string.IsNullOrEmpty(filter))
            {
                pricelist = pricelist.Where(x => x.Name.ToLower().Contains(filter.ToLower())).ToList();
            }

            if (pricelist == null)
            {
                return NotFound();
            }

            return pricelist;
        }

        // PUT: api/Pricelists/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Route("api/pricelists/{code}")]
        [HttpPut]
        public async Task<IActionResult> PutPricelist(int code, [FromBody] Pricelist pricelist)
        {
            if (code != pricelist.Code)
            {
                return BadRequest();
            }

            _context.Entry(pricelist).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PricelistExists(code))
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

        // POST: api/Pricelists
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Route("api/pricelists")]
        [HttpPost]
        public async Task<ActionResult<Pricelist>> PostPricelist([FromBody] Pricelist pricelist)
        {
            _context.Pricelists.Add(pricelist);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPricelists", new { id = pricelist.Code }, pricelist);
        }
        // DELETE: api/Pricelists/5
        [Route("api/pricelists/{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeletePricelist(int id)
        {
            var pricelist = await _context.Pricelists.FindAsync(id);
            if (pricelist == null)
            {
                return NotFound();
            }

            _context.Pricelists.Remove(pricelist);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PricelistExists(int id)
        {
            return _context.Pricelists.Any(e => e.Code == id);
        }
    }
}
