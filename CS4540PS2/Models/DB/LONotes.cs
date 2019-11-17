using System;
using System.Collections.Generic;

namespace CS4540PS2.Models
{
    public partial class LONotes
    {
        public int NoteId { get; set; }
        public string Note { get; set; }
        public DateTime? NoteModified { get; set; }
        public string NoteUserModified { get; set; }
        public int Loid { get; set; }

        public virtual LearningOutcomes Lo { get; set; }
    }
}
