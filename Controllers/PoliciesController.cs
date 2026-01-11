using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Asandului_Oana_Maria_Insurance.Data;
using Asandului_Oana_Maria_Insurance.Models;

namespace Asandului_Oana_Maria_Insurance.Controllers
{
    public class PoliciesController : Controller
    {
        private readonly InsuranceContext _context;

        public PoliciesController(InsuranceContext context)
        {
            _context = context;
        }

        // GET: Policies
        public async Task<IActionResult> Index(
    string searchCustomer,
    int? providerId,
    string sortOrder)
        {
            // sort keys
            ViewData["CurrentSort"] = sortOrder;
            ViewData["PremiumSort"] = string.IsNullOrEmpty(sortOrder) ? "premium_desc" : "";
            ViewData["StartSort"] = sortOrder == "start_asc" ? "start_desc" : "start_asc";

            // keep filters
            ViewData["SearchCustomer"] = searchCustomer;
            ViewData["ProviderId"] = providerId;

            // 🔴 AICI este FIXUL IMPORTANT
            ViewData["Providers"] = new SelectList(
                _context.Provider,
                "ProviderID",
                "Name",
                providerId   // <-- ASTA păstrează providerul selectat
            );

            var query = _context.Policy
                .Include(p => p.Customer)
                .Include(p => p.Provider)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchCustomer))
            {
                query = query.Where(p =>
                    p.Customer != null &&
                    p.Customer.Name.Contains(searchCustomer));
            }

            if (providerId.HasValue)
            {
                query = query.Where(p => p.ProviderID == providerId.Value);
            }

            query = sortOrder switch
            {
                "premium_desc" => query.OrderByDescending(p => p.Premium),
                "start_asc" => query.OrderBy(p => p.StartDate),
                "start_desc" => query.OrderByDescending(p => p.StartDate),
                _ => query.OrderBy(p => p.Premium)
            };

            return View(await query.ToListAsync());
        }


        // GET: Policies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var policy = await _context.Policy
    .Include(p => p.Customer)
    .Include(p => p.Provider)
    .Include(p => p.Claims)
    .Include(p => p.Payments)
    .FirstOrDefaultAsync(m => m.PolicyID == id);

            if (policy == null)
                return NotFound();

            return View(policy);
        }

        // GET: Policies/Create
        public IActionResult Create()
        {
            // show Names in dropdowns (not IDs)
            ViewData["CustomerID"] = new SelectList(_context.Customer, "CustomerID", "Name");
            ViewData["ProviderID"] = new SelectList(_context.Provider, "ProviderID", "Name");
            return View();
        }

        // POST: Policies/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PolicyID,PolicyNumber,StartDate,EndDate,Premium,CustomerID,ProviderID")] Policy policy)
        {
            if (ModelState.IsValid)
            {
                _context.Add(policy);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // repopulate dropdowns if validation fails
            ViewData["CustomerID"] = new SelectList(_context.Customer, "CustomerID", "Name", policy.CustomerID);
            ViewData["ProviderID"] = new SelectList(_context.Provider, "ProviderID", "Name", policy.ProviderID);
            return View(policy);
        }

        // GET: Policies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var policy = await _context.Policy.FindAsync(id);
            if (policy == null)
                return NotFound();

            ViewData["CustomerID"] = new SelectList(_context.Customer, "CustomerID", "Name", policy.CustomerID);
            ViewData["ProviderID"] = new SelectList(_context.Provider, "ProviderID", "Name", policy.ProviderID);
            return View(policy);
        }

        // POST: Policies/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PolicyID,PolicyNumber,StartDate,EndDate,Premium,CustomerID,ProviderID")] Policy policy)
        {
            if (id != policy.PolicyID)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(policy);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PolicyExists(policy.PolicyID))
                        return NotFound();
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }

            ViewData["CustomerID"] = new SelectList(_context.Customer, "CustomerID", "Name", policy.CustomerID);
            ViewData["ProviderID"] = new SelectList(_context.Provider, "ProviderID", "Name", policy.ProviderID);
            return View(policy);
        }

        // GET: Policies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var policy = await _context.Policy
                .Include(p => p.Customer)
                .Include(p => p.Provider)
                .FirstOrDefaultAsync(m => m.PolicyID == id);

            if (policy == null)
                return NotFound();

            return View(policy);
        }

        // POST: Policies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var policy = await _context.Policy.FindAsync(id);
            if (policy != null)
            {
                _context.Policy.Remove(policy);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool PolicyExists(int id)
        {
            return _context.Policy.Any(e => e.PolicyID == id);
        }
    }
}
