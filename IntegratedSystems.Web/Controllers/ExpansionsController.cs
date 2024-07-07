using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IntegratedSystems.Domain.Domain_Models;
using IntegratedSystems.Repository;

namespace IntegratedSystems.Web.Controllers
{
    public class ExpansionsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ExpansionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Expansions
        public async Task<IActionResult> Index()
        {
            return View(await _context.Expansions.ToListAsync());
        }

        // GET: Expansions/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expansion = await _context.Expansions
                .FirstOrDefaultAsync(m => m.Id == id);
            if (expansion == null)
            {
                return NotFound();
            }

            return View(expansion);
        }

        // GET: Expansions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Expansions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ExpansionName,ExpansionDescription,ExpansionImage,Rating,CardNumber,Id")] Expansion expansion)
        {
            if (ModelState.IsValid)
            {
                expansion.Id = Guid.NewGuid();
                _context.Add(expansion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(expansion);
        }

        // GET: Expansions/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expansion = await _context.Expansions.FindAsync(id);
            if (expansion == null)
            {
                return NotFound();
            }
            return View(expansion);
        }

        // POST: Expansions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ExpansionName,ExpansionDescription,ExpansionImage,Rating,CardNumber,Id")] Expansion expansion)
        {
            if (id != expansion.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(expansion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExpansionExists(expansion.Id))
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
            return View(expansion);
        }

        // GET: Expansions/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expansion = await _context.Expansions
                .FirstOrDefaultAsync(m => m.Id == id);
            if (expansion == null)
            {
                return NotFound();
            }

            return View(expansion);
        }

        // POST: Expansions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var expansion = await _context.Expansions.FindAsync(id);
            if (expansion != null)
            {
                _context.Expansions.Remove(expansion);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExpansionExists(Guid id)
        {
            return _context.Expansions.Any(e => e.Id == id);
        }
    }
}
