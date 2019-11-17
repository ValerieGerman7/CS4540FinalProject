using System;
using System.Collections.Generic;

namespace CS4540PS2.Models
{
    public partial class Instructors
    {
        public int Ikey { get; set; }
        public int CourseInstanceId { get; set; }
        public int UserId { get; set; }

        public virtual CourseInstance CourseInstance { get; set; }
        public virtual UserLocator User { get; set; }
    }
}
