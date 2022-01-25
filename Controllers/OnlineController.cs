using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using teachBackend.Models;

namespace teachBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OnlineController : ControllerBase
    {
        private readonly OnlineContext _context;

        public OnlineController(OnlineContext context)
        {
            _context = context;
        }

        // GET: api/Online
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Online>>> GetOnlines()
        {
            return await _context.Onlines.ToListAsync();
        }

        // GET: api/Online/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Online>> GetOnline(long id)
        {
            var online = await _context.Onlines.FindAsync(id);

            if (online == null)
            {
                return NotFound();
            }

            return online;
        }

        // PUT: api/Online/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOnline(long id, Online online)
        {
            if (id != online.Id)
            {
                return BadRequest();
            }

            _context.Entry(online).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OnlineExists(id))
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

        // POST: api/Online
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Online>> PostOnline(Online online)
        {
            _context.Onlines.Add(online);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOnline", new { id = online.Id }, online);
        }

        // DELETE: api/Online/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Online>> DeleteOnline(long id)
        {
            var online = await _context.Onlines.FindAsync(id);
            if (online == null)
            {
                return NotFound();
            }

            _context.Onlines.Remove(online);
            await _context.SaveChangesAsync();

            return online;
        }

        private bool OnlineExists(long id)
        {
            return _context.Onlines.Any(e => e.Id == id);
        }
    }
}
