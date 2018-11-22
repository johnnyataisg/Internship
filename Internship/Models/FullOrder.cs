using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Internship.Models
{
    public class FullOrder
    {
        public int Key { get; set; }
        public int ID { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public string TicketNumber { get; set; }
        public DateTime EventDate { get; set; }
    }
}