using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NorthwindAPI.Models;

namespace NorthwindAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerDemographicsController : ControllerBase
    {
        private readonly NorthwindContext _context;

        public CustomerDemographicsController(NorthwindContext context)
        {
            _context = context;
        }

        // GET: api/CustomerDemographics
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerDemographic>>> GetCustomerDemographics()
        {
            return await _context.CustomerDemographics.ToListAsync();
        }

        // GET: api/CustomerDemographics/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerDemographic>> GetCustomerDemographic(string id)
        {
            var customerDemographic = await _context.CustomerDemographics.FindAsync(id);

            if (customerDemographic == null)
            {
                return NotFound();
            }

            return customerDemographic;
        }

        // PUT: api/CustomerDemographics/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomerDemographic(string id, CustomerDemographic customerDemographic)
        {
            if (id != customerDemographic.CustomerTypeId)
            {
                return BadRequest();
            }

            _context.Entry(customerDemographic).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerDemographicExists(id))
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

        // POST: api/CustomerDemographics
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CustomerDemographic>> PostCustomerDemographic(CustomerDemographic customerDemographic)
        {
            _context.CustomerDemographics.Add(customerDemographic);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (CustomerDemographicExists(customerDemographic.CustomerTypeId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetCustomerDemographic", new { id = customerDemographic.CustomerTypeId }, customerDemographic);
        }

        // DELETE: api/CustomerDemographics/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomerDemographic(string id)
        {
            var customerDemographic = await _context.CustomerDemographics.FindAsync(id);
            if (customerDemographic == null)
            {
                return NotFound();
            }

            _context.CustomerDemographics.Remove(customerDemographic);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CustomerDemographicExists(string id)
        {
            return _context.CustomerDemographics.Any(e => e.CustomerTypeId == id);
        }
    }
}
