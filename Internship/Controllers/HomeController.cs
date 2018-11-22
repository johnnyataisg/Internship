using Internship.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using Internship.DAL;

namespace Internship.Controllers
{
    public class HomeController : Controller
    {
        private InternshipContext ordersDatabase = new InternshipContext();

        [HttpGet]
        public ActionResult Index()
        {
            return View(new List<FullOrder>());
        }

        [HttpPost]
        public ActionResult Index(HttpPostedFileBase postedFile)
        {
            List<FullOrder> fullOrders = new List<FullOrder>();
            string filePath = string.Empty;
            if (postedFile != null)
            {
                string path = Server.MapPath("~/Uploads/");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                filePath = path + Path.GetFileName(postedFile.FileName);
                string extension = Path.GetExtension(postedFile.FileName);
                postedFile.SaveAs(filePath);

                //Read the contents of CSV file.
                string csvData = System.IO.File.ReadAllText(filePath);

                int i = 0;
                //Execute a loop over the rows.
                foreach (string row in csvData.Split('\n'))
                {
                    if (!string.IsNullOrEmpty(row) && i != 0)
                    {
                        fullOrders.Add(new FullOrder
                        {
                            Key = i + 1,
                            ID = Convert.ToInt32(row.Split(',')[0]),
                            FName = row.Split(',')[1],
                            LName = row.Split(',')[2],
                            TicketNumber = row.Split(',')[3],
                            EventDate = Convert.ToDateTime(row.Split(',')[4])
                        });
                    }
                    i++;
                }
                foreach(FullOrder order in fullOrders)
                {
                    ordersDatabase.Orders.Add(new Order
                    {
                        orderID = order.ID,
                        firstName = order.FName,
                        lastName = order.LName
                    });
                    ordersDatabase.SaveChanges();
                    ordersDatabase.Tickets.Add(new Ticket
                    {
                        orderKey = ordersDatabase.Orders.ToList().Last().orderKey,
                        ticketNumber = order.TicketNumber,
                        eventDate = order.EventDate
                    });
                    ordersDatabase.SaveChanges();
                }
            }

            return View(fullOrders);
        }

        public ActionResult DisplayAll()
        {
            List<FullOrder> allOrders = new List<FullOrder>();
            foreach(Order order in ordersDatabase.Orders.ToList())
            {
                Ticket ticket = ordersDatabase.Tickets.Where(tkt => tkt.orderKey == order.orderKey).FirstOrDefault();
                
                allOrders.Add(new FullOrder
                {
                    Key = order.orderKey,
                    ID = order.orderID,
                    FName = order.firstName,
                    LName = order.lastName,
                    TicketNumber = ticket.ticketNumber,
                    EventDate = ticket.eventDate
                });
            }
            return View(allOrders);
        }

        public ActionResult DeleteAll()
        {
            IList<Order> removeOrders = ordersDatabase.Orders.ToList();
            ordersDatabase.Orders.RemoveRange(removeOrders);
            ordersDatabase.SaveChanges();
            IList<Ticket> removeTickets = ordersDatabase.Tickets.ToList();
            ordersDatabase.Tickets.RemoveRange(removeTickets);
            ordersDatabase.SaveChanges();
            return RedirectToAction("DisplayAll");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}