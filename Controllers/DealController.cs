using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CRM.Models;

namespace CRM.Controllers
{
    public class DealController : Controller
    {
        private DataContext db = new DataContext();
        // GET: Deal
       
        public ActionResult Index()
        {
            if (Session["user_id"] != null)
            {
                ViewData["customers"] = db.Customer.ToList();
                if (Session["job_title"].Equals("Manager"))
                {
                    ViewData["customers"] = db.Customer.ToList();
                    return View(db.Deal.ToList());
                }
                return View(db.Deal.ToList().FindAll(d => d.agent_id == Int32.Parse(Session["user_id"].ToString())));
            }
            return RedirectToAction("Login", "User");
            
        }

        // GET: Deal/Details/5
        public ActionResult Details(int id)
        {
            if (Session["user_id"] == null)
            {
                return RedirectToAction("Login", "User");
            }
            Deal deal = db.Deal.Find(id);
            ViewData["customer"] = db.Customer.Find(deal.customer_id);
            if (Session["job_title"].Equals("Manager"))
            {
                ViewData["agent"] = db.User.Find(deal.agent_id);
            }
            ViewData["next-stage"] = getNextStage(deal.stage);
            return View(deal);
        }

        // GET: Deal/Create
        public ActionResult Create()
        {
            if (Session["user_id"] == null)
            {
                return RedirectToAction("Login", "User");
            }
            if (db.Customer.Count() > 0)
            {
                Deal deal = new Deal();
                deal.stage = "Leads";
                deal.expected_closing = DateTime.Now.AddDays(7);
                ViewData["promotions"] = db.Promotion.ToList();
                ViewData["customers"] = db.Customer.ToList();
                return View(deal);
            }
            else
            {
                
                return Content("<div class='alert alert - danger' role='alert'>No customer found. Input customer details in customer section.</ div > ");
            }
        }

        // POST: Deal/Create
        [HttpPost]
        public ActionResult Create(Deal deal)
        {
            try
            {

                // TODO: Add insert logic here
                deal.agent_id = Int32.Parse(Session["user_id"].ToString());
                deal.date_created = DateTime.Now;
                deal.date_updated = DateTime.Now;
                db.Deal.Add(deal);
                db.SaveChanges();
                Activity act = new Activity();
                act.deal_id = deal.ID;
                act.stage = deal.stage;
                act.type = "Creation";
                act.date_created = DateTime.Now;
                db.Activity.Add(act);
                db.SaveChanges();

                return Json(new { status = "success", message = "Promotion created!", deal = deal, customer = db.Customer.Find(deal.customer_id) });
            }
            catch
            {
                return Json(new { status = "error", message = "Something went wrong. Unable to create deal." });
            }
        }

        // GET: Deal/Edit/5
        public ActionResult Edit(int id)
        {
            if (Session["user_id"] == null)
            {
                return RedirectToAction("Login", "User");
            }
            if (db.Customer.Count() > 0)
            {
                ViewData["promotions"] = db.Promotion.ToList();
                ViewData["customers"] = db.Customer.ToList();

            }
            else
            {
                return Content("<div class='alert alert - danger' role='alert'>No customer found. Input customer details in customer section.</ div > ");
            }
            return View("Create", db.Deal.Find(id));
        }

        // POST: Deal/Edit/5
        [HttpPost]
        public ActionResult Edit(FormCollection collection)
        {
            Deal deal = db.Deal.Find(Int32.Parse(collection["id"]));
            if (TryUpdateModel(deal, collection))
            {
                try
                {
                    // TODO: Add update logic here
                    deal.agent_id = Int32.Parse(Session["user_id"].ToString());
                    deal.date_updated = DateTime.Now;
                    db.SaveChanges();
                    ViewData["mode"] = "edit";
                    return Json(new { status = "success", message = "Customer information updated", deal = db.Deal.Find(Int32.Parse(collection["id"])), customer = db.Customer.Find(deal.customer_id) });
                }
                catch
                {
                    return Json(new { status = "error", message = "Something went wrong. " });
                }
            }
            return Json(new { status = "error", message = "Something went wrong. Unable to save changes." });
        }


        // POST: Deal/Delete/5
        [HttpPost]
        public ActionResult Delete(FormCollection collection)
        {
            try
            {
                Deal deal = db.Deal.Find(Int32.Parse(collection["id"]));
                deal.deleted = 1;
                db.SaveChanges();
                return Json(new { status = "success", message = "Promotion   removed" });
            }
            catch
            {
                return Json(new { status = "error", message = "Something went wrong. Unable to delete promo." });
            }
        }
        [HttpPost]
        public JsonResult Upgrade(FormCollection collection)
        {
            
            Deal deal = db.Deal.Find(Int32.Parse(collection["id"]));
            deal.stage = getNextStage(deal.stage);
            db.SaveChanges();
            Activity act = new Activity();
            act.deal_id = deal.ID;
            act.type = "Upgrade";
            act.stage = deal.stage;
            act.date_created = DateTime.Now;
            db.Activity.Add(act);
            db.SaveChanges();
            return Json(new { status ="success", message = "Deal is on " + deal.stage, stage = deal.stage , next = getNextStage(deal.stage)  });
        }

        private string getNextStage(string stage)
        {
            string[] stages = new string[] { "Leads", "Pitched", "Qualified", "Proposal", "Negotiation", "Closed" };
            var stg = "Closed";
            switch (stage)
            {
                case "Leads": stg = stages[1]; break;
                case "Pitched": stg = stages[2]; break;
                case "Qualified": stg = stages[3]; break;
                case "Proposal": stg = stages[4]; break;
                case "Negotiation": stg = stages[5]; break;
                default: break;
            }
            return stg;
        }
        [HttpPost]
        public JsonResult Win(FormCollection col)
        {
            Deal deal = db.Deal.Find(Int32.Parse(col["id"]));
            if( 1 == Int32.Parse(col["win"]))
            {
                deal.win = 1;
                db.SaveChanges();
                
            }
            return Json(new { status = "success" , message = "Success" });
        }
    }
}