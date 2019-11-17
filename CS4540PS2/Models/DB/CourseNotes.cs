using System;
using System.Collections.Generic;

namespace CS4540PS2.Models
{
    public partial class CourseNotes
    {
        public int NoteId { get; set; }
        public string Note { get; set; }
        public DateTime? NoteModified { get; set; }
        public int CourseInstanceId { get; set; }

        public virtual CourseInstance CourseInstance { get; set; }
    }
}
