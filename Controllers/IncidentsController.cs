using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IndustryIncident.Models;
using IndustryIncident.Models.ViewModels;
using System.Data.Entity.Validation;
using System.Security.Policy;
using Serilog;

namespace IndustryIncident.Controllers
{
    public class IncidentsController : Controller
    {
        private readonly IndustryIncidentContext _context;

        public Serilog.ILogger _logger { get; }

        public IncidentsController(IndustryIncidentContext context, Serilog.ILogger logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: Incidents
        public async Task<IActionResult> Index()
        {
            try { 
            var currentUser = _context.Users.FirstOrDefault(x => x.Id == this.User.Identity.Name);
            if (currentUser == null)
            {
                return RedirectToAction("index", "notfound");

            }
            var userzone = _context.UserAcces.FirstOrDefault(x => x.Iduser == this.User.Identity.Name);

            var industryIncidentContext = _context.Incidents;
            var listIncid = await industryIncidentContext.ToListAsync();
            var listViewModel = new List<IncidentViewModel>();
            foreach (var incid in listIncid)
            {
                var zone = _context.Zones.FirstOrDefault(x => x.Id == incid.Zone);
                var type = _context.IncidentTypes.FirstOrDefault(x => x.Id == incid.Type);
                var indicator = _context.Indicators.FirstOrDefault(x => x.Id == incid.Indicator);
                if (incid.Zone == userzone.Idzone)
                {
                    listViewModel.Add(new IncidentViewModel()
                    {
                        Zone = zone,
                        Indicator = indicator,
                        Type = type,
                        Iduser = incid.Iduser,
                        Date = incid.Date,
                        Description = incid.Description,
                        Id = incid.Id

                    });
                }
            }
            return View(listViewModel);
            }catch(Exception ex)
            {
                _logger.Error(ex.Message);
                return RedirectToAction("index", "notfound");

            }
        }

        // GET: Incidents/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var currentUser = _context.Users.FirstOrDefault(x => x.Id == this.User.Identity.Name);

            if (currentUser == null)
            {
                return RedirectToAction("index", "notfound");

            }
            if (id == null || _context.Incidents == null)
            {
                return NotFound();
            }

            var incident = await _context.Incidents

                .FirstOrDefaultAsync(m => m.Id == id);
            if (incident == null)
            {
                return NotFound();
            }
            var zone = _context.Zones.FirstOrDefault(x => x.Id == incident.Zone);
            var type = _context.IncidentTypes.FirstOrDefault(x => x.Id == incident.Type);
            var indicator = _context.Indicators.FirstOrDefault(x => x.Id == incident.Indicator);

            IncidentViewModel viewModel = new IncidentViewModel()
            {
                Zone = zone,
                Indicator = indicator,
                Type = type,
                Iduser = incident.Iduser,
                Date = incident.Date,
                Description = incident.Description,
                Id = incident.Id
            };

            return View(viewModel);
        }

        // GET: Incidents/Create
        public IActionResult Create()
        {
            var currentUser = _context.Users.FirstOrDefault(x => x.Id == this.User.Identity.Name);

            if (currentUser == null)
            {
                return RedirectToAction("index", "notfound");

            }
            var userzone = _context.UserAcces.FirstOrDefault(x => x.Iduser == this.User.Identity.Name);
            ViewData["Indicator"] = new SelectList(_context.Indicators, "Id", "Name");
            ViewData["Type"] = new SelectList(_context.IncidentTypes, "Id", "Type");
            ViewData["Zone"] = new SelectList(_context.Zones.Where(x => x.Id == userzone.Idzone), "Id", "Name");
            return View();
        }

        // POST: Incidents/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Iduser,Type,Description,Zone,Date,Indicator")] Incident incident)
        {
            if (ModelState.IsValid)
            {
                _context.Add(incident);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            incident.Iduser = this.User.Identity.Name;
            ViewData["Indicator"] = new SelectList(_context.Indicators, "Id", "Name", incident.Indicator);
            ViewData["Type"] = new SelectList(_context.IncidentTypes, "Id", "Type", incident.Type);
            ViewData["Zone"] = new SelectList(_context.Zones, "Id", "Name", incident.Zone);
            return View(incident);
        }

        // GET: Incidents/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var currentUser = _context.Users.FirstOrDefault(x => x.Id == this.User.Identity.Name);

            if (currentUser == null)
            {
                return RedirectToAction("index", "notfound");

            }
            if (id == null || _context.Incidents == null)
            {
                return NotFound();
            }

            var incident = await _context.Incidents.FindAsync(id);
            if (incident == null)
            {
                return NotFound();
            }
            incident.Iduser = this.User.Identity.Name;
            var userzone = _context.UserAcces.FirstOrDefault(x => x.Iduser == incident.Iduser);
            ViewData["Indicator"] = new SelectList(_context.Indicators, "Id", "Name", incident.Indicator);
            ViewData["Type"] = new SelectList(_context.IncidentTypes, "Id", "Type", incident.Type);
            ViewData["Zone"] = new SelectList(_context.Zones.Where(x => x.Id == userzone.Idzone), "Id", "Name", incident.Zone);
            return View(incident);
        }

        // POST: Incidents/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Iduser,Type,Description,Zone,Date,Indicator")] Incident incident)
        {
            if (id != incident.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(incident);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IncidentExists(incident.Id))
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
            incident.Iduser = this.User.Identity.Name;
            ViewData["Indicator"] = new SelectList(_context.Indicators, "Id", "Name", incident.Indicator);
            ViewData["Type"] = new SelectList(_context.IncidentTypes, "Id", "Type", incident.Type);
            ViewData["Zone"] = new SelectList(_context.Zones, "Id", "Name", incident.Zone);
            return View(incident);
        }

        // GET: Incidents/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            var currentUser = _context.Users.FirstOrDefault(x => x.Id == this.User.Identity.Name);

            if (currentUser == null)
            {
                return RedirectToAction("index", "notfound");

            }
            if (_context.Incidents == null)
            {
                return Problem("Entity set 'IndustryIncidentContext.Incidents'  is null.");
            }
            var incident = await _context.Incidents.FindAsync(id);
            if (incident != null)
            {
                _context.Incidents.Remove(incident);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // POST: Incidents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Incidents == null)
            {
                return Problem("Entity set 'IndustryIncidentContext.Incidents'  is null.");
            }
            var incident = await _context.Incidents.FindAsync(id);
            if (incident != null)
            {
                _context.Incidents.Remove(incident);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IncidentExists(int id)
        {
            return _context.Incidents.Any(e => e.Id == id);
        }
    }
}
