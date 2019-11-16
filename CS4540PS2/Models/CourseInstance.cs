using System;
using System.Collections.Generic;

namespace CS4540PS2.Models
{
    public partial class CourseInstance
    {
        public CourseInstance()
        {
            CourseNotes = new HashSet<CourseNotes>();
            Instructors = new HashSet<Instructors>();
            LearningOutcomes = new HashSet<LearningOutcomes>();
        }

        public int CourseInstanceId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Department { get; set; }
        public int Number { get; set; }
        public string Semester { get; set; }
        public int Year { get; set; }
        public int? StatusId { get; set; }
        public DateTime DueDate { get; set; }

        public virtual Departments DepartmentNavigation { get; set; }
        public virtual CourseStatus Status { get; set; }
        public virtual ICollection<CourseNotes> CourseNotes { get; set; }
        public virtual ICollection<Instructors> Instructors { get; set; }
        public virtual ICollection<LearningOutcomes> LearningOutcomes { get; set; }
    }
}
