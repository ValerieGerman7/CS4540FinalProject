using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CS4540PS2.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CS4540PS2.Controllers {
    public class CourseController : Controller {
        private readonly LearningOutcomeDBContext _context;
        public CourseController(LearningOutcomeDBContext context) {
            _context = context;
        }

        public async Task<IActionResult> Index() {
            return View();
        }

        public async Task<IActionResult> Course() {
            using (_context) {
                var getCourse = from courses in _context.CourseInstance
                                select courses;
                var getLOs = from courses in _context.CourseInstance
                             join lots in _context.LearningOutcomes
                             on courses.CourseInstanceId equals lots.CourseInstanceId
                             select new {
                                 CourseName = courses.Name,
                                 LearningOutcome = lots.Name,
                                    EvaluationMetrics = _context.EvaluationMetrics.Where(x => x.Loid == lots.Loid)
                                    .Select(x => new {
                                        x.Name,
                                        x.Description,
                                        Samples=_context.SampleFiles.Where(sample => sample.Emid == x.Emid)
                                        .Select(sampleSelect => new { sampleSelect.Score, sampleSelect.FileName })
                                    })
                                };
                JsonResult res = Json(getLOs.ToArray());
                string st = JsonConvert.SerializeObject(res.Value);
                return View("Course", st);
            }
        }



    }
}