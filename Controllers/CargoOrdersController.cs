using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CargoManagerSystem.Models;

namespace CargoManagerSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CargoOrdersController : ControllerBase
    {
        private readonly MyCargoDBContext _context;

        public CargoOrdersController(MyCargoDBContext context)
        {
            _context = context;
        }

        // GET: api/CargoOrders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CargoOrder>>> GetCargoOrders()
        {
          if (_context.CargoOrders == null)
          {
              return NotFound();
          }
            return await _context.CargoOrders.ToListAsync();
        }

        // GET: api/CargoOrders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CargoOrder>> GetCargoOrder(int id)
        {
          if (_context.CargoOrders == null)
          {
              return NotFound();
          }
            var cargoOrder = await _context.CargoOrders.FindAsync(id);

            if (cargoOrder == null)
            {
                return NotFound();
            }

            return cargoOrder;
        }

        // PUT: api/CargoOrders/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCargoOrder(int id, CargoOrder cargoOrder)
        {
            if (id != cargoOrder.CargoorderId)
            {
                return BadRequest();
            }

            _context.Entry(cargoOrder).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CargoOrderExists(id))
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

        // POST: api/CargoOrders
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CargoOrder>> PostCargoOrder(CargoOrder cargoOrder)
        {
          if (_context.CargoOrders == null)
          {
              return Problem("Entity set 'MyCargoDBContext.CargoOrders'  is null.");
          }
            _context.CargoOrders.Add(cargoOrder);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCargoOrder", new { id = cargoOrder.CargoorderId }, cargoOrder);
        }

        // DELETE: api/CargoOrders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCargoOrder(int id)
        {
            if (_context.CargoOrders == null)
            {
                return NotFound();
            }
            var cargoOrder = await _context.CargoOrders.FindAsync(id);
            if (cargoOrder == null)
            {
                return NotFound();
            }

            _context.CargoOrders.Remove(cargoOrder);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CargoOrderExists(int id)
        {
            return (_context.CargoOrders?.Any(e => e.CargoorderId == id)).GetValueOrDefault();
        }
    }
}
