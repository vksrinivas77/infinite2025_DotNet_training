    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using MVC_CC_9.Models;

    namespace MVC_CC_9.Controllers
    {
        public class CodeController : Controller
        {
            northwindEntities context = new northwindEntities();

            public ActionResult Index()
            {
                var Customers = context.Customers.ToList();
                return View(Customers);
            }

            public ActionResult GetCustomerByCountryGermany()
            {
                var Customers = context.Customers.ToList();
                var CustomersGermany = from data in Customers where (data.Country).Equals("Germany") select data;
                return View(CustomersGermany);
            }

            public ActionResult GetCustomerByOrderID()
            {
                var Customers = context.Customers.ToList();
                var Orders = context.Orders.ToList();

                var CustomersByOrderID = from data in Customers
                                         join order in Orders on data.CustomerID equals order.CustomerID
                                         where order.OrderID == 10248
                                         select data;

                return View(CustomersByOrderID);

            }




        }
    }