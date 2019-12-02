using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CS4540PS2.Models
{
    public partial class Notifications
    {
        [Key]
        public int NotificationId { get; set; }
        public int UserId { get; set; }
        public string Text { get; set; }
        public DateTime DateNotified { get; set; }
        public bool Read { get; set; }


        public virtual CourseInstance CourseInstance { get; set; }
        public virtual UserLocator User { get; set; }
    }
}
