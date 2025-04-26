using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EZFIN_PROJECT.Data;
using EZFIN_PROJECT.Model;

namespace EZFIN_PROJECT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RevenuesController : ControllerBase
    {
        private readonly FinanceContext _context;

        public RevenuesController(FinanceContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Revenue>>> GetRevenues()
        {
            return await _context.Revenues.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Revenue>> GetRevenue(int id)
        {
            var revenue = await _context.Revenues.FindAsync(id);

            if (revenue == null)
                return NotFound();

            return revenue;
        }

        [HttpPost]
        public async Task<ActionResult<Revenue>> CreateRevenue(Revenue revenue)
        {
            _context.Revenues.Add(revenue);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetRevenue), new { id = revenue.RevenueID }, revenue);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRevenue(int id, Revenue revenue)
        {
            if (id != revenue.RevenueID)
                return BadRequest();

            _context.Entry(revenue).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRevenue(int id)
        {
            var revenue = await _context.Revenues.FindAsync(id);
            if (revenue == null)
                return NotFound();

            _context.Revenues.Remove(revenue);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
