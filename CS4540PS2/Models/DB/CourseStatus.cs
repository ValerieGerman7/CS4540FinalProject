using System;
using System.Collections.Generic;

namespace CS4540PS2.Models
{
    public partial class CourseStatus
    {
        public CourseStatus()
        {
            CourseInstance = new HashSet<CourseInstance>();
        }

        public int StatusId { get; set; }
        public string Status { get; set; }

        public virtual ICollection<CourseInstance> CourseInstance { get; set; }
    }
}
