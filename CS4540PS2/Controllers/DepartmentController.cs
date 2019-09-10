using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CS4540PS2.Models;
using Microsoft.AspNetCore.Mvc;

namespace CS4540PS2.Controllers {
    public class DepartmentController : Controller {
        private readonly LearningOutcomeDBContext _context;
        public DepartmentController(LearningOutcomeDBContext context) {
            _context = context;
        }

        public IActionResult Index() {
            using (_context) {
                var getDepts = from courses in _context.CourseInstance
                               group courses by courses.Department into deptGroup
                               orderby deptGroup.Key
                               select deptGroup.Key;
                return View("Index", getDepts.ToList<string>());
            }
        }
        //TODO: Move to own controller

        public async Task<IActionResult> Department(string DeptCode) {
            return View("Department", GetDeptData(DeptCode));
        }

        public DepartmentData GetDeptData(string DeptCode) {
            using (_context) {
                var getDept = from courses in _context.CourseInstance
                              where courses.Department == DeptCode
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
                    DeptName = (DeptCode.Equals("CS") ? "Computer Science" : DeptCode),
                    DeptCode = DeptCode,
                    Courses = x
                };
                return d;
            }
        }

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