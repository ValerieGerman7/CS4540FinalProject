using System;
using System.Collections.Generic;

namespace CS4540PS2.Models
{
    public partial class LearningOutcomes
    {
        public LearningOutcomes()
        {
            EvaluationMetrics = new HashSet<EvaluationMetrics>();
        }

        public int Loid { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CourseInstanceId { get; set; }

        public virtual CourseInstance CourseInstance { get; set; }
        public virtual ICollection<EvaluationMetrics> EvaluationMetrics { get; set; }
    }
}
