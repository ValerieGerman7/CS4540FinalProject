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
/// Date: 25 Sept 2019
/// Course: CS 4540, University of Utah
/// Copyright: CS 4540 and Valerie German - This work may not be copied for use in Academic Coursework.
/// I, Valerie German, certify that I wrote this code from scratch and did not copy it in part or whole from another source. Any references used in the completion of this assignment are cited in my README file.
/// File Contents: This file contains controller for department webpages. Chair pages for viewing departments and courses.
/// </summary>
namespace CS4540PS2.Controllers {
    [Authorize(Roles="Chair")]
    public class DepartmentController : Controller {
        private readonly LearningOutcomeDBContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;
        public DepartmentController(LearningOutcomeDBContext context, RoleManager<IdentityRole> role) {
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
                .Where(c => c.CourseInstanceId == CourseId).FirstOrDefaultAsync();
            if(course == null) {
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
            LearningOutcomes lo = _context.LearningOutcomes.Where(l => l.Loid == LearningOutcomeId).FirstOrDefault();
            if (lo == null) return Json(new { success = false });
            lo.Note = NewNote;
            lo.NoteModified = DateTime.Now;
            lo.NoteUserModifed = User.Identity.Name;
            _context.SaveChanges();
            return Json(new { success = true, noteContent = NewNote, modified = lo.NoteModified, user = User.Identity.Name });
        }

        /// <summary>
        /// Returns a department page, listing classes with the given department
        /// code.
        /// </summary>
        /// <param name="DeptCode"></param>
        /// <returns></returns>
        public async Task<IActionResult> Department(string DeptCode) {
            if(DeptCode == null) { DeptCode = "CS"; } //Temp for viewing
            return View("Department", GetDeptData(DeptCode));
        }

        /// <summary>
        /// Returns information about a department's courses.
        /// </summary>
        /// <param name="DeptCode"></param>
        /// <returns></returns>
        public DepartmentData GetDeptData(string DeptCode) {
            using (_context) {
                var getDept = from courses in _context.CourseInstance
                              where courses.Department == DeptCode
                              select new CourseStatData {
                                  CourseId = courses.CourseInstanceId,
                                  CourseName = courses.Name,
                                  CourseNum = courses.Number,
                                  CourseDescript = courses.Description,
                                  Semester = courses.Semester,
                                  Year = courses.Year,
                                  NumLearningOutcomes = (from lo in _context.LearningOutcomes
                                                         where lo.CourseInstanceId == courses.CourseInstanceId
                                                         select lo.Name).Count(),
                                  NumLOWithEvaluationMetrics = (from lo in _context.LearningOutcomes
                                                                where lo.CourseInstanceId == courses.CourseInstanceId
                                                                && lo.EvaluationMetrics.Count > 0
                                                                select lo.Name).Count(),
                                  NumEvaluationMetrics = (from lo in _context.LearningOutcomes
                                                          join em in _context.EvaluationMetrics on lo.Loid equals em.Loid
                                                          where lo.CourseInstanceId == courses.CourseInstanceId
                                                          select em.Name).Count(),
                                  NumEMWithSamples = (from lo in _context.LearningOutcomes
                                                      join em in _context.EvaluationMetrics on lo.Loid equals em.Loid
                                                      where lo.CourseInstanceId == courses.CourseInstanceId
                                                      && em.SampleFiles.Count > 0
                                                      select em.Name).Count(),
                              };
                List<CourseStatData> x = getDept.ToList<CourseStatData>();
                DepartmentData d = new DepartmentData {
                    DeptName = (DeptCode.Equals("CS") ? "Computer Science" : DeptCode),
                    DeptCode = DeptCode,
                    Courses = x
                };
                return d;
            }
        }

    }

    /// <summary>
    /// Struct containing information about a department.
    /// </summary>
    public struct DepartmentData {
        public string DeptName { get; set; }
        public string DeptCode { get; set; }
        public List<CourseStatData> Courses { get; set; }
    }
    /// <summary>
    /// Struct containing information about a course.
    /// </summary>
    public struct CourseStatData {
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public int CourseNum { get; set; }
        public string Semester { get; set; }
        public int Year { get; set; }
        public string CourseDescript { get; set; }
        public int NumLearningOutcomes { get; set; }
        public int NumLOWithEvaluationMetrics { get; set; }
        public int NumEvaluationMetrics { get; set; }
        public int NumEMWithSamples { get; set; }
    }
}