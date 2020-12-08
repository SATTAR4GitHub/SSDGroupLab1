using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using techloops.Data;
using techloops.Models;

namespace techloops.Controllers
{
    public class FreelancersController : Controller
    {
        private readonly MainDbContext _context;

        public FreelancersController(MainDbContext context)
        {
            _context = context;
        }

        // GET: Freelancers
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index(string sortOrder, string searchString, 
            string currentFilter, int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["FirstNameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "firstName_asc" : "";
            ViewData["LastNameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "lastName_desc" : "";
            ViewData["CountrySortParm"] = String.IsNullOrEmpty(sortOrder) ? "country_asc" : "";
            ViewData["DateSortParm"] = String.IsNullOrEmpty(sortOrder) ? "date_desc" : "";

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            // Filter parameter
            ViewData["CurrentFilter"] = searchString;

            var freelancers = from m in _context.Freelancers
                              select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                freelancers = freelancers.Where(m => m.Country.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "firstName_asc":
                    freelancers = freelancers.OrderBy(m => m.FirstName);
                    break;
                case "lastName_desc":
                    freelancers = freelancers.OrderByDescending(m => m.LastName);
                    break;
                case "country_asc":
                    freelancers = freelancers.OrderBy(m => m.Country);
                    break;
                case "date_desc":
                    freelancers = freelancers.OrderByDescending(m => m.DateApplied);
                    break;
                default:
                    freelancers = freelancers.OrderBy(m => m.Id);
                    break;
            }

            int pageSize = 10;

            //return View(await freelancers.AsNoTracking().ToListAsync());
            return View(await Pagination<Freelancer>.CreateAsync(freelancers.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        // GET: Freelancers/Details/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var freelancer = await _context.Freelancers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (freelancer == null)
            {
                return NotFound();
            }

            return View(freelancer);
        }

        // GET: Freelancers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Freelancers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,Email,Skills,DateApplied,Country")] Freelancer freelancer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(freelancer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Confirmation));
            }
            return View(freelancer);
        }

        // GET: Freelancers/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var freelancer = await _context.Freelancers.FindAsync(id);
            if (freelancer == null)
            {
                return NotFound();
            }
            return View(freelancer);
        }

        // POST: Freelancers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,Email,Skills,DateApplied,Country")] Freelancer freelancer)
        {
            if (id != freelancer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(freelancer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FreelancerExists(freelancer.Id))
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
            return View(freelancer);
        }

        // GET: Freelancers/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var freelancer = await _context.Freelancers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (freelancer == null)
            {
                return NotFound();
            }

            return View(freelancer);
        }

        // POST: Freelancers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var freelancer = await _context.Freelancers.FindAsync(id);
            _context.Freelancers.Remove(freelancer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Confirmation()
        {
            return View(await _context.Freelancers.ToListAsync());
        }

        private bool FreelancerExists(int id)
        {
            return _context.Freelancers.Any(e => e.Id == id);
        }
    }
}
