using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CRM.Models;

namespace CRM.Controllers
{
    public class UserController : Controller
    {
        private DataContext db = new DataContext();

        public ActionResult Index()
        {
            if (Session["user_id"] != null)
            {
                
                var deals = db.Deal.ToList().FindAll(d=>d.agent_id == Int32.Parse(Session["user_id"].ToString()));
                var customers = db.Customer.ToList();
                var list = db.Activity.ToList();
                
                var index = "AgentIndex";
                if (Session["job_title"].Equals("Manager"))
                {
                    index = "ManagerIndex";
                    list = db.Activity.ToList();
                    deals = db.Deal.ToList();
                    customers = db.Customer.ToList();
                }
                
                ViewData["activities"] = list;
                ViewData["deals"] = deals;
                ViewData["customers"] = customers;
                ViewData["leads-count"] = list.FindAll(a => a.stage.Equals("Leads")).Count;
                ViewData["pitched-count"] = list.FindAll(a => a.stage.Equals("Pitched")).Count;
                ViewData["qualified-count"] = list.FindAll(a => a.stage.Equals("Qualified")).Count;
                ViewData["proposal-count"] = list.FindAll(a => a.stage.Equals("Proposal")).Count;
                ViewData["negotiation-count"] = list.FindAll(a => a.stage.Equals("Negotiation")).Count;
                ViewData["closed-count"] = list.FindAll(a => a.stage.Equals("Closed")).Count;
                ViewData["activity-count"] = list.Count();
                ViewData["win-count"] = deals.FindAll(d => d.win == 1).Count;
                ViewData["lost-count"] = deals.FindAll(d => d.win == 1).Count;
                ViewData["totalValue"] = db.Deal.Sum(d => d.value);
                return View(index);
                
            }
            return RedirectToAction("Login");
        }

        // GET: User
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(FormCollection collection)
        {
            if(db.User.ToList().Count <= 0)
            {
                TempData["message"] = "Invalid login. No users found.";
                return RedirectToAction("Register");
            }
            if (db.User.ToList().FindAll(u => u.username.Equals(collection["username"])).Count > 0)
            {
                User user = db.User.ToList().Find(u => u.username.Equals(collection["username"]));
                if (user.password.Equals(collection["password"]))
                {
                    ViewData["message"] = "Welcome " + user.name + "!";
                    Session["user_id"] = user.ID;
                    Session["user_name"] = user.name;
                    Session["job_title"] = user.job_title;

                    return RedirectToAction("Index");
                }
                TempData["message"] = "Wrong password";
            }
            else
            {
                TempData["message"] = "Username not found.";
            }
            
            return View();
        }

        public ActionResult Logout()
        {
            Session.Remove("user_id");
            Session.Remove("user_name");
            Session.Abandon();
            return RedirectToAction("Login");
        }

        public ActionResult Register()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult Register(User user)
        {
            try
            {
                user.date_created = DateTime.Now;
                db.User.Add(user);
                db.SaveChanges();
                TempData["registered"] = "Successfully registered!";
                return RedirectToAction("Login");
            }
            catch
            {
                ViewBag.Error = "Something went wrong!";
            }

            return View();
        }
        [HttpPost]
        public ActionResult Email(FormCollection collection)
        {
            try
            {

                var users = db.User.ToList();
                if (null != users)
                {
                    foreach (var user in users)
                    {
                        if (user.email.Equals(collection["email"]))
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
        [HttpPost]
        public ActionResult Username(FormCollection collection)
        {
            try
            {
                var users = db.User.ToList();
                if (null != users)
                {
                    foreach( var user in users)
                    {
                        if (user.username.Equals(collection["username"]))
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