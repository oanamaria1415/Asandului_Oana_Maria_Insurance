using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Asandului_Oana_Maria_Insurance.Data;
using Asandului_Oana_Maria_Insurance.Models;

namespace Asandului_Oana_Maria_Insurance.Controllers
{
    public class ClaimsController : Controller
    {
        private readonly InsuranceContext _context;

        public ClaimsController(InsuranceContext context)
        {
            _context = context;
        }

        // GET: Claims
        public async Task<IActionResult> Index()
        {
            var claims = _context.Claim
                .Include(c => c.Policy);

            return View(await claims.ToListAsync());
        }

        // GET: Claims/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var claim = await _context.Claim
                .Include(c => c.Policy)
                .FirstOrDefaultAsync(m => m.ClaimID == id);

            if (claim == null) return NotFound();

            return View(claim);
        }

        // GET: Claims/Create
        public IActionResult Create()
        {
            ViewData["PolicyID"] = new SelectList(_context.Policy, "PolicyID", "PolicyNumber");
            return View();
        }

        // POST: Claims/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ClaimID,ClaimDate,Description,Amount,PolicyID")] Claim claim)
        {
            if (ModelState.IsValid)
            {
                _context.Add(claim);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["PolicyID"] = new SelectList(_context.Policy, "PolicyID", "PolicyNumber", claim.PolicyID);
            return View(claim);
        }

        // GET: Claims/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var claim = await _context.Claim.FindAsync(id);
            if (claim == null) return NotFound();

            ViewData["PolicyID"] = new SelectList(_context.Policy, "PolicyID", "PolicyNumber", claim.PolicyID);
            return View(claim);
        }

        // POST: Claims/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ClaimID,ClaimDate,Description,Amount,PolicyID")] Claim claim)
        {
            if (id != claim.ClaimID) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(claim);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClaimExists(claim.ClaimID)) return NotFound();
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }

            ViewData["PolicyID"] = new SelectList(_context.Policy, "PolicyID", "PolicyNumber", claim.PolicyID);
            return View(claim);
        }

        // GET: Claims/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var claim = await _context.Claim
                .Include(c => c.Policy)
                .FirstOrDefaultAsync(m => m.ClaimID == id);

            if (claim == null) return NotFound();

            return View(claim);
        }

        // POST: Claims/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var claim = await _context.Claim.FindAsync(id);
            if (claim != null)
            {
                _context.Claim.Remove(claim);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool ClaimExists(int id)
        {
            return _context.Claim.Any(e => e.ClaimID == id);
        }
    }
}
