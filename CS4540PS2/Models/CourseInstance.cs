using System;
using System.Collections.Generic;

namespace CS4540PS2.Models
{
    public partial class CourseInstance
    {
        public CourseInstance()
        {
            LearningOutcomes = new HashSet<LearningOutcomes>();
        }

        public int CourseInstanceId { get; set; }
        public string Name { get; set; }
        public string Descripton { get; set; }
        public string Department { get; set; }
        public int Number { get; set; }
        public string Semester { get; set; }
        public int Year { get; set; }

        public virtual ICollection<LearningOutcomes> LearningOutcomes { get; set; }
    }
}
