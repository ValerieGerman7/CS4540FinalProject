using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CS4540PS2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

/// <summary>
/// Author: Valerie German
/// Date: 18 Oct 2019
/// Course: CS 4540, University of Utah
/// Copyright: CS 4540 and Valerie German - This work may not be copied for use in Academic Coursework.
/// I, Valerie German, certify that I wrote this code from scratch and did not copy it in part or whole from another source. Any references used in the completion of this assignment are cited in my README file.
/// File Contents: This file contains controller for department webpages. Chair pages for viewing departments and courses.
/// </summary>
namespace CS4540PS2.Controllers {
    [Authorize(Roles = "Chair")]
    public class DepartmentController : Controller {
        private readonly LOTDBContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;
        public DepartmentController(LOTDBContext context, RoleManager<IdentityRole> role) {
            _context = context;
            _roleManager = role;
        }

        /// <summary>
        /// Returns a list of departments.
        /// </summary>
        /// <returns></returns>
        public IActionResult Index() {
            using (_context) {
                var getDepts = from courses in _context.CourseInstance
                               group courses by courses.Department into deptGroup
                               orderby deptGroup.Key
                               select deptGroup.Key;
                return View("Index", getDepts.ToList<string>());
            }
        }

        /// <summary>
        /// Returns a course overview (chair view) containing information about a course, its learning
        /// outcomes, evaluation metrics and sample files.
        /// </summary>
        /// <param name="CourseId"></param>
        /// <returns></returns>
        public async Task<IActionResult> Course(int CourseId) {
            CourseInstance course = await _context.CourseInstance
                .Include(lo => lo.LearningOutcomes)
                .ThenInclude(em => em.EvaluationMetrics)
                .ThenInclude(sa => sa.SampleFiles)
                .Include(lo => lo.LearningOutcomes)
                .ThenInclude(no => no.LONotes)
                .Where(c => c.CourseInstanceId == CourseId).FirstOrDefaultAsync();
            if (course == null) {
                return NotFound();
            }
            return View("Course", course);
        }

        /// <summary>
        /// Updates the identified learning outcome's note along with the last modified date and last user
        /// modifying date.
        /// </summary>
        /// <param name="LearningOutcomeId"></param>
        /// <param name="NewNote"></param>
        /// <returns></returns>
        public JsonResult ChangeNote(int LearningOutcomeId, string NewNote) {
            LearningOutcomes lo = _context.LearningOutcomes.Include(l => l.LONotes)
                .Where(l => l.Loid == LearningOutcomeId).FirstOrDefault();
            if (lo == null) return Json(new { success = false });
            if (lo.LONotes.Count == 0) {
                lo.LONotes.Add(new LONotes());
            }
            lo.LONotes.First().Note = NewNote;
            lo.LONotes.First().NoteModified = DateTime.Now;
            lo.LONotes.First().NoteUserModified = User.Identity.Name;
            _context.SaveChanges();
            return Json(new { success = true, noteContent = NewNote, modified = lo.LONotes.First().NoteModified, user = User.Identity.Name });
        }

        /// <summary>
        /// Returns a department page, listing classes with the given department
        /// code.
        /// </summary>
        /// <param name="DeptCode"></param>
        /// <returns></returns>
        public async Task<IActionResult> Department(string DeptCode) {
            if (DeptCode == null) { DeptCode = "CS"; } //Temp for viewing
            return View("Department", _context.CourseInstance.Include(c => c.LearningOutcomes)
                .ThenInclude(lo => lo.EvaluationMetrics).ThenInclude(em => em.SampleFiles)
                .Where(c => c.Department == DeptCode));
        }
    }
}