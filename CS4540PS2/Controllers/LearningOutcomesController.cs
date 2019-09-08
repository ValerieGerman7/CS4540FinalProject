using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CS4540PS2.Models;

namespace CS4540PS2.Controllers
{
    public class LearningOutcomesController : Controller
    {
        private readonly LearningOutcomeDBContext _context;

        public LearningOutcomesController(LearningOutcomeDBContext context)
        {
            _context = context;
        }

        // GET: LearningOutcomes
        public async Task<IActionResult> Index()
        {
            var learningOutcomeDBContext = _context.LearningOutcomes.Include(l => l.CourseInstance);
            return View(await learningOutcomeDBContext.ToListAsync());
        }

        // GET: LearningOutcomes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var learningOutcomes = await _context.LearningOutcomes
                .Include(l => l.CourseInstance)
                .FirstOrDefaultAsync(m => m.Loid == id);
            if (learningOutcomes == null)
            {
                return NotFound();
            }

            return View(learningOutcomes);
        }

        // GET: LearningOutcomes/Create
        public IActionResult Create()
        {
            ViewData["CourseInstanceId"] = new SelectList(_context.CourseInstance, "CourseInstanceId", "Department");
            return View();
        }

        // POST: LearningOutcomes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Loid,Name,Description,CourseInstanceId")] LearningOutcomes learningOutcomes)
        {
            if (ModelState.IsValid)
            {
                _context.Add(learningOutcomes);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CourseInstanceId"] = new SelectList(_context.CourseInstance, "CourseInstanceId", "Department", learningOutcomes.CourseInstanceId);
            return View(learningOutcomes);
        }

        // GET: LearningOutcomes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var learningOutcomes = await _context.LearningOutcomes.FindAsync(id);
            if (learningOutcomes == null)
            {
                return NotFound();
            }
            ViewData["CourseInstanceId"] = new SelectList(_context.CourseInstance, "CourseInstanceId", "Department", learningOutcomes.CourseInstanceId);
            return View(learningOutcomes);
        }

        // POST: LearningOutcomes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Loid,Name,Description,CourseInstanceId")] LearningOutcomes learningOutcomes)
        {
            if (id != learningOutcomes.Loid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(learningOutcomes);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LearningOutcomesExists(learningOutcomes.Loid))
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
            ViewData["CourseInstanceId"] = new SelectList(_context.CourseInstance, "CourseInstanceId", "Department", learningOutcomes.CourseInstanceId);
            return View(learningOutcomes);
        }

        // GET: LearningOutcomes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var learningOutcomes = await _context.LearningOutcomes
                .Include(l => l.CourseInstance)
                .FirstOrDefaultAsync(m => m.Loid == id);
            if (learningOutcomes == null)
            {
                return NotFound();
            }

            return View(learningOutcomes);
        }

        // POST: LearningOutcomes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var learningOutcomes = await _context.LearningOutcomes.FindAsync(id);
            _context.LearningOutcomes.Remove(learningOutcomes);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LearningOutcomesExists(int id)
        {
            return _context.LearningOutcomes.Any(e => e.Loid == id);
        }
    }
}
