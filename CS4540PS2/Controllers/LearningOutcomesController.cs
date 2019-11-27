using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CS4540PS2.Models;
using Microsoft.AspNetCore.Authorization;

/// <summary>
/// Author: Valerie German
/// Date: 10 Sept 2019
/// Course: CS 4540, University of Utah
/// Copyright: CS 4540 and Valerie German - This work may not be copied for use in Academic Coursework.
/// I, Valerie German, certify that I wrote this code from scratch and did not copy it in part or whole from another source. Any references used in the completion of this assignment are cited in my README file.
/// File Contents: This file contains controller for learning outcome webpages. Admins may create/edit/delete learning outcomes. These actions are only available to admins.
/// </summary>
namespace CS4540PS2.Controllers {
    [Authorize(Roles = "Admin")]
    public class LearningOutcomesController : Controller {
        private readonly LearningOutcomeDBContext _context;
        public LearningOutcomesController(LearningOutcomeDBContext context) {
            _context = context;
        }

        /// <summary>
        /// Returns index page listing all learning outcomes.
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? pageNumber, int resultsPerPage = 10)
        {
            ViewData["PageNumber"] = pageNumber;
            ViewData["resultsPerPage"] = resultsPerPage;
            // Set up the possible ordering schemes of the table.
            ViewData["CurrentSort"] = sortOrder;
            ViewData["TitleSortParam"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["CourseNumSortParam"] = sortOrder == "courseNum_asc" ? "courseNum_desc" : "courseNum_asc";
            
            // If there is a search string, filter by that, otherwise use the default
            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewData["CurrentFilter"] = searchString;

            // get learning outcomes
            var learningOutcomes = _context.LearningOutcomes
                .Include(l => l.CourseInstance)
                .OrderBy(l => l.Name)
                .AsNoTracking();    
            
            if (learningOutcomes.Count() == 0)
                return View();

            // allow the user to search the learning outcomes
            learningOutcomes = FilterBySearch(searchString, learningOutcomes);

            // reorder the results based on the selected filter
            learningOutcomes = OrderBySelection(sortOrder, learningOutcomes);

            return View(await PaginatedList<LearningOutcomes>.CreateAsync(learningOutcomes.AsNoTracking(), pageNumber ?? 1, resultsPerPage));
        }

        /// <summary>
        /// Filters the learning outcomes returned by the database by a user supplied search string.
        /// </summary>
        private IQueryable<LearningOutcomes> FilterBySearch(string searchString, IQueryable<LearningOutcomes> learningOutcomes)
        {
            // allow the user to search
            if (!string.IsNullOrEmpty(searchString))
            {
                string[] searchWords = searchString.Split(' ');
                foreach (string s in searchWords)
                {
                    if (!string.IsNullOrEmpty(s))
                    {
                        learningOutcomes = learningOutcomes.Where(l => l.Name.Contains(s)
                            || l.Description.Contains(s));
                    }
                }
            }
            return learningOutcomes;
        }

        /// <summary>
        /// Reorders the learning outcomees in the view based on user selection
        /// </summary>
        private IQueryable<LearningOutcomes> OrderBySelection(string sortOrder, IQueryable<LearningOutcomes> learningOutcomes)
        {
            // reorder the results based on the selected filter           
            switch (sortOrder)
            {
                case "name_desc":
                    learningOutcomes = learningOutcomes.OrderByDescending(l => l.Name);
                    break;
                case "courseNum_asc":
                    learningOutcomes = learningOutcomes.OrderBy(l => l.CourseInstance.Number);
                    break;
                case "courseNum_desc":
                    learningOutcomes = learningOutcomes.OrderByDescending(l => l.CourseInstance.Number);
                    break;               
                default:
                    learningOutcomes = learningOutcomes.OrderBy(l => l.Name);
                    break;
            }
            return learningOutcomes;
        }

        /// <summary>
        /// Redirects to the course department view page with course with given CourseInstanceId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> RedirectToCourse(int? id) {
            if (id == null)
                return View("Error", new ErrorViewModel() {
                    ErrorMessage = "Insufficient information to locate course."
                });
            using (_context) {
                CourseInstance getid = (from courses in _context.CourseInstance
                                        where courses.CourseInstanceId == id
                                        select courses).FirstOrDefault<CourseInstance>();
                if (getid == null)
                    return View("Error", new ErrorViewModel() {
                        ErrorMessage = "Insufficient information to locate course."
                    });
                return await Course(getid.Department, getid.Number, getid.Semester, getid.Year);
            }
        }
        /// <summary>
        /// Course department view page.
        /// </summary>
        /// <param name="Dept"></param>
        /// <param name="Num"></param>
        /// <param name="Sem"></param>
        /// <param name="Year"></param>
        /// <returns></returns>
        public async Task<IActionResult> Course(string Dept, int? Num, string Sem, int? Year) {
            if (Dept.Equals(null) || Num == null || Sem.Equals(null) || Year == null)
                return View("Error", new ErrorViewModel() {
                    ErrorMessage = "Insufficient information to locate course."
                });
            CourseInstance course = _context.CourseInstance.Where(c => c.Department == Dept && c.Number == Num
                && c.Semester == Sem && c.Year == Year)
                .Include(c => c.CourseNotes)
                .Include(c => c.LearningOutcomes)
                .ThenInclude(lo => lo.EvaluationMetrics)
                .ThenInclude(em => em.SampleFiles)
                .Include(c => c.LearningOutcomes)
                .ThenInclude(lo => lo.LONotes)
                .FirstOrDefault();
            if(course == null) {
                return Forbid();
            }
            return View("Course", course);
        }


