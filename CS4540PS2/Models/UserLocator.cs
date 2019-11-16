using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        [Key]
        public int Id { get; set; }
        public string UserLoginEmail { get; set; }
        public string UserTitle { get; set; }

        public virtual Notifications Notifications { get; set; }
        public virtual ICollection<Instructors> Instructors { get; set; }
        [InverseProperty(nameof(Messages.Sender))]
        public virtual ICollection<Messages> MessagesReceiverNavigation { get; set; }
        [InverseProperty(nameof(Messages.Receiver))]
        public virtual ICollection<Messages> MessagesSenderNavigation { get; set; }
    }
}
