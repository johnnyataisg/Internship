using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Internship.Models
{
    [Table("Ticket")]
    public class Ticket
    {
        [Key]
        public int ticketKey { get; set; }
        public string ticketNumber { get; set; }
        public DateTime eventDate { get; set; }

        [ForeignKey("Order")]
        public virtual int? orderKey { get; set; }
        public virtual Order Order { get; set; }
    }
}