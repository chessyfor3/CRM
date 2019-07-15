using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CRM.Models;

namespace CRM.Controllers
{
    public class CustomerController : Controller
    {
        private DataContext db = new DataContext();
        // GET: Customer
        public ActionResult Index()
        {
            if (Session["user_id"] != null)
            {
                return View(db.Customer.ToList());
            }
            return RedirectToAction("Login","User");
        }

        // GET: Customer/Details/5
        public ActionResult Details(int id)
        {
            if (Session["user_id"] == null)
            {
                return RedirectToAction("Login", "User");
            }
            return View(db.Customer.Find(id));
        }

        // GET: Customer/Create
        public ActionResult Create()
        {
            if (Session["user_id"] == null)
            {
                return RedirectToAction("Login", "User");
            }
            return View(new Customer());
        }

        // POST: Customer/Create
        [HttpPost]
        public ActionResult Create(Customer customer)
        {
            
            try
            {
                // TODO: Add insert logic here
                customer.date_created = DateTime.Now;
                db.Customer.Add(customer);
                db.SaveChanges();
                return Json(new { status = "success", message = "Customer successfully saved!", customer = customer });
            }
            catch
            {
                return Json(new { status = "success", message = "Something went wrong!" });
            }
        }

        // GET: Customer/Edit/5
        public ActionResult Edit(int id)
        {
            if (Session["user_id"] == null)
            {
                return RedirectToAction("Login", "User");
            }
            return View("Create",db.Customer.Find(id));
        }

        // POST: Customer/Edit/5
        [HttpPost]
        public ActionResult Edit(FormCollection collection)
        {
            
            var customer = db.Customer.Find(Int32.Parse(collection["id"]));
            if (TryUpdateModel(customer,collection))
            {
                try
                {
                    // TODO: Add update logic here
                    db.SaveChanges();
                    
                }
                catch
                {
                    return Json(new { status = "error", message = "Something went wrong. Unable to save changes." });
                }
            }
            return Json(new { status = "success", message = "Customer information updated", customer = customer });
        }


        [HttpPost]
        public ActionResult Delete(FormCollection collection)
        {

            
            try
            {
                // TODO: Add delete logic here
                Customer customer = db.Customer.Find(Int32.Parse(collection["id"]));
                db.Customer.Remove(customer);
                db.SaveChanges();
                return Json(new { status = "success", message = "Customer removed" });
            }
            catch
            {
                return Json(new { status = "error", message = "Something went wrong. User was not removed." });
            }
        }

        [HttpPost]
        public ActionResult Email(FormCollection collection)
        {
            try
            {

                var customers = db.User.ToList();
                if (null != customers)
                {
                    foreach (var customer in customers)
                    {
                        if (customer.email.Equals(collection["email"]))
                        {
                            return Json(new { status = "found" });
                        }

                    }
                }
            }
            catch
            {
                return Json(new { status = "error" });
            }

            return Json(new { status = "none" });
        }
    }
}
