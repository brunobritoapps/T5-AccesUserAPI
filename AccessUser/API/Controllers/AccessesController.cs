using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Data;
using API.Models;
using Microsoft.AspNetCore.Cors;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AccessPolicy")]
    //#Add ref cors
    public class AccessesController : ControllerBase
    {
        private readonly AccessContext _context;

        public AccessesController(AccessContext context)
        {
            _context = context;
        }

        // GET: api/Accesses
        [HttpGet]
        public IEnumerable<Access> GetAccess()
        {
            return _context.Access;
        }

        // GET: api/Accesses/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAccess([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var access = await _context.Access.FindAsync(id);

            if (access == null)
            {
                return NotFound();
            }

            return Ok(access);
        }

        // PUT: api/Accesses/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAccess([FromRoute] int id, [FromBody] Access access)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != access.AccessId)
            {
                return BadRequest();
            }

            _context.Entry(access).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AccessExists(id))
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

        // POST: api/Accesses
        [HttpPost]
        public async Task<IActionResult> PostAccess([FromBody] Access access)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Access.Add(access);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAccess", new { id = access.AccessId }, access);
        }

        // DELETE: api/Accesses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccess([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var access = await _context.Access.FindAsync(id);
            if (access == null)
            {
                return NotFound();
            }

            _context.Access.Remove(access);
            await _context.SaveChangesAsync();

            return Ok(access);
        }

        private bool AccessExists(int id)
        {
            return _context.Access.Any(e => e.AccessId == id);
        }
    }
}