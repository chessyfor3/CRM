using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CRM.Models;

namespace CRM.Controllers
{
    public class TaskController : Controller
    {
        private DataContext db = new DataContext();
        // GET: Task
        public ActionResult Index()
        {
            if (Session["user_id"] == null)
            {
                return RedirectToAction("Login", "User");
            }
            if (Session["job_title"].Equals("Manager"))
            {
                return View(db.Task.ToList().FindAll(t=> t.deleted == 0));
            }
            else
            {
                return View(db.Task.ToList().FindAll(t => t.user_id == Int32.Parse(Session["user_id"].ToString()) && t.finished == 0));
            }
            
        }

        public ActionResult Details(int id)
        {
            if (Session["user_id"] == null)
            {
                return RedirectToAction("Login", "User");
            }
            return View(db.Task.Find(id));
        }

        // GET: Promotion/Create
        public ActionResult Create()
        {
            if (Session["user_id"] == null)
            {
                return RedirectToAction("Login", "User");
            }
            Task task = new Task();
            if (!Session["job_title"].Equals("Manager"))
            {
                task.user_id = Int32.Parse(Session["user_id"].ToString());
            }
            else
            {
                task.user_id = Int32.Parse(Session["user_id"].ToString());
            }
            
            task.date = DateTime.Now;
            ViewData["agents"] = db.User.ToList();
            return View(task);
        }

        // POST: Promotion/Create
        [HttpPost]
        public ActionResult Create(Task task)
        {
            try
            {
                // TODO: Add insert logic here
                task.date_created = DateTime.Now;
                task.date_updated = DateTime.Now;
                db.Task.Add(task);
                db.SaveChanges();
                
                return Json(new { status = "success", message = "Task created!", task = task, date = task.date.ToShortDateString() });
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
            ViewData["agents"] = db.User.ToList();
            return View("Create", db.Task.Find(id));
        }

        // POST: Promotion/Edit/5
        [HttpPost]
        public ActionResult Edit(FormCollection collection)
        {
            Task task = db.Task.Find(Int32.Parse(collection["id"]));
            if (TryUpdateModel(task, collection))
            {
                try
                {
                    task.date_updated = DateTime.Now;

                    db.SaveChanges();
                    return Json(new { status = "success", message = "Task information updated", task = task, date = task.date.ToShortDateString() });
                }
                catch
                {
                    return Json(new { status = "error", message = "Something went wrong. " });
                }
            }
            return Json(new { status = "error", message = "Something went wrong. Unable to save changes." });
        }


        // POST: Promotion/Delete/5
        [HttpPost]
        public ActionResult Delete(FormCollection collection)
        {
            try
            {
                Task task = db.Task.Find(Int32.Parse(collection["id"]));
                task.deleted = 1;
                db.SaveChanges();
                return Json(new { status = "success", message = "Task removed" });
            }
            catch
            {
                return Json(new { status = "error", message = "Something went wrong. Unable to delete promo." });
            }
        }

        public ActionResult Today()
        {
            if (Session["user_id"] == null)
            {
                return RedirectToAction("Login", "User");
            }
            var tasks = db.Task.ToList().FindAll(t => t.date.ToShortDateString().Equals(DateTime.Now.ToShortDateString()) && t.finished == 0);
            
            if (tasks.Count() > 0)
            {
                
                if (Session["job_title"].Equals("Manager"))
                {
                    ViewData["agents"] = db.User.ToList();
                    return View(tasks);
                }
                else
                {
                    ViewData["agents"] = db.User.ToList();
                    return View(tasks.FindAll(t=> t.user_id == Int32.Parse(Session["user_id"].ToString()) ));
                }
                
            }
            return Content("No tasks for today.");
        }

        public ActionResult Finished()
        {
            if (Session["user_id"] == null)
            {
                return RedirectToAction("Login", "User");
            }
            var tasks = db.Task.ToList().FindAll(t => t.deleted == 0 && t.finished == 1);

            if (tasks.Count() > 0)
            {
                if (Session["job_title"].Equals("Manager"))
                {
                    ViewData["agents"] = db.User.ToList();
                    return View(tasks);
                }

            }
            return Content("No tasks finished yet.");
        }

        [HttpPost]
        public ActionResult Mark(FormCollection col)
        {
            if (Session["user_id"] == null)
            {
                return RedirectToAction("Login", "User");
            }
            try
            {
                Task task = db.Task.Find(Int32.Parse(col["id"]));
                task.finished = 1;
                if (Session["job_title"].Equals("Manager"))
                {
                    task.deleted = 1;
                }
                db.SaveChanges();
                return Json(new { status = "success", message = "Task marked done." });
            }
            catch
            {
                return Json(new { status = "error", message = "Something went wrong." });
            }
        }
        [HttpPost]
        public ActionResult Confirm(FormCollection col)
        {
            if (Session["user_id"] == null)
            {
                return RedirectToAction("Login", "User");
            }
            try
            {
                Task task = db.Task.Find(Int32.Parse(col["id"]));
                task.deleted = 1;
                db.SaveChanges();
                return Json(new { status = "success", message = "Done task confirmed." });
            }
            catch
            {
                return Json(new { status = "error", message = "Something went wrong." });
            }
        }
    }
}