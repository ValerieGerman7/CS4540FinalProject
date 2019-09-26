using CS4540PS2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

/// <summary>
/// Author: Valerie German
/// Date: 25 Sept 2019
/// Course: CS 4540, University of Utah
/// Copyright: CS 4540 and Valerie German - This work may not be copied for use in Academic Coursework.
/// I, Valerie German, certify that I wrote this code from scratch and did not copy it in part or whole from another source. Any references used in the completion of this assignment are cited in my README file.
/// File Contents: This file contains controller for admin course webpages - creating/editing/deleting courses and course overview.
/// </summary>
namespace CS4540PS2.Controllers {
    [Authorize(Roles="Admin")]
    public class CourseController : Controller {
        private readonly LearningOutcomeDBContext _context;
        /// <summary>
        /// Construct a course controller with a database context.
        /// </summary>
        /// <param name="context"></param>
        public CourseController(LearningOutcomeDBContext context) {
            _context = context;
        }

        /// <summary>
        /// Return the index page listing all course instances.
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index() {
            var instances = _context.CourseInstance;
            return View(await instances.ToListAsync());
        }
        
        /// <summary>
        /// GET for course create page.
        /// </summary>
        /// <returns></returns>
        public IActionResult Create() {
            ViewData["CourseInstanceId"] = new SelectList(_context.CourseInstance, "CourseInstanceId", "Department");
            return View();
        }

        /// <summary>
        /// POST for creating a new course.
        /// </summary>
        /// <param name="courseInstance"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Description,Department,Number,Semester,Year")] CourseInstance courseInstance) {
            if (ModelState.IsValid) {
                _context.Add(courseInstance);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CourseInstanceId"] = new SelectList(_context.CourseInstance, "CourseInstanceId", "Department", courseInstance.CourseInstanceId);
            return View(courseInstance);
        }

        /// <summary>
        /// Get for course editing page.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Edit(int? id) {
            if (id == null) {
                return NotFound();
            }

            var courseInstance = await _context.CourseInstance.FindAsync(id);
            if (courseInstance == null) {
                return NotFound();
            }
            ViewData["CourseInstanceId"] = new SelectList(_context.CourseInstance, "CourseInstanceId", "Department", courseInstance.CourseInstanceId);
            return View(courseInstance);
        }

        /// <summary>
        /// POST for editing a course.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="courseInstance"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CourseInstanceId,Name,Description,Department,Number,Semester,Year")] CourseInstance courseInstance) {
            if (id != courseInstance.CourseInstanceId) {
                return NotFound();
            }

            if (ModelState.IsValid) {
                try {
                    _context.Update(courseInstance);
                    await _context.SaveChangesAsync();
                } catch (DbUpdateConcurrencyException) {
                    if (!CourseInstanceExists(courseInstance.CourseInstanceId)) {
                        return NotFound();
                    } else {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CourseInstanceId"] = new SelectList(_context.CourseInstance, "CourseInstanceId", "Department", courseInstance.CourseInstanceId);
            return View(courseInstance);
        }

        /// <summary>
        /// GET for deleting course.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Delete(int? id) {
            if (id == null) {
                return NotFound();
            }

            var courseInstance = await _context.CourseInstance
                .FirstOrDefaultAsync(m => m.CourseInstanceId == id);
            if (courseInstance == null) {
                return NotFound();
            }

            return View(courseInstance);
        }

        /// <summary>
        /// Post for deleting a course.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id) {
            var courseInstance = await _context.CourseInstance.FindAsync(id);
            _context.CourseInstance.Remove(courseInstance);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Returns true if a course with the given id exists.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private bool CourseInstanceExists(int id) {
            return _context.CourseInstance.Any(e => e.CourseInstanceId == id);
        }


    }

    /// <summary>
    /// Struct for holding information about a specific course
    /// </summary>
    public struct CourseInfo {
        public string CourseName { get; set; }
        public string CourseDescription { get; set; }
        public string Department { get; set; }
        public int Number { get; set; }
        public string Semester { get; set; }
        public int Year { get; set; }
        public int ID { get; set; }
        public List<LearningOutcomeData> LearningOutcomes { get; set; }
    }
    /// <summary>
    /// Struct for holding information about a course's learning outcome.
    /// </summary>
    public struct LearningOutcomeData {
        public string LOName { get; set; }
        public string LODescription { get; set; }
        public int LOID { get; set; }
        public List<EvaluationMetricData> EvaluationMetrics { get; set; }
    }
    /// <summary>
    /// Struct for holding information about an evalutation metric.
    /// </summary>
    public struct EvaluationMetricData {
        public string Name { get; set; }
        public string Description { get; set; }
        public int EMID { get; set; }
        public List<SamplesData> Samples { get; set; }
    }
    /// <summary>
    /// Struct for holding information about a sample.
    /// </summary>
    public struct SamplesData {
        public string FileName { get; set; }
        public int Score { get; set; }
        public int SID { get; set; }
    }

}