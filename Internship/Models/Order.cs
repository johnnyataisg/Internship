using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Internship.Models
{
    [Table("Order")]
    public class Order
    {
        [Key]
        public int orderKey { get; set; }
        public int orderID { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
    }
}