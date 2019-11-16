using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CS4540PS2.Models
{
    public partial class Messages
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime? Date { get; set; }
        public int Sender { get; set; }
        public int Receiver { get; set; }

        [ForeignKey("Sender")]
        public virtual UserLocator ReceiverNavigation { get; set; }
        [ForeignKey("Receiver")]
        public virtual UserLocator SenderNavigation { get; set; }
    }
}
