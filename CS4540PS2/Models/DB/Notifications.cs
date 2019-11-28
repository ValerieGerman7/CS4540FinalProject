using System;
using System.Collections.Generic;

namespace CS4540PS2.Models
{
    public partial class Notifications
    {
        public int UserId { get; set; }
        public string Text { get; set; }
        public DateTime DateNotified { get; set; }

        public virtual UserLocator User { get; set; }
    }
}