        /// <summary>
        /// GET Returns information about a learning outcome
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Details(int? id) {
            if (id == null) {
                return NotFound();
            }

            var learningOutcomes = await _context.LearningOutcomes
                .Include(l => l.CourseInstance)
                .FirstOrDefaultAsync(m => m.Loid == id);
            if (learningOutcomes == null) {
                return NotFound();
            }

            return View(learningOutcomes);
        }

        /// <summary>
        /// GET Returns page for creating a learning outcome.
        /// </summary>
        /// <returns></returns>
        public IActionResult Create() {
            ViewData["CourseInstances"] = _context.CourseInstance.ToList<CourseInstance>();
            return View();
        }

        /// <summary>
        /// POST Creates a new learning outcome.
        /// </summary>
        /// <param name="learningOutcomes"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Loid,Name,Description,CourseInstanceId")] LearningOutcomes learningOutcomes) {
            if (ModelState.IsValid) {
                _context.Add(learningOutcomes);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CourseInstanceId"] = new SelectList(_context.CourseInstance, "CourseInstanceId", "Department", learningOutcomes.CourseInstanceId);
            return View(learningOutcomes);
        }

        /// <summary>
        /// GET Returns page for editing a learning outcome
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Edit(int? id) {
            if (id == null) {
                return NotFound();
            }

            var learningOutcomes = await _context.LearningOutcomes.FindAsync(id);
            if (learningOutcomes == null) {
                return NotFound();
            }
            ViewData["CourseInstanceId"] = new SelectList(_context.CourseInstance, "CourseInstanceId", "Department", learningOutcomes.CourseInstanceId);
            return View(learningOutcomes);
        }

        /// <summary>
        /// POST Edits a learning outcome.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="learningOutcomes"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Loid,Name,Description,CourseInstanceId")] LearningOutcomes learningOutcomes) {
            if (id != learningOutcomes.Loid) {
                return NotFound();
            }

            if (ModelState.IsValid) {
                try {
                    _context.Update(learningOutcomes);
                    await _context.SaveChangesAsync();
                } catch (DbUpdateConcurrencyException) {
                    if (!LearningOutcomesExists(learningOutcomes.Loid)) {
                        return NotFound();
                    } else {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CourseInstanceId"] = new SelectList(_context.CourseInstance, "CourseInstanceId", "Department", learningOutcomes.CourseInstanceId);
            return View(learningOutcomes);
        }

        /// <summary>
        /// Returns webpage for deleting a learning outcome.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Delete(int? id) {
            if (id == null) {
                return NotFound();
            }

            var learningOutcomes = await _context.LearningOutcomes
                .Include(l => l.CourseInstance)
                .FirstOrDefaultAsync(m => m.Loid == id);
            if (learningOutcomes == null) {
                return NotFound();
            }

            return View(learningOutcomes);
        }

        /// <summary>
        /// POST deletes a learning outcome.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id) {
            var learningOutcomes = await _context.LearningOutcomes.FindAsync(id);
            _context.LearningOutcomes.Remove(learningOutcomes);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        /// <summary>
        /// Returns true if the learning outcome with the given ID exists.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private bool LearningOutcomesExists(int id) {
            return _context.LearningOutcomes.Any(e => e.Loid == id);
        }

    }
}
