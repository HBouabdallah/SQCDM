using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IndustryIncident.Models;

namespace IndustryIncident.Controllers
{
    public class IncidentTypesController : Controller
    {
        private readonly IndustryIncidentContext _context;

        public IncidentTypesController(IndustryIncidentContext context)
        {
            _context = context;
        }

        // GET: IncidentTypes
        public async Task<IActionResult> Index()
        {
            if (!this.User.IsInRole("Admin"))
            {
                return RedirectToAction("index", "notfound");

            }
            return View(await _context.IncidentTypes.ToListAsync());
        }

        // GET: IncidentTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (!this.User.IsInRole("Admin"))
            {
                return RedirectToAction("index", "notfound");

            }
            if (id == null || _context.IncidentTypes == null)
            {
                return NotFound();
            }

            var incidentType = await _context.IncidentTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (incidentType == null)
            {
                return NotFound();
            }

            return View(incidentType);
        }

        // GET: IncidentTypes/Create
        public IActionResult Create()
        {
            if (!this.User.IsInRole("Admin"))
            {
                return RedirectToAction("index", "notfound");

            }
            return View();
        }

        // POST: IncidentTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Type")] IncidentType incidentType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(incidentType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(incidentType);
        }

        // GET: IncidentTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (!this.User.IsInRole("Admin"))
            {
                return RedirectToAction("index", "notfound");

            }
            if (id == null || _context.IncidentTypes == null)
            {
                return NotFound();
            }

            var incidentType = await _context.IncidentTypes.FindAsync(id);
            if (incidentType == null)
            {
                return NotFound();
            }
            return View(incidentType);
        }

        // POST: IncidentTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Type")] IncidentType incidentType)
        {
            if (!this.User.IsInRole("Admin"))
            {
                return RedirectToAction("index", "notfound");

            }
            if (id != incidentType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(incidentType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IncidentTypeExists(incidentType.Id))
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
            return View(incidentType);
        }

        // GET: IncidentTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (!this.User.IsInRole("Admin"))
            {
                return RedirectToAction("index", "notfound");

            }
            if (id == null || _context.IncidentTypes == null)
            {
                return NotFound();
            }

            var incidentType = await _context.IncidentTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (incidentType == null)
            {
                return NotFound();
            }

            return View(incidentType);
        }

        // POST: IncidentTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.IncidentTypes == null)
            {
                return Problem("Entity set 'IndustryIncidentContext.IncidentTypes'  is null.");
            }
            var incidentType = await _context.IncidentTypes.FindAsync(id);
            if (incidentType != null)
            {
                _context.IncidentTypes.Remove(incidentType);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IncidentTypeExists(int id)
        {
          return _context.IncidentTypes.Any(e => e.Id == id);
        }
    }
}
