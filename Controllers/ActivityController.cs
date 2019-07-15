using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CRM.Models;

namespace CRM.Controllers
{
    public class ActivityController : Controller
    {
        private DataContext db = new DataContext();
        // GET: Activity
        public ActionResult Index()
        {
            if (Session["user_id"] == null && !Session["job_title"].Equals("Manager"))
            {
                return RedirectToAction("Login", "User");
            }
            ViewData["deals"] = db.Deal.ToList();
            return View(db.Activity.ToList());
        }
        public ActionResult Details(int id)
        {
            if (Session["user_id"] == null && !Session["job_title"].Equals("Manager"))
            {
                return RedirectToAction("Login", "User");
            }
            Activity act = db.Activity.Find(id);
            Deal deal = db.Deal.Find(act.deal_id);
            ViewData["customer"] = db.Customer.Find(deal.customer_id);
            ViewData["deal"] = deal;
            ViewData["agent"] = db.User.Find(deal.agent_id);
            return View(db.Activity.Find(id));
        }
    }
}