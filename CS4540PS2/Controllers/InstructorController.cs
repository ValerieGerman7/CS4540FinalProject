using CS4540PS2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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
    public class InstructorController : Controller {
        private readonly LearningOutcomeDBContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;
        /// <summary>
        /// Construct a course controller with a database context.
        /// </summary>
        /// <param name="context"></param>
        public InstructorController(LearningOutcomeDBContext context, RoleManager<IdentityRole> role) {
            _context = context;
            _roleManager = role;
        }

        /// <summary>
        /// Return the index page listing all course instances belonging to the current professor.
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index() {
            var instances = _context.CourseInstance.Where(i => 
                i.Instructors.Where(ins => ins.InstructorLoginEmail == User.Identity.Name).Any());
            return View(await instances.ToListAsync());
        }

        /// <summary>
        /// Updates the course's note and the date modified.
        /// </summary>
        /// <param name="CourseInstanceId"></param>
        /// <param name="NewNote"></param>
        /// <returns></returns>
        public JsonResult ChangeNote(int CourseInstanceId, string NewNote) {
            CourseInstance course = _context.CourseInstance
                .Where(c => c.CourseInstanceId == CourseInstanceId && c.Instructors.Where(ins => ins.InstructorLoginEmail == User.Identity.Name).Any())
                .FirstOrDefault();
            if (course == null) return Json(new { success = false });
            course.Note = NewNote;
            course.NoteModified = DateTime.Now;
            _context.SaveChanges();
            return Json(new { success = true, noteContent = NewNote, modified = course.NoteModified });
        }

        /// <summary>
        /// Updates the identified learning outcome's note along with the last modified date and last user
        /// modifying date.
        /// </summary>
        /// <param name="LearningOutcomeId"></param>
        /// <param name="NewNote"></param>
        /// <returns></returns>
        public JsonResult ChangeLONote(int LearningOutcomeId, string NewNote) {
            LearningOutcomes lo = _context.LearningOutcomes.Where(l => l.Loid == LearningOutcomeId)
                .Where(l => l.CourseInstance.Instructors.Where(ins => ins.InstructorLoginEmail == User.Identity.Name).Any())
                .FirstOrDefault();
            if (lo == null) return Json(new { success = false });
            lo.Note = NewNote;
            lo.NoteModified = DateTime.Now;
            lo.NoteUserModifed = User.Identity.Name;
            _context.SaveChanges();
            return Json(new { success = true, noteContent = NewNote, modified = lo.NoteModified, user = User.Identity.Name });
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
            CourseInfo info = GetCourseInfo(Dept, (int)Num, Sem, (int)Year, _context, User.Identity.Name);
            if (info.CourseName == null)
                return Forbid();
            return View("Course", info);
        }

        /// <summary>
        /// Returns an object containing relevant information about the given course.
        /// </summary>
        /// <param name="Dept"></param>
        /// <param name="Num"></param>
        /// <param name="Sem"></param>
        /// <param name="Year"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public static CourseInfo GetCourseInfo(string Dept, int Num, string Sem, int Year, LearningOutcomeDBContext context, string userEmail) {
            using (context) {
                var getCourse = from courses in context.CourseInstance
                                where courses.Department == Dept
                                && courses.Number == Num
                                && courses.Semester == Sem
                                && courses.Year == Year
                                && courses.Instructors.Where(i => i.InstructorLoginEmail == userEmail).Any()
                                select new CourseInfo {
                                    CourseName = courses.Name,
                                    CourseDescription = courses.Description,
                                    Department = courses.Department,
                                    Number = courses.Number,
                                    Semester = courses.Semester,
                                    Year = courses.Year,
                                    ID = courses.CourseInstanceId,
                                    Note = courses.Note,
                                    NoteModified = courses.NoteModified,
                                    LearningOutcomes = courses.LearningOutcomes.Select(lo =>
                                        new LearningOutcomeData {
                                            LOName = lo.Name,
                                            LODescription = lo.Description,
                                            LOID = lo.Loid,
                                            Note = lo.Note,
                                            NoteModified = lo.NoteModified,
                                            NoteUserModified = lo.NoteUserModifed,
                                            EvaluationMetrics = lo.EvaluationMetrics.Where(em => em.Loid == lo.Loid)
                                            .Select(x => new EvaluationMetricData {
                                                Name = x.Name,
                                                Description = x.Description,
                                                EMID = x.Emid,
                                                Samples = x.SampleFiles.Where(sample => sample.Emid == x.Emid)
                                                        .Select(sampleSelect => new SamplesData {
                                                            Score = sampleSelect.Score,
                                                            FileName = sampleSelect.FileName,
                                                            SID = sampleSelect.Sid
                                                        }).ToList<SamplesData>()
                                            }).ToList<EvaluationMetricData>()
                                        }
                                    ).ToList<LearningOutcomeData>()
                                };
                return getCourse.FirstOrDefault();
            }
        }
    }
}