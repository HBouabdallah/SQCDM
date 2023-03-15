using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IndustryIncident.Models;
using Microsoft.AspNetCore.Authorization;

namespace IndustryIncident.Controllers
{
    [Authorize]

    public class IndicatorsController : Controller
    {
        private readonly IndustryIncidentContext _context;

        public IndicatorsController(IndustryIncidentContext context)
        {
            _context = context;
        }

        // GET: Indicators
        public async Task<IActionResult> Index()
        {
            if (!this.User.IsInRole("Admin"))
            {
                return RedirectToAction("index", "notfound");

            }
            return View(await _context.Indicators.ToListAsync());
        }

        // GET: Indicators/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (!this.User.IsInRole("Admin"))
            {
                return RedirectToAction("index", "notfound");

            }
            if (id == null || _context.Indicators == null)
            {
                return NotFound();
            }

            var indicator = await _context.Indicators
                .FirstOrDefaultAsync(m => m.Id == id);
            if (indicator == null)
            {
                return NotFound();
            }

            return View(indicator);
        }

        // GET: Indicators/Create
        public IActionResult Create()
        {
            if (!this.User.IsInRole("Admin"))
            {
                return RedirectToAction("index", "notfound");

            }
            return View();
        }

        // POST: Indicators/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Indicator indicator)
        {
            if (ModelState.IsValid)
            {
                _context.Add(indicator);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(indicator);
        }

        // GET: Indicators/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (!this.User.IsInRole("Admin"))
            {
                return RedirectToAction("index", "notfound");

            }
            if (id == null || _context.Indicators == null)
            {
                return NotFound();
            }

            var indicator = await _context.Indicators.FindAsync(id);
            if (indicator == null)
            {
                return NotFound();
            }
            return View(indicator);
        }

        // POST: Indicators/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Indicator indicator)
        {
            if (id != indicator.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(indicator);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IndicatorExists(indicator.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(indicator);
        }

        // GET: Indicators/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (_context.Indicators == null)
            {
                return Problem("Entity set 'IndustryIncidentContext.Indicators'  is null.");
            }
            var indicator = await _context.Indicators.FindAsync(id);
            if (indicator != null)
            {
                _context.Indicators.Remove(indicator);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // POST: Indicators/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Indicators == null)
            {
                return Problem("Entity set 'IndustryIncidentContext.Indicators'  is null.");
            }
            var indicator = await _context.Indicators.FindAsync(id);
            if (indicator != null)
            {
                _context.Indicators.Remove(indicator);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IndicatorExists(int id)
        {
          return _context.Indicators.Any(e => e.Id == id);
        }
    }
}
