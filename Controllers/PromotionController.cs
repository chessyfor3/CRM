using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CRM.Models;

namespace CRM.Controllers
{
    public class PromotionController : Controller
    {
        private DataContext db = new DataContext();
        // GET: Promotion
        public ActionResult Index()
        {
            if (Session["user_id"] == null)
            {
                return RedirectToAction("Login", "User");
            }
            return View(db.Promotion.ToList());
        }

        // GET: Promotion/Details/5
        public ActionResult Details(int id)
        {
            if (Session["user_id"] == null)
            {
                return RedirectToAction("Login", "User");
            }
            return View(db.Promotion.Find(id));
        }

        // GET: Promotion/Create
        public ActionResult Create()
        {
            if (Session["user_id"] == null)
            {
                return RedirectToAction("Login", "User");
            }
            Promotion promo = new Promotion();
            promo.start = DateTime.Now;
            promo.end = DateTime.Now.AddDays(5);
            return View(promo);
        }

        // POST: Promotion/Create
        [HttpPost]
        public ActionResult Create(Promotion promo)
        {
            try
            {
                // TODO: Add insert logic here
                promo.date_created = DateTime.Now;
                promo.date_updated = DateTime.Now;
                db.Promotion.Add(promo);
                db.SaveChanges();
                var valid = new {  start = promo.start.ToShortDateString(), end = promo.end.ToShortDateString() };
                return Json(new { status = "success", message = "Promotion created!" , promo = promo, validity = valid });
            }
            catch
            {
                return Json(new { status = "error", message = "Something went wrong. Unable to create promotion." });
            }
        }

        // GET: Promotion/Edit/5
        public ActionResult Edit(int id)
        {
            if (Session["user_id"] == null)
            {
                return RedirectToAction("Login", "User");
            }
            return View("Create",db.Promotion.Find(id));
        }

        // POST: Promotion/Edit/5
        [HttpPost]
        public ActionResult Edit(FormCollection collection)
        {
            Promotion promo = db.Promotion.Find(Int32.Parse(collection["id"]));
            
            try
            {
                promo.date_updated = DateTime.Now;
                promo.code = collection["code"];
                promo.name = collection["name"];
                promo.description = collection["description"];
                promo.ext_desc = collection["ext_desc"];
                promo.start = DateTime.Parse(collection["start"]);
                promo.end = DateTime.Parse(collection["end"]);
                db.SaveChanges();
                var valid = new { start = promo.start.ToShortDateString(), end = promo.end.ToShortDateString() };
                return Json(new { status = "success", message = "Customer information updated", promo = promo, validity = valid});
            }
            catch
            {
                return Json(new { status = "error", message = "Something went wrong. " });
            }
            
        }

       
        // POST: Promotion/Delete/5
        [HttpPost]
        public ActionResult Delete(FormCollection collection)
        {
            try
            {
                Promotion promo = db.Promotion.Find(Int32.Parse(collection["id"]));
                db.Promotion.Remove(promo);
                db.SaveChanges();
                return Json(new { status = "success", message = "Promotion   removed" });
            }
            catch
            {
                return Json(new { status = "error", message = "Something went wrong. Unable to delete promo." });
            }
        }
    }
}
