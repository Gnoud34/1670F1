﻿using System;
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
    public class JobPostsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public JobPostsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: JobPosts
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.JobPost.Include(j => j.Category).Include(j => j.Employer);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: JobPosts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobPost = await _context.JobPost
                .Include(j => j.Category)
                .Include(j => j.Employer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (jobPost == null)
            {
                return NotFound();
            }

            return View(jobPost);
        }

        // GET: JobPosts/Create
        public IActionResult Create()
        {
            ViewData["CategoryName"] = new SelectList(_context.Category, "Id", "CategoryName");
            ViewData["FullName"] = new SelectList(_context.Set<Employer>(), "Id", "FullName");
            return View();
        }

        // POST: JobPosts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,JobName,JobDescription,Location,RequireSkill,Salary,CategoryId,EmployerId")] JobPost jobPost)
        {
            if (ModelState.IsValid)
            {
                _context.Add(jobPost);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryName"] = new SelectList(_context.Category, "Id", "CategoryName", jobPost.CategoryId);
            ViewData["FullName"] = new SelectList(_context.Set<Employer>(), "Id", "FullName", jobPost.EmployerId);
            return View(jobPost);
        }

        // GET: JobPosts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobPost = await _context.JobPost.FindAsync(id);
            if (jobPost == null)
            {
                return NotFound();
            }
            ViewData["CategoryName"] = new SelectList(_context.Category, "Id", "CategoryName", jobPost.CategoryId);
            ViewData["FullName"] = new SelectList(_context.Set<Employer>(), "Id", "FullName", jobPost.EmployerId);
            return View(jobPost);
        }

        // POST: JobPosts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,JobName,JobDescription,Location,RequireSkill,Salary,CategoryId,EmployerId")] JobPost jobPost)
        {
            if (id != jobPost.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(jobPost);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JobPostExists(jobPost.Id))
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
            ViewData["CategoryName"] = new SelectList(_context.Category, "Id", "CategoryName", jobPost.CategoryId);
            ViewData["FullName"] = new SelectList(_context.Set<Employer>(), "Id", "FullName", jobPost.EmployerId);
            return View(jobPost);
        }

        // GET: JobPosts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobPost = await _context.JobPost
                .Include(j => j.Category)
                .Include(j => j.Employer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (jobPost == null)
            {
                return NotFound();
            }

            return View(jobPost);
        }

        // POST: JobPosts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var jobPost = await _context.JobPost.FindAsync(id);
            _context.JobPost.Remove(jobPost);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool JobPostExists(int id)
        {
            return _context.JobPost.Any(e => e.Id == id);
        }
    }
}
