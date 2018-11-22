using Internship.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Internship.DAL
{
    public class InternshipContext : DbContext
    {
        public InternshipContext() : base("InternshipContext")
        {

        }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Ticket> Tickets { get; set; }

        public System.Data.Entity.DbSet<Internship.Models.FullOrder> FullOrders { get; set; }
    }
}