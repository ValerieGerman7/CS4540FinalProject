using CS4540PS2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Author: Valerie German
/// Date: 18 Oct 2019
/// Course: CS 4540, University of Utah
/// Copyright: CS 4540 and Valerie German - This work may not be copied for use in Academic Coursework.
/// I, Valerie German, certify that I wrote this code from scratch and did not copy it in part or whole from another source. Any references used in the completion of this assignment are cited in my README file.
/// File Contents: This file contains controller for all instructor webpages.
/// </summary>
namespace CS4540PS2.Controllers {
    [Authorize(Roles = "Instructor")]
    public partial class InstructorController : Controller {
        private readonly LOTDBContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;
        /// <summary>
        /// Construct a course controller with a database context.
        /// </summary>
        /// <param name="context"></param>
        public InstructorController(LOTDBContext context, RoleManager<IdentityRole> role) {
            _context = context;
            _roleManager = role;
        }


        /// <summary>
        /// Return the index page listing all course instances belonging to the current professor.
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index() {
            var instances = _context.CourseInstance.Where(i => 
                i.Instructors.Where(ins => ins.User.UserLoginEmail == User.Identity.Name).Any());
            return View(await instances.ToListAsync());
        }

        /// <summary>
        /// Updates the course's note and the date modified.
        /// </summary>
        /// <param name="CourseInstanceId"></param>
        /// <param name="NewNote"></param>
        /// <returns></returns>
        public JsonResult ChangeNote(int CourseInstanceId, string NewNote) {
            CourseInstance course = _context.CourseInstance.Include(c => c.CourseNotes)
                .Where(c => c.CourseInstanceId == CourseInstanceId && c.Instructors.Where(ins => ins.User.UserLoginEmail == User.Identity.Name).Any())
                .FirstOrDefault();
            if (course == null) return Json(new { success = false });
            if(course.CourseNotes.Count == 0) {
                course.CourseNotes.Add(new CourseNotes());
            }
            course.CourseNotes.First().Note = NewNote;
            course.CourseNotes.First().NoteModified = DateTime.Now;
            _context.SaveChanges();
            return Json(new { success = true, noteContent = NewNote, modified = course.CourseNotes.First().NoteModified });
        }

        /// <summary>
        /// Updates the identified learning outcome's note along with the last modified date and last user
        /// modifying date.
        /// </summary>
        /// <param name="LearningOutcomeId"></param>
        /// <param name="NewNote"></param>
        /// <returns></returns>
        public JsonResult ChangeLONote(int LearningOutcomeId, string NewNote) {
            LearningOutcomes lo = _context.LearningOutcomes.Where(l => l.Loid == LearningOutcomeId).Include(l => l.LONotes)
                .Where(l => l.CourseInstance.Instructors.Where(ins => ins.User.UserLoginEmail == User.Identity.Name).Any())
                .FirstOrDefault();
            if (lo == null) return Json(new { success = false });
            if(lo.LONotes.Count == 0) {
                lo.LONotes.Add(new LONotes());
            }
            lo.LONotes.First().Note = NewNote;
            lo.LONotes.First().NoteModified = DateTime.Now;
            lo.LONotes.First().NoteUserModified = User.Identity.Name;
            _context.SaveChanges();
            return Json(new { success = true, noteContent = NewNote, modified = lo.LONotes.First().NoteModified, user = User.Identity.Name });
        }

        /// <summary>
        /// Return a webpage containing information about the selected course.
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
            if (course == null)
                return Forbid();
            return View("Course", course);
        }

    }
}