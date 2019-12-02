using System;
using System.Collections.Generic;

namespace CS4540PS2.Models
{
    public partial class UserLocator
    {
        public UserLocator()
        {
            Instructors = new HashSet<Instructors>();
            MessagesReceiverNavigation = new HashSet<Messages>();
            MessagesSenderNavigation = new HashSet<Messages>();
        }

        public int Id { get; set; }
        public string UserLoginEmail { get; set; }
        public string UserTitle { get; set; }

        public virtual ICollection<Notifications> Notifications { get; set; }
        public virtual ICollection<Instructors> Instructors { get; set; }
        public virtual ICollection<Messages> MessagesReceiverNavigation { get; set; }
        public virtual ICollection<Messages> MessagesSenderNavigation { get; set; }
    }
}
