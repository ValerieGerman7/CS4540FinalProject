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
/// File Contents: This file contains controller for admin course webpages - creating/editing/deleting instances and course overview.
/// </summary>
namespace CS4540PS2.Controllers {
    [Authorize(Roles="Admin")]
    public class CourseController : Controller {
        private readonly LOTDBContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserContext _userContext;
        private UserManager<IdentityUser> _userManager;
        /// <summary>
        /// Construct a course controller with a database context.
        /// </summary>
        /// <param name="context"></param>
        public CourseController(LOTDBContext context, RoleManager<IdentityRole> role, UserContext user, UserManager<IdentityUser> manage) {
            _context = context;
            _roleManager = role;
            _userContext = user;
            _userManager = manage;
        }

        /// <summary>
        /// Return the index page listing all course instances.
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? pageNumber) {
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
            } else {
                searchString = currentFilter;
            }            
            ViewData["CurrentFilter"] = searchString;

            // get course instances
            var instances = from c in _context.CourseInstance
                            select c;

            /* TODO: test if this is necessary*/
            if (instances.Count() == 0)
                return View();

            // allow the user to search the course catalogue
            instances = FilterBySearch(searchString, instances);

            // reorder the results based on the selected filter
            instances = OrderBySelection(sortOrder, instances);

            //return View(await instances.ToListAsync());
            int pageSize = 5;
            return View(await PaginatedList<CourseInstance>.CreateAsync(instances.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        /// <summary>
        /// Filters the courses returned by the database by a user supplied search string.
        /// </summary>
        private IQueryable<CourseInstance> FilterBySearch(string searchString, IQueryable<CourseInstance> courseInstances) {
            // allow the user to search the course catalogue
            if (!string.IsNullOrEmpty(searchString)) {
                string[] searchWords = searchString.Split(' ');
                foreach (string s in searchWords)
                {
                    if (!string.IsNullOrEmpty(s))
                    {
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
            switch (sortOrder)
            {
                case "course_num_desc":
                    courseInstances = courseInstances.OrderByDescending(c => c.Number);
                    break;
                case "title_asc":
                    courseInstances = courseInstances.OrderBy(c => c.Name);
                    break;
                case "title_desc":
                    courseInstances = courseInstances.OrderByDescending(c => c.Name);
                    break;
                case "dept_asc":
                    courseInstances = courseInstances.OrderBy(c => c.Department);
                    break;
                case "dept_desc":
                    courseInstances = courseInstances.OrderByDescending(c => c.Department);
                    break;
                case "semester_asc":
                    courseInstances = courseInstances.OrderBy(c => c.Semester);
                    break;
                case "semester_desc":
                    courseInstances = courseInstances.OrderByDescending(c => c.Semester);
                    break;
                case "year_asc":
                    courseInstances = courseInstances.OrderBy(c => c.Year);
                    break;
                case "year_desc":
                    courseInstances = courseInstances.OrderByDescending(c => c.Year);
                    break;
                default:
                    courseInstances = courseInstances.OrderBy(c => c.Number);
                    break;
            }
            return courseInstances;
        }

        /// <summary>
        /// GET for course create page.
        /// </summary>
        /// <returns></returns>
        public IActionResult Create() {
            ViewData["CourseInstanceId"] = new SelectList(_context.CourseInstance, "CourseInstanceId", "Department");
            ViewData["Departments"] = _context.Departments;
            ViewData["Statuses"] = _context.CourseStatus;
            return View(new CourseEditData() {
                Departments = _context.Departments,
                CourseStatus = _context.CourseStatus,
                Professors = GetInstructors()
            });
        }

        /// <summary>
        /// Get a list of all instructor users.
        /// </summary>
        /// <returns></returns>
        public List<UserLocator> GetInstructors() {
            List<IdentityUser> instructors = new List<IdentityUser>();
            foreach(IdentityUser user in _userContext.Users) {
                if(_userManager.IsInRoleAsync(user, "Instructor").Result) {
                    instructors.Add(user);
                }
            }
            return _context.UserLocator.Where(u => instructors.Where(i => i.Email == u.UserLoginEmail).Any()).ToList();

        }

        /// <summary>
        /// POST for creating a new course.
        /// Courses may be assigned an instructor.
        /// </summary>
        /// <param name="courseInstance"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Description,Department,Number,Semester,Year,StatusId,DueDate")] CourseInstance courseInstance, string instructor) {
            if (ModelState.IsValid) {
                _context.Add(courseInstance);
                if (instructor != null) {
                    UserLocator instr = _context.UserLocator.Where(u => u.UserLoginEmail == instructor).FirstOrDefault();
                    if (instr != null) {
                        _context.Instructors.Add(new Instructors() { CourseInstance = courseInstance, User = instr });
                    }
                }
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
            //ViewData["CourseInstanceId"] = new SelectList(_context.CourseInstance, "CourseInstanceId", "Department", courseInstance.CourseInstanceId);
            //return View(courseInstance);
            return View(new CourseEditData() {
                Course = _context.CourseInstance.Where(c => c.CourseInstanceId == id).Include(c => c.Instructors).ThenInclude(i => i.User).FirstOrDefault(),
                Departments = _context.Departments,
                CourseStatus = _context.CourseStatus,
                Professors = GetInstructors()
            });
        }

        /// <summary>
        /// POST for editing a course.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="courseInstance"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CourseInstanceId,Name,Description,Department,Number,Semester,Year,StatusId,DueDate")] CourseInstance courseInstance, string newInstructor) {
            if (id != courseInstance.CourseInstanceId) {
                return NotFound();
            }
            if (ModelState.IsValid) {
                try {
                    if (newInstructor != null) {
                        UserLocator instructorUserLoc = _context.UserLocator.Where(u => u.UserLoginEmail == newInstructor).FirstOrDefault();
                        if (instructorUserLoc != null) {
                            //Current instructor (TODO: change to multiple)
                            Instructors currentCourseInstructor = _context.Instructors.Where(i => i.CourseInstanceId == courseInstance.CourseInstanceId).FirstOrDefault();
                            if(currentCourseInstructor == null) { //No current instructor
                                Instructors newInst = new Instructors() {
                                    CourseInstanceId = courseInstance.CourseInstanceId,
                                    UserId = instructorUserLoc.Id
                                };
                                _context.Instructors.Add(newInst);
                            } else { //Change current
                                currentCourseInstructor.User = instructorUserLoc;
                                _context.Update(currentCourseInstructor);
                            }
                        }
                    }
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
            //ViewData["CourseInstanceId"] = new SelectList(_context.CourseInstance, "CourseInstanceId", "Department", courseInstance.CourseInstanceId);
            //return View(courseInstance);
            return View(new CourseEditData() {
                Course = _context.CourseInstance.Where(c => c.CourseInstanceId == id).Include(c => c.Instructors).ThenInclude(i => i.User).FirstOrDefault(),
                Departments = _context.Departments,
                CourseStatus = _context.CourseStatus,
                Professors = GetInstructors()
            });
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

    public class CourseEditData {
        public CourseInstance Course { get; set; } = null;
        public DbSet<Departments> Departments { get; set; }
        public DbSet<CourseStatus> CourseStatus { get; set; }
        public List<UserLocator> Professors { get; set; }
    }
}