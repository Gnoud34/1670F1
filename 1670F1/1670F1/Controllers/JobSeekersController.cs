using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using _1670F1.Data;
using _1670F1.Models;

namespace _1670F1.Controllers
{
    public class JobSeekersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public JobSeekersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: JobSeekers
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.JobSeeker.Include(j => j.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: JobSeekers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobSeeker = await _context.JobSeeker
                .Include(j => j.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (jobSeeker == null)
            {
                return NotFound();
            }

            return View(jobSeeker);
        }

        // GET: JobSeekers/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.Set<User>(), "Id", "Id");
            return View();
        }

        // POST: JobSeekers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FullName,DateTime,Gender,Age,Email,PhoneNumber,Image,UserId")] JobSeeker jobSeeker)
        {
            if (ModelState.IsValid)
            {
                _context.Add(jobSeeker);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Set<User>(), "Id", "Id", jobSeeker.UserId);
            return View(jobSeeker);
        }

        // GET: JobSeekers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobSeeker = await _context.JobSeeker.FindAsync(id);
            if (jobSeeker == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Set<User>(), "Id", "Id", jobSeeker.UserId);
            return View(jobSeeker);
        }

        // POST: JobSeekers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FullName,DateTime,Gender,Age,Email,PhoneNumber,Image,UserId")] JobSeeker jobSeeker)
        {
            if (id != jobSeeker.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(jobSeeker);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JobSeekerExists(jobSeeker.Id))
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
            ViewData["UserId"] = new SelectList(_context.Set<User>(), "Id", "Id", jobSeeker.UserId);
            return View(jobSeeker);
        }

        // GET: JobSeekers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobSeeker = await _context.JobSeeker
                .Include(j => j.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (jobSeeker == null)
            {
                return NotFound();
            }

            return View(jobSeeker);
        }

        // POST: JobSeekers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var jobSeeker = await _context.JobSeeker.FindAsync(id);
            _context.JobSeeker.Remove(jobSeeker);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool JobSeekerExists(int id)
        {
            return _context.JobSeeker.Any(e => e.Id == id);
        }
    }
}
