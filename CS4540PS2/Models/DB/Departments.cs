using System;
using System.Collections.Generic;

namespace CS4540PS2.Models
{
    public partial class Departments
    {
        public Departments()
        {
            CourseInstance = new HashSet<CourseInstance>();
        }

        public string Name { get; set; }
        public string Code { get; set; }

        public virtual ICollection<CourseInstance> CourseInstance { get; set; }
    }
}
