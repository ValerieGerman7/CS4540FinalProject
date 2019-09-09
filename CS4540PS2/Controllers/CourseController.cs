using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CS4540PS2.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CS4540PS2.Controllers {
    public class CourseController : Controller {
        private readonly LearningOutcomeDBContext _context;
        public CourseController(LearningOutcomeDBContext context) {
            _context = context;
        }

        public async Task<IActionResult> Index() {
            return View();
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


    }

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
}