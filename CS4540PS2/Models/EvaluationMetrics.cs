using System;
using System.Collections.Generic;

namespace CS4540PS2.Models
{
    public partial class EvaluationMetrics
    {
        public EvaluationMetrics()
        {
            SampleFiles = new HashSet<SampleFiles>();
        }

        public int Emid { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Loid { get; set; }

        public virtual LearningOutcomes Lo { get; set; }
        public virtual ICollection<SampleFiles> SampleFiles { get; set; }
    }
}
