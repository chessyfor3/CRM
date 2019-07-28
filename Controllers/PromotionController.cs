using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CRM.Models;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Net.Http.Headers;

namespace CRM.Controllers
{
    public class PromotionController : Controller
    {
        private DataContext db = new DataContext();
        private System.Net.Http.HttpClient client = new HttpClient();
        private string url = "https://localhost:44387/api/";
        // GET: Promotion
        public ActionResult Index()
        {
            if (Session["user_id"] == null)
            {
                return RedirectToAction("Login", "User");
            }
            client.BaseAddress = new Uri(url);
            var responseTask = client.GetAsync("promo");
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var promos = result.Content.ReadAsAsync<List<Promotion>>().Result;
                return View(promos);

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
            //Promotion promo = new Promotion();
            //promo.start = DateTime.Now;
            //promo.end = DateTime.Now.AddDays(5);
            //promo.code = "XXXX";
            //promo.description = "Sample description for users. This is a test";
            //promo.ext_desc = "This is a description intended for the customers";
            //promo.name = "One-Time Big-Time Promo";
            //return View(promo);
            client.BaseAddress = new Uri(url);
            var responseTask = client.GetAsync(string.Format("promo/{0}", 0));

            responseTask.Wait();
            var result = responseTask.Result;

            if (result.IsSuccessStatusCode)
            {
                var promo = result.Content.ReadAsAsync<Promotion>().Result;
                return View(promo);

            }
            return Content("Something went wrong!");
        }

        // POST: Promotion/Create
        [System.Web.Mvc.HttpPost]
        public ActionResult Create(Promotion promo)
        {
            //try
            //{
            //    // TODO: Add insert logic here
            //    promo.date_created = DateTime.Now;
            //    promo.date_updated = DateTime.Now;
            //    db.Promotion.Add(promo);
            //    db.SaveChanges();
            //    var valid = new {  start = promo.start.ToShortDateString(), end = promo.end.ToShortDateString() };
            //    return Json(new { status = "success", message = "Promotion created!" , promo = promo, validity = valid });
            //}
            //catch
            //{
            //    return Json(new { status = "error", message = "Something went wrong. Unable to create promotion." });
            //}
            client.BaseAddress = new Uri(url);
            var postTask = client.PostAsJsonAsync<Promotion>("promo", promo);
            postTask.Wait();

            var result = postTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var promotion = result.Content.ReadAsAsync<Promotion>().Result;
                var valid = new { start = promo.start.ToShortDateString(), end = promo.end.ToShortDateString() };
                return Json(new { status = "success", message = "Promotion created!", promo = promotion, validity = valid });
            }
            return Content("Something went wrong!");
        }

        // GET: Promotion/Edit/5
        public ActionResult Edit(int id)
        {
            if (Session["user_id"] == null)
            {
                return RedirectToAction("Login", "User");
            }
            client.BaseAddress = new Uri(url);
            var responseTask = client.GetAsync(string.Format("promo/{0}", id));

            responseTask.Wait();
            var result = responseTask.Result;

            if (result.IsSuccessStatusCode)
            {
                var promo = result.Content.ReadAsAsync<Promotion>().Result;
                return View("Create",promo);

            }
            return Content("Something went wrong!");
        }

        // POST: Promotion/Edit/5
        [System.Web.Mvc.HttpPost]
        public ActionResult Edit(Promotion promo)
        {

            //try
            //{
            //    promo.date_updated = DateTime.Now;
            //    promo.code = collection["code"];
            //    promo.name = collection["name"];
            //    promo.description = collection["description"];
            //    promo.ext_desc = collection["ext_desc"];
            //    promo.start = DateTime.Parse(collection["start"]);
            //    promo.end = DateTime.Parse(collection["end"]);
            //    db.SaveChanges();
            //    var valid = new { start = promo.start.ToShortDateString(), end = promo.end.ToShortDateString() };
            //    return Json(new { status = "success", message = "Customer information updated", promo = promo, validity = valid});
            //}
            //catch
            //{
            //    return Json(new { status = "error", message = "Something went wrong. " });
            //}
            
            client.BaseAddress = new Uri(url);
            var putTask = client.PutAsJsonAsync<Promotion>(string.Format("promo/{0}", promo.ID), promo);
            putTask.Wait();
            var result = putTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var promotion = result.Content.ReadAsAsync<Promotion>().Result;
                var valid = new { start = promo.start.ToShortDateString(), end = promo.end.ToShortDateString() };
                return Json(new { status = "success", message = "Promotion created!", promo = promotion, validity = valid });
            }
            return Content("Something went wrong!");

        }

       
        // POST: Promotion/Delete/5
        [System.Web.Mvc.HttpPost]
        public ActionResult Delete(FormCollection collection)
        {
            //try
            //{
            //    Promotion promo = db.Promotion.Find(Int32.Parse(collection["id"]));
            //    db.Promotion.Remove(promo);
            //    db.SaveChanges();
            //    return Json(new { status = "success", message = "Promotion   removed" });
            //}
            //catch
            //{
            //    return Json(new { status = "error", message = "Something went wrong. Unable to delete promo." });
            //}
            int id = Int32.Parse(collection["id"]);
            client.BaseAddress = new Uri(url);
            
            var deleteTask = client.DeleteAsync(string.Format("promo/{0}", id));
            deleteTask.Wait();
            var result = deleteTask.Result;
            if (result.IsSuccessStatusCode)
            {
                return Json(new { status = "success", message = "Promotion   removed" });
            }
            return Content("Something went wrong!");
        }
    }
}
