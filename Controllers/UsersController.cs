using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IndustryIncident.Models;
using IndustryIncident.Models.ViewModels;
using System.Data.Entity;

namespace IndustryIncident.Controllers
{
    public class UsersController : Controller
    {
        private readonly IndustryIncidentContext _context;

        public UsersController(IndustryIncidentContext context)
        {
            _context = context;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            if (!this.User.IsInRole("Admin"))
            {
                return RedirectToAction("index", "notfound");

            }
            var listUser = _context.Users.ToList();

            var listViewModel = new List<UserViewModel>();
            foreach (var user in listUser)
            {
                var acces = from userAccess in _context.UserAcces
                            join zone in _context.Zones on userAccess.Idzone equals zone.Id
                            join u in _context.Users on userAccess.Iduser equals u.Id
                            where u.Id == user.Id
                            select zone;
                var role = from userRole in _context.UserRoles
                           join r in _context.Roles on userRole.Idrole equals r.Id
                           join u in _context.Users on userRole.Iduser equals u.Id
                           where u.Id == user.Id

                           select r;
                listViewModel.Add(new UserViewModel()
                {
                    Acces = acces.FirstOrDefault(),
                    Role = role.FirstOrDefault(),
                    Title = user.Title,
                    Name = user.Name,
                    FamillyName = user.FamillyName,
                    Id = user.Id
                });
            }
            return View(listViewModel);
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (!this.User.IsInRole("Admin"))
            {
                return RedirectToAction("index", "notfound");

            }
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var user = _context.Users
                .FirstOrDefault(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            var acces = from userAccess in _context.UserAcces
                        join zone in _context.Zones on userAccess.Idzone equals zone.Id
                        join u in _context.Users on userAccess.Iduser equals u.Id
                        where u.Id == user.Id
                        select zone;
            var role = from userRole in _context.UserRoles
                       join r in _context.Roles on userRole.Idrole equals r.Id
                       join u in _context.Users on userRole.Iduser equals u.Id
                       where u.Id == user.Id
                       select r;
            var listViewModel = new UserViewModel()
            {
                Acces = acces.FirstOrDefault(),
                Role = role.FirstOrDefault(),
                Title = user.Title,
                Name = user.Name,
                FamillyName = user.FamillyName,
                Id = user.Id
            };
            return View(listViewModel);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            if (!this.User.IsInRole("Admin"))
            {
                return RedirectToAction("index", "notfound");

            }
            ViewData["Role"] = new SelectList(_context.Roles, "Id", "Role1");
            ViewData["Zone"] = new SelectList(_context.Zones, "Id", "Name");
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,FamillyName,Title,AccesID,RoleID")] UserViewModel userViewModel)
        {
            ModelState.Remove("Role.Role1");
            ModelState.Remove("Acces.Name");
            if (ModelState.IsValid)
            {
                var user = new User()
                {
                    Id = userViewModel.Id,
                    FamillyName = userViewModel.FamillyName,
                    Name = userViewModel.Name,
                    Title = userViewModel.Title
                };
                var userRole = new UserRole()
                {
                    Iduser = user.Id,
                    Idrole = userViewModel.RoleID
                };
                var userAcces = new UserAcce()
                {
                    Iduser = user.Id,
                    Idzone = userViewModel.AccesID
                };
                _context.Add(user);
                _context.Add(userAcces);
                _context.Add(userRole);
                await _context.SaveChangesAsync();
                ViewData["Role"] = new SelectList(_context.Roles, "Id", "Role1", userViewModel.RoleID);
                ViewData["Zone"] = new SelectList(_context.Zones, "Id", "Name", userViewModel.AccesID);
                return RedirectToAction(nameof(Index));
            }
            return View(userViewModel);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (!this.User.IsInRole("Admin"))
            {
                return RedirectToAction("index", "notfound");

            }
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            var acces = from userAccess in _context.UserAcces
                        join zone in _context.Zones on userAccess.Idzone equals zone.Id
                        join u in _context.Users on userAccess.Iduser equals u.Id
                        where u.Id == user.Id
                        select zone;
            var role = from userRole in _context.UserRoles
                       join r in _context.Roles on userRole.Idrole equals r.Id
                       join u in _context.Users on userRole.Iduser equals u.Id
                       where u.Id == user.Id
                       select r;
            var userViewModel = new UserViewModel()
            {
                Acces = acces.FirstOrDefault(),
                Role = role.FirstOrDefault(),
                Title = user.Title,
                Name = user.Name,
                FamillyName = user.FamillyName,
                Id = user.Id,
                AccesID = acces.FirstOrDefault().Id,
                RoleID = role.FirstOrDefault().Id
            };
            ViewData["Role"] = new SelectList(_context.Roles, "Id", "Role1", userViewModel.RoleID);
            ViewData["Zone"] = new SelectList(_context.Zones, "Id", "Name", userViewModel.AccesID);

            return View(userViewModel);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Name,FamillyName,Title,AccesID,RoleID")] UserViewModel userViewModel)
        {
            ModelState.Remove("Role.Role1");
            ModelState.Remove("Acces.Name");
            if (id != userViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var user = new User()
                    {
                        Id = userViewModel.Id,
                        FamillyName = userViewModel.FamillyName,
                        Name = userViewModel.Name,
                        Title = userViewModel.Title
                    };
                    var userRole = new UserRole()
                    {
                        Iduser = user.Id,
                        Idrole = userViewModel.RoleID
                    };
                    var userAcces = new UserAcce()
                    {
                        Iduser = user.Id,
                        Idzone = userViewModel.AccesID
                    };
                    var currentRole = _context.UserAcces.FirstOrDefault(x => x.Iduser == user.Id);
                    var currentAcces = _context.UserRoles.FirstOrDefault(x => x.Iduser == user.Id);
                    if (currentRole != null) _context.Remove(currentRole);
                    if (currentAcces != null) _context.Remove(currentAcces);
                    await _context.SaveChangesAsync();
                    _context.Update(user);
                    _context.Add(userAcces);
                    _context.Add(userRole);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(userViewModel.Id))
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
            ViewData["Role"] = new SelectList(_context.Roles, "Id", "Role1", userViewModel.RoleID);
            ViewData["Zone"] = new SelectList(_context.Zones, "Id", "Name", userViewModel.AccesID);
            return View(userViewModel);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (_context.Users == null)
            {
                return Problem("Entity set 'IndustryIncidentContext.Users'  is null.");
            }
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Users == null)
            {
                return Problem("Entity set 'IndustryIncidentContext.Users'  is null.");
            }
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(string id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
