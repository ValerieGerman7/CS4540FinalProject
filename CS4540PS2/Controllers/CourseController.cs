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
            return View("Course", GetCourseInfo(Dept, Num, Sem, Year, _context));
        }

        public static CourseInfo GetCourseInfo(string Dept, int Num, string Sem, int Year, LearningOutcomeDBContext context) {
            using (context) {
                var getCourse = from courses in context.CourseInstance
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
                                    ID = courses.CourseInstanceId,
                                    LearningOutcomes = courses.LearningOutcomes.Select(lo =>
                                        new LearningOutcomeData {
                                            LOName = lo.Name,
                                            LODescription = lo.Description,
                                            LOID = lo.Loid,
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
    public struct LearningOutcomeData {
        public string LOName { get; set; }
        public string LODescription { get; set; }
        public int LOID { get; set; }
        public List<EvaluationMetricData> EvaluationMetrics { get; set; }
    }
    public struct EvaluationMetricData {
        public string Name { get; set; }
        public string Description { get; set; }
        public int EMID { get; set; }
        public List<SamplesData> Samples { get; set; }
    }
    public struct SamplesData {
        public string FileName { get; set; }
        public int Score { get; set; }
        public int SID { get; set; }
    }

}