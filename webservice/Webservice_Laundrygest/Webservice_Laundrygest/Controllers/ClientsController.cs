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
    public class ClientsController : ControllerBase
    {
        private readonly LaundrygestContext _context;

        public ClientsController(LaundrygestContext context)
        {
            _context = context;
        }

        // GET: api/Clients
        [Route("api/clients")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Client>>> GetClients()
        {
            return await _context.Clients.ToListAsync();
        }

        // GET: api/Clients/5
        [Route("api/client/{id}")]
        [HttpGet]
        public async Task<ActionResult<Client>> GetClient(int id)
        {
            var client = await _context.Clients.FindAsync(id);

            if (client == null)
            {
                return NotFound();
            }

            return client;
        }

        // GET: api/Clients/5
        [Route("api/clients/{filter}")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Client>>> GetClientsFilter(string filter)
        {
            var clients = await _context.Clients.Where(x=>x.FirstName.Contains(filter) || x.LastName.Contains(filter) || x.Telephone.ToString().Contains(filter)).ToListAsync();

            if (clients == null)
            {
                return NotFound();
            }

            return clients;
        }

        // PUT: api/Clients/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Route("api/client/{id}")]
        [HttpPut]
        public async Task<IActionResult> PutClient(int id, Client client)
        {
            if (id != client.Code)
            {
                return BadRequest();
            }

            _context.Entry(client).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClientExists(id))
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

        // POST: api/Clients
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Route("api/client")]
        [HttpPost]
        public async Task<ActionResult<Client>> PostClient([FromBody] Client client)
        {
            _context.Clients.Add(client);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetClient", new { id = client.Code }, client);
        }

        // DELETE: api/Clients/5
        [Route("api/client/{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteClient(int id)
        {
            var client = await _context.Clients.FindAsync(id);
            if (client == null)
            {
                return NotFound();
            }

            _context.Clients.Remove(client);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ClientExists(int id)
        {
            return _context.Clients.Any(e => e.Code == id);
        }
    }
}
