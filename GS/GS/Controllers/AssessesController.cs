using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GS.Models;

namespace GS.Controllers
{
    public class AssessesController : Controller
    {
        private readonly DACSDbContext _context;

        public AssessesController(DACSDbContext context)
        {
            _context = context;
        }

        // GET: Assesses
        public async Task<IActionResult> Index()
        {
            var dACSDbContext = _context.Assesses.Include(a => a.ApplicationUser);
            return View(await dACSDbContext.ToListAsync());
        }

        // GET: Assesses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assess = await _context.Assesses
                .Include(a => a.ApplicationUser)
                .FirstOrDefaultAsync(m => m.IdAS == id);
            if (assess == null)
            {
                return NotFound();
            }

            return View(assess);
        }

        // GET: Assesses/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Assesses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdAS,Content,TimeAS,RoleAS,UserId")] Assess assess)
        {
            if (ModelState.IsValid)
            {
                _context.Add(assess);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", assess.UserId);
            return View(assess);
        }

        // GET: Assesses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assess = await _context.Assesses.FindAsync(id);
            if (assess == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", assess.UserId);
            return View(assess);
        }

        // POST: Assesses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdAS,Content,TimeAS,RoleAS,UserId")] Assess assess)
        {
            if (id != assess.IdAS)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(assess);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AssessExists(assess.IdAS))
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
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", assess.UserId);
            return View(assess);
        }

        // GET: Assesses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assess = await _context.Assesses
                .Include(a => a.ApplicationUser)
                .FirstOrDefaultAsync(m => m.IdAS == id);
            if (assess == null)
            {
                return NotFound();
            }

            return View(assess);
        }

        // POST: Assesses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var assess = await _context.Assesses.FindAsync(id);
            if (assess != null)
            {
                _context.Assesses.Remove(assess);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AssessExists(int id)
        {
            return _context.Assesses.Any(e => e.IdAS == id);
        }
    }
}
