using CS4540PS2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CS4540PS2.Controllers {
    public class CourseController : Controller {
        private readonly LearningOutcomeDBContext _context;
        public CourseController(LearningOutcomeDBContext context) {
            _context = context;
        }

        public async Task<IActionResult> Index() {
            var instances = _context.CourseInstance;
            return View(await instances.ToListAsync());
        }
        
        public async Task<IActionResult> Course(string Dept, int Num, string Sem, int Year) {
            //JObject courseInfo = GetCourseInfo(Dept, Num, Sem, Year);
            return View("Course", GetCourseInfo(Dept, Num, Sem, Year));
        }

        public CourseInfo GetCourseInfo(string Dept, int Num, string Sem, int Year) {
            using (_context) {
                var getCourse = from courses in _context.CourseInstance
                                where courses.Department == Dept
                                && courses.Number == Num
                                && courses.Semester == Sem
                                && courses.Year == Year
                                select new CourseInfo {
                                    CourseName = courses.Name,
                                    CourseDescription = courses.Description,
                                    Department = courses.Department,
                                    Number = courses.Number,
                                    Semester = courses.Semester,
                                    Year = courses.Year,
                                    LearningOutcomes = courses.LearningOutcomes.Select(lo =>
                                        new LearningOutcomeData {
                                            LOName = lo.Name,
                                            LODescription = lo.Description,
                                            EvaluationMetrics = lo.EvaluationMetrics.Where(em => em.Loid == lo.Loid)
                                            .Select(x => new EvaluationMetricData {
                                                Name = x.Name,
                                                Description = x.Description,
                                                Samples = x.SampleFiles.Where(sample => sample.Emid == x.Emid)
                                                        .Select(sampleSelect => new SamplesData {
                                                            Score = sampleSelect.Score,
                                                            FileName = sampleSelect.FileName }).ToList<SamplesData>()
                                            }).ToList<EvaluationMetricData>()
                                        }
                                    ).ToList<LearningOutcomeData>()
                                };
                //JsonResult res = Json(getCourse.FirstOrDefault());
                //return JObject.Parse(JsonConvert.SerializeObject(res));
                return getCourse.FirstOrDefault();
            }
        }

        // GET: LearningOutcomes/Create
        public IActionResult Create() {
            ViewData["CourseInstanceId"] = new SelectList(_context.CourseInstance, "CourseInstanceId", "Department");
            return View();
        }

        // POST: LearningOutcomes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
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

        // GET: LearningOutcomes/Edit/5
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

        // POST: LearningOutcomes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
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

        // GET: LearningOutcomes/Delete/5
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

        // POST: LearningOutcomes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id) {
            var courseInstance = await _context.CourseInstance.FindAsync(id);
            _context.CourseInstance.Remove(courseInstance);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CourseInstanceExists(int id) {
            return _context.CourseInstance.Any(e => e.CourseInstanceId == id);
        }



        //TODO: Move to own controller

        public async Task<IActionResult> Department(string DeptCode) {
            return View("Department", GetDeptData(DeptCode));
        }

        public DepartmentData GetDeptData(string DeptCode) {
            using (_context) {
                var getDept = from courses in _context.CourseInstance
                              select new CourseStatData {
                                  CourseName = courses.Name,
                                  CourseNum = courses.Number,
                                  CourseDescript = courses.Description,
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
                    DeptName = "Computer Science",
                    DeptCode = "CS",
                    Courses = x
                };
                return d;
            }
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
        public List<LearningOutcomeData> LearningOutcomes { get; set; }
    }
    public struct LearningOutcomeData {
        public string LOName { get; set; }
        public string LODescription { get; set; }
        public List<EvaluationMetricData> EvaluationMetrics { get; set; }
    }
    public struct EvaluationMetricData {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<SamplesData> Samples { get; set; }
    }
    public struct SamplesData {
        public string FileName { get; set; }
        public int Score { get; set; }
    }

    public struct DepartmentData {
        public string DeptName { get; set; }
        public string DeptCode { get; set; }
        public List<CourseStatData> Courses { get; set; }
    }

    public struct CourseStatData {
        public string CourseName { get; set; }
        public int CourseNum { get; set; }
        public string CourseDescript { get; set; }
        public int NumLearningOutcomes { get; set; }
        public int NumLOWithEvaluationMetrics { get; set; }
        public int NumEvaluationMetrics { get; set; }
        public int NumEMWithSamples { get; set; }
    }
}