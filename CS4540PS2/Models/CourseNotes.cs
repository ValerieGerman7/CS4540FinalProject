using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

/// <summary>
/// Author: Valerie German
/// Date: 18 Oct 2019
/// Course: CS 4540, University of Utah
/// Copyright: CS 4540 and Valerie German - This work may not be copied for use in Academic Coursework.
/// I, Valerie German, certify that I wrote this code from scratch and did not copy it in part or whole from another source. Any references used in the completion of this assignment are cited in my README file.
/// File Contents: This file contains the model for course notes.
/// </summary>
namespace CS4540PS2.Models {
    public partial class CourseNotes {
        public CourseNotes() {
        }

        [Key]
        public int NoteId { get; set; }
        public string Note { get; set; }
        public DateTime? NoteModified { get; set; }

        public virtual CourseInstance CourseInstance { get; set; }
    }
}
