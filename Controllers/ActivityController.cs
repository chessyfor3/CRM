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
    public class ActivityController : Controller
    {
        private DataContext db = new DataContext();
        private HttpClient client = new HttpClient();
        private string url = "https://localhost:44387/api/";
        // GET: Activity
        public ActionResult Index()
        {
            if (Session["user_id"] == null)
            {
                return RedirectToAction("Login", "User");
            }
            if (Session["job_title"].Equals("Agent"))
            {
                return RedirectToAction("Login", "User");
            }
            ViewData["deals"] = db.Deal.ToList();
            return View();
        }
        public ActionResult Details(int id)
        {
            
            if (Session["user_id"] == null)
            {
                return RedirectToAction("Login", "User");
            }
            if (Session["job_title"].Equals("Agent"))
            {
                return RedirectToAction("Login", "User");
            }
            client.BaseAddress = new Uri(url);
            //client.DefaultRequestHeaders.Clear();
            //client.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue("nl-NL"));
            //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var responseTask = client.GetAsync(string.Format("act/{0}", id));
            
            responseTask.Wait();
            var result = responseTask.Result;
            
            if (result.IsSuccessStatusCode)
            {
                var activity = result.Content.ReadAsAsync<ActApi>().Result;
                return View(activity);

            }

            //Activity act = db.Activity.Find(id);
            //Deal deal = db.Deal.Find(act.deal_id);
            //ViewData["customer"] = db.Customer.Find(deal.customer_id);
            //ViewData["deal"] = deal;
            //ViewData["agent"] = db.User.Find(deal.agent_id);
            return Content("Something went wrong!");
        }
    }
}