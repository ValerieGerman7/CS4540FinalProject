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
/// Date: 6 Dec 2019
/// Course: CS 4540, University of Utah
/// Copyright: CS 4540 and Valerie German - This work may not be copied for use in Academic Coursework.
/// I, Valerie German, certify that I wrote this code from scratch and did not copy it in part or whole from another source. Any references used in the completion of this assignment are cited in my README file.
/// File Contents: This file contains controller for department webpages. Chair pages for viewing departments and courses. Chairs may also manage course notifications.
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
            return View("Index", _context.Departments.OrderBy(d => d.Code));
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
                .Include(c => c.Status)
                .Include(c => c.Instructors)
                .ThenInclude(i => i.User)
                .Where(c => c.CourseInstanceId == CourseId).FirstOrDefaultAsync();
            if (course == null) {
                return NotFound();
            }
            return View("Course", course);
        }

        /// <summary>
        /// Update the course's due date. Returns success boolean in JsonResult.
        /// </summary>
        /// <param name="courseId"></param>
        /// <param name="newDueDate"></param>
        /// <returns></returns>
        public async Task<IActionResult> UpdateCourseDueDate(int? courseId, DateTime newDueDate) {
            if(courseId == null || newDueDate == null) {
                return new JsonResult(new { success = false });
            }
            CourseInstance course = await _context.CourseInstance.Where(c => c.CourseInstanceId == courseId).FirstOrDefaultAsync();
            if (course == null) new JsonResult(new { success = false });
            course.DueDate = newDueDate;
            _context.SaveChanges();
            return new JsonResult(new { success = true });
        }

        /// <summary>
        /// Sets the course status to approved for the given course.
        /// </summary>
        /// <param name="courseId"></param>
        /// <returns></returns>
        public async Task<IActionResult> ApproveCourse(int? courseId) {
            if (courseId == null) {
                return new JsonResult(new { success = false });
            }
            CourseInstance course = await _context.CourseInstance.Include(c => c.Status).Include(c => c.Instructors).ThenInclude(i => i.User)
                .Where(c => c.CourseInstanceId == courseId).FirstOrDefaultAsync();
            CourseStatus complete = _context.CourseStatus.Where(s => s.Status == CourseStatusNames.Complete).FirstOrDefault();
            if(complete == null) return new JsonResult(new { success = false });
            course.Status = complete;
            //Notify Instructors
            foreach (Instructors inst in course.Instructors) {
                Notifications notify = new Notifications() {
                    CourseInstance = course,
                    User = inst.User,
                    Text = "This course was approved.",
                    DateNotified = DateTime.Now,
                    Read = false
                };
                _context.Notifications.Add(notify);
            }
            _context.SaveChanges();
            return new JsonResult(new { success = true });
        }

        /// <summary>
        /// Sets the course status to in-review for the given course.
        /// </summary>
        /// <param name="courseId"></param>
        /// <returns></returns>
        public async Task<IActionResult> SetReviewCourse(int? courseId, string message) {
            if (courseId == null) {
                return new JsonResult(new { success = false });
            }
            CourseInstance course = await _context.CourseInstance.Include(c => c.Status)
                .Include(c => c.Instructors).ThenInclude(i => i.User)
                .Where(c => c.CourseInstanceId == courseId).FirstOrDefaultAsync();
            CourseStatus inRev = _context.CourseStatus.Where(s => s.Status == CourseStatusNames.InReview).FirstOrDefault();
            if (inRev == null) return new JsonResult(new { success = false });
            course.Status = inRev;
            //Notify Instructors
            foreach(Instructors inst in course.Instructors) {
                Notifications notify = new Notifications() {
                    CourseInstance = course,
                    User = inst.User,
                    Text = "The course status was set to in-review. Chair message: " + message,
                    DateNotified = DateTime.Now,
                    Read = false
                };
                _context.Notifications.Add(notify);
            }
            _context.SaveChanges();
            return new JsonResult(new { success = true });
        }

        /// <summary>
        /// Sets the course status to approved for the given course.
        /// </summary>
        /// <param name="courseId"></param>
        /// <returns></returns>
        public async Task<IActionResult> ArchiveCourse(int? courseId) {
            if (courseId == null) {
                return new JsonResult(new { success = false });
            }
            CourseInstance course = await _context.CourseInstance.Include(c => c.Status).Include(c => c.Instructors).ThenInclude(i => i.User)
                .Where(c => c.CourseInstanceId == courseId).FirstOrDefaultAsync();
            CourseStatus archive = _context.CourseStatus.Where(s => s.Status == CourseStatusNames.Archived).FirstOrDefault();
            if (archive == null) return new JsonResult(new { success = false });
            course.Status = archive;
            //Notify Instructors
            foreach (Instructors inst in course.Instructors) {
                Notifications notify = new Notifications() {
                    CourseInstance = course,
                    User = inst.User,
                    Text = "This course was archived.",
                    DateNotified = DateTime.Now,
                    Read = false
                };
                _context.Notifications.Add(notify);
            }
            _context.SaveChanges();
            return new JsonResult(new { success = true });
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
        /// Retries the file associated with the sample file. Returns NotFound if the file is null, the user has invalid permissions
        /// or the record doesn't exist.
        /// </summary>
        /// <param name="sfId"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetSampleFile(int? sfId) {
            SampleFiles sfObj = _context.SampleFiles.Where(s => s.Sid == sfId).FirstOrDefault();
            if (sfObj == null || sfObj.FileContent == null || sfObj.ContentType == null || sfObj.FileName == null) {
                return NotFound();
            }
            return File(sfObj.FileContent, sfObj.ContentType, sfObj.FileName);
        }

        /// <summary>
        /// Returns a department page, listing classes with the given department
        /// code.
        /// </summary>
        /// <param name="DeptCode"></param>
        /// <returns></returns>
        public async Task<IActionResult> Department(string DeptCode) {
            if (DeptCode == null) { DeptCode = "CS"; } //Temp for viewing
            Departments deptDB = _context.Departments.Where(d => d.Code == DeptCode)
                .Include(d => d.CourseInstance).ThenInclude(c => c.LearningOutcomes)
                .ThenInclude(l => l.EvaluationMetrics).ThenInclude(e => e.SampleFiles)
                .Include(d => d.CourseInstance).ThenInclude(c => c.Instructors).ThenInclude(i => i.User)
                .Include(d => d.CourseInstance).ThenInclude(c => c.Status)
                .FirstOrDefault();
            if(deptDB == null) {
                return NotFound();
            }
            return View("Department", deptDB);
        }
    }
}