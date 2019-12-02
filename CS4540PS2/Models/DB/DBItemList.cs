using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CS4540PS2.Models.DB
{
    public class DBItemList
    {
        public int ItemType { get; set; }
        public IQueryable<CourseInstance> Courses { get; set; }
        public IQueryable<EvaluationMetrics> EvaluationMetrics { get; set; }
        public IQueryable<Instructors> Instructors { get; set; }
        public IQueryable<LearningOutcomes> LearningOutcomes { get; set; }
    }
}