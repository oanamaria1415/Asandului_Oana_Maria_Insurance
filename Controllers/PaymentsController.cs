using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Asandului_Oana_Maria_Insurance.Data;
using Asandului_Oana_Maria_Insurance.Models;

namespace Asandului_Oana_Maria_Insurance.Controllers
{
    public class PaymentsController : Controller
    {
        private readonly InsuranceContext _context;

        public PaymentsController(InsuranceContext context)
        {
            _context = context;
        }

        // GET: Payments
        public async Task<IActionResult> Index()
        {
            var payments = _context.Payment
                .Include(p => p.Policy);

            return View(await payments.ToListAsync());
        }

        // GET: Payments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var payment = await _context.Payment
                .Include(p => p.Policy)
                .FirstOrDefaultAsync(m => m.PaymentID == id);

            if (payment == null) return NotFound();

            return View(payment);
        }

        // GET: Payments/Create
        public IActionResult Create()
        {
            ViewData["PolicyID"] = new SelectList(_context.Policy, "PolicyID", "PolicyNumber");
            return View();
        }

        // POST: Payments/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PaymentID,PaymentDate,Amount,Method,PolicyID")] Payment payment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(payment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["PolicyID"] = new SelectList(_context.Policy, "PolicyID", "PolicyNumber", payment.PolicyID);
            return View(payment);
        }

        // GET: Payments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var payment = await _context.Payment.FindAsync(id);
            if (payment == null) return NotFound();

            ViewData["PolicyID"] = new SelectList(_context.Policy, "PolicyID", "PolicyNumber", payment.PolicyID);
            return View(payment);
        }

        // POST: Payments/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PaymentID,PaymentDate,Amount,Method,PolicyID")] Payment payment)
        {
            if (id != payment.PaymentID) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(payment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PaymentExists(payment.PaymentID)) return NotFound();
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }

            ViewData["PolicyID"] = new SelectList(_context.Policy, "PolicyID", "PolicyNumber", payment.PolicyID);
            return View(payment);
        }

        // GET: Payments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var payment = await _context.Payment
                .Include(p => p.Policy)
                .FirstOrDefaultAsync(m => m.PaymentID == id);

            if (payment == null) return NotFound();

            return View(payment);
        }

        // POST: Payments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var payment = await _context.Payment.FindAsync(id);
            if (payment != null)
            {
                _context.Payment.Remove(payment);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool PaymentExists(int id)
        {
            return _context.Payment.Any(e => e.PaymentID == id);
        }
    }
}
