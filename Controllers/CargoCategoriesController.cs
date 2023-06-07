using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CargoManagerSystem.Models;
using Microsoft.AspNetCore.Authorization;

namespace CargoManagerSystem.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CargoCategoriesController : ControllerBase
    {
        private readonly MyCargoDBContext _context;

        public CargoCategoriesController(MyCargoDBContext context)
        {
            _context = context;
        }

        // GET: api/CargoCategories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CargoCategory>>> GetCargoCategories()
        {
          if (_context.CargoCategories == null)
          {
              return NotFound();
          }
            return await _context.CargoCategories.ToListAsync();
        }

        // GET: api/CargoCategories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CargoCategory>> GetCargoCategory(int id)
        {
          if (_context.CargoCategories == null)
          {
              return NotFound();
          }
            var cargoCategory = await _context.CargoCategories.FindAsync(id);

            if (cargoCategory == null)
            {
                return NotFound();
            }

            return cargoCategory;
        }

        // PUT: api/CargoCategories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCargoCategory(int id, CargoCategory cargoCategory)
        {
            if (id != cargoCategory.CargoCategoryId)
            {
                return BadRequest();
            }

            _context.Entry(cargoCategory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CargoCategoryExists(id))
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

        // POST: api/CargoCategories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CargoCategory>> PostCargoCategory(CargoCategory cargoCategory)
        {
          if (_context.CargoCategories == null)
          {
              return Problem("Entity set 'MyCargoDBContext.CargoCategories'  is null.");
          }
            _context.CargoCategories.Add(cargoCategory);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCargoCategory", new { id = cargoCategory.CargoCategoryId }, cargoCategory);
        }

        // DELETE: api/CargoCategories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCargoCategory(int id)
        {
            if (_context.CargoCategories == null)
            {
                return NotFound();
            }
            var cargoCategory = await _context.CargoCategories.FindAsync(id);
            if (cargoCategory == null)
            {
                return NotFound();
            }

            _context.CargoCategories.Remove(cargoCategory);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CargoCategoryExists(int id)
        {
            return (_context.CargoCategories?.Any(e => e.CargoCategoryId == id)).GetValueOrDefault();
        }
    }
}
