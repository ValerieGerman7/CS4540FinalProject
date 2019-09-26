using System;
using System.Collections.Generic;

/// <summary>
/// Author: Valerie German
/// Date: 25 Sept 2019
/// Course: CS 4540, University of Utah
/// Copyright: CS 4540 and Valerie German - This work may not be copied for use in Academic Coursework.
/// I, Valerie German, certify that I wrote this code from scratch and did not copy it in part or whole from another source. Any references used in the completion of this assignment are cited in my README file.
/// File Contents: This file contains the model for the CourseInstance table in the database. This represents a single instance of a course.
/// </summary>
namespace CS4540PS2.Models {
    public partial class CourseInstance {
        public CourseInstance() {
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

        public virtual ICollection<Instructors> Instructors { get; set; }
        public virtual ICollection<LearningOutcomes> LearningOutcomes { get; set; }
    }
}
