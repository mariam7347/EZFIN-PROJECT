using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EZFIN_PROJECT.Data;
using EZFIN_PROJECT.Model;

namespace EZFIN_PROJECT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SavingPlansController : ControllerBase
    {
        private readonly FinanceContext _context;

        public SavingPlansController(FinanceContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SavingPlan>>> GetSavingPlans()
        {
            return await _context.SavingPlans.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SavingPlan>> GetSavingPlan(int id)
        {
            var savingPlan = await _context.SavingPlans.FindAsync(id);

            if (savingPlan == null)
                return NotFound();

            return savingPlan;
        }

        [HttpPost]
        public async Task<ActionResult<SavingPlan>> CreateSavingPlan(SavingPlan savingPlan)
        {
            _context.SavingPlans.Add(savingPlan);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSavingPlan), new { id = savingPlan.SavingPlanID }, savingPlan);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSavingPlan(int id, SavingPlan savingPlan)
        {
            if (id != savingPlan.SavingPlanID)
                return BadRequest();

            _context.Entry(savingPlan).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSavingPlan(int id)
        {
            var savingPlan = await _context.SavingPlans.FindAsync(id);
            if (savingPlan == null)
                return NotFound();

            _context.SavingPlans.Remove(savingPlan);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
