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
using System.Diagnostics;
using System.Threading.Tasks;
using System.Threading;

/// <summary>
/// Author: Valerie German
/// Date: 4 Dec 2019
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
        private readonly UserContext _userContext;
        private UserManager<IdentityUser> _userManager;
        /// <summary>
        /// Construct a course controller with a database context.
        /// </summary>
        /// <param name="context"></param>
        public InstructorController(LOTDBContext context, RoleManager<IdentityRole> role, UserContext users, UserManager<IdentityUser> manager) {
            _context = context;
            _roleManager = role;
            _userContext = users;
            _userManager = manager;
        }


        /// <summary>
        /// Return the index page listing all course instances belonging to the current professor.
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index(string status) {
            if(status == null) {
                //All non-archived classes this user is an instructor of.
                var instances = _context.CourseInstance
                    .Include(c => c.Status)
                    .Include(c => c.Instructors).ThenInclude(i => i.User)
                    .Where(c => c.Instructors.Where(ins => ins.User.UserLoginEmail == User.Identity.Name).Any()
                            && c.Status.Status != CourseStatusNames.Archived);
                return View(new ValueTuple<string, IEnumerable<CourseInstance>>(null, await instances.ToListAsync()));
            } else {
                //All classes of the selected status this user is an instructor of.
                var instances = _context.CourseInstance
                    .Include(c => c.Status)
                    .Include(c => c.Instructors).ThenInclude(i => i.User)
                    .Where(c => c.Instructors.Where(ins => ins.User.UserLoginEmail == User.Identity.Name).Any()
                            && c.Status.Status == status);
                return View(new ValueTuple<string, IEnumerable<CourseInstance>>(status, await instances.ToListAsync()));
            }
        }

        /// <summary>
        /// View for all courses that have been archived.
        /// </summary>
        public async Task<IActionResult> ArchivedCourses(string sortOrder, string currentFilter, string searchString, int? pageNumber, int resultsPerPage = 5) {
            ViewData["PageNumber"] = pageNumber;
            ViewData["ResultsPerPage"] = resultsPerPage;
            // Set up the possible ordering schemes of the table.
            ViewData["CurrentSort"] = sortOrder;
            ViewData["CourseNumSortParam"] = String.IsNullOrEmpty(sortOrder) ? "course_num_desc" : "";
            ViewData["CourseTitleSortParam"] = sortOrder == "title_asc" ? "title_desc" : "title_asc";
            ViewData["DepartmentSortParam"] = sortOrder == "dept_asc" ? "dept_desc" : "dept_asc";
            ViewData["SemesterSortParam"] = sortOrder == "semester_asc" ? "semester_desc" : "semester_asc";
            ViewData["YearSortParam"] = sortOrder == "year_asc" ? "year_desc" : "year_asc";

            // If there is a search string, filter by that, otherwise use the default
            if (searchString != null) {
                pageNumber = 1;
            }
            else {
                searchString = currentFilter;
            }
            ViewData["CurrentFilter"] = searchString;

            // get course instances
            var instances = _context.CourseInstance.Include(c => c.Status)
                .Include(c => c.Instructors).ThenInclude(i => i.User)
                .Where(c => c.Status.Status == CourseStatusNames.Archived);

            /* TODO: test if this is necessary*/
            if (instances.Count() == 0)
                return View();

            // allow the user to search the course catalogue
            instances = FilterBySearch(searchString, instances);

            // reorder the results based on the selected filter
            instances = OrderBySelection(sortOrder, instances);

            return View(await PaginatedList<CourseInstance>.CreateAsync(instances.AsNoTracking(), pageNumber ?? 1, resultsPerPage));
        }

        /// <summary>
        /// Filters the courses returned by the database by a user supplied search string.
        /// </summary>
        private IQueryable<CourseInstance> FilterBySearch(string searchString, IQueryable<CourseInstance> courseInstances) {
            // allow the user to search the course catalogue
            if (!string.IsNullOrEmpty(searchString)) {
                string[] searchWords = searchString.Split(' ');
                foreach (string s in searchWords) {
                    if (!string.IsNullOrEmpty(s)) {
                        courseInstances = courseInstances.Where(c => c.Name.Contains(s)
                            || c.Department.Contains(s)
                            || c.Number.ToString().Contains(s)
                            || c.Semester.Contains(s)
                            || c.Year.ToString().Contains(s));
                    }
                }
            }
            return courseInstances;
        }

        /// <summary>
        /// Reorders the courses in the view based on user selection
        /// </summary>
        private IQueryable<CourseInstance> OrderBySelection(string sortOrder, IQueryable<CourseInstance> courseInstances) {
            // reorder the results based on the selected filter           
            switch (sortOrder) {
                case "course_num_desc":
                    courseInstances = courseInstances.OrderByDescending(c => c.Number)
                        .ThenBy(c => c.Year);
                    break;
                case "title_asc":
                    courseInstances = courseInstances.OrderBy(c => c.Name)
                        .ThenBy(c => c.Year);
                    break;
                case "title_desc":
                    courseInstances = courseInstances.OrderByDescending(c => c.Name)
                        .ThenBy(c => c.Year);
                    break;
                case "dept_asc":
                    courseInstances = courseInstances.OrderBy(c => c.Department)
                        .ThenBy(c => c.Year);
                    break;
                case "dept_desc":
                    courseInstances = courseInstances.OrderByDescending(c => c.Department)
                        .ThenBy(c => c.Year);
                    break;
                case "semester_asc":
                    courseInstances = courseInstances.OrderBy(c => c.Semester)
                        .ThenBy(c => c.Year);
                    break;
                case "semester_desc":
                    courseInstances = courseInstances.OrderByDescending(c => c.Semester)
                    .ThenBy(c => c.Year);
                    break;
                case "year_asc":
                    courseInstances = courseInstances.OrderBy(c => c.Year);
                    break;
                case "year_desc":
                    courseInstances = courseInstances.OrderByDescending(c => c.Year);
                    break;
                default:
                    courseInstances = courseInstances.OrderBy(c => c.Number)
                        .ThenBy(c => c.Year);
                    break;
            }
            return courseInstances;
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
            //Finds the associated learning outcome. The user must be an instructor and the course cannot be archived.
            LearningOutcomes lo = _context.LearningOutcomes.Where(l => l.Loid == LearningOutcomeId).Include(l => l.LONotes)
                .Where(l => l.CourseInstance.Instructors.Where(ins => ins.User.UserLoginEmail == User.Identity.Name).Any()
                        && l.CourseInstance.Status.Status != CourseStatusNames.Archived)
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
            if (Dept == null || Num == null || Sem.Equals(null) || Year == null)
                return View("Error", new ErrorViewModel() {
                    ErrorMessage = "Insufficient information to locate course."
                });
            //Retrieves the associated course, the user must be an instructor of the course, or the course must be archived.
            CourseInstance course = _context.CourseInstance.Where(c => c.Department == Dept && c.Number == Num
                && c.Semester == Sem && c.Year == Year)
                .Include(c => c.CourseNotes)
                .Include(c => c.Instructors).ThenInclude(i => i.User)
                .Include(c => c.LearningOutcomes).ThenInclude(lo => lo.EvaluationMetrics).ThenInclude(em => em.SampleFiles)
                .Include(c => c.LearningOutcomes).ThenInclude(lo => lo.LONotes)
                .Include(c => c.Status)
                .Where(c => c.Instructors.Where(i => i.User.UserLoginEmail == User.Identity.Name).Any() || c.Status.Status == CourseStatusNames.Archived)
                .FirstOrDefault();
            if (course == null)
                return Forbid();
            return View("Course", course);
        }

        /// <summary>
        /// Instructor changes the course's status to 'Awaiting Approval'
        /// </summary>
        /// <param name="courseId"></param>
        /// <returns></returns>
        public async Task<IActionResult> RequestApproval(int? courseId) {
            if (courseId == null) return new JsonResult(new { success = false });
            CourseInstance course = _context.CourseInstance.Include(c => c.Status)
                .Where(c => c.CourseInstanceId == courseId).FirstOrDefault();
            if(course == null) return new JsonResult(new { success = false });
            if(course.Status.Status.Equals(CourseStatusNames.InProgress) || course.Status.Status.Equals(CourseStatusNames.InReview)) {
                //Change status
                CourseStatus app = _context.CourseStatus.Where(s => s.Status == CourseStatusNames.AwaitingApproval).FirstOrDefault();
                if(app == null) return new JsonResult(new { success = false });
                course.Status = app;
                //Notify Chairs
                foreach(IdentityUser user in _userManager.GetUsersInRoleAsync("Chair").Result) {
                    UserLocator userLoc = _context.UserLocator.Where(u => u.UserLoginEmail == user.Email).FirstOrDefault();
                    if(userLoc != null) {
                        Notifications notify = new Notifications() {
                            CourseInstance = course,
                            Text = "Course approval requested.",
                            DateNotified = DateTime.Now,
                            User = userLoc
                        };
                        _context.Notifications.Add(notify);
                    }
                }
                _context.SaveChanges();
                return new JsonResult(new { success = true });
            } else {
                return new JsonResult(new { success = false });
            }
        }

        
    }
}