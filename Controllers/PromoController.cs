using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CRM.Models;
namespace CRM.Controllers
{
    public class PromoController : ApiController
    {
        private DataContext db = new DataContext();
        // GET api/<controller>
        public IHttpActionResult Get()
        {
            return Ok(db.Promotion.ToList());
        }

        // GET api/<controller>/5
        public IHttpActionResult Get(int id)
        {
            Promotion promo = null;
            if (id == 0)
            {
                promo = new Promotion();
                promo.start = DateTime.Now;
                promo.end = DateTime.Now.AddDays(5);
                promo.code = "XXXX";
                promo.description = "Sample description for users. This is a test";
                promo.ext_desc = "This is a description intended for the customers";
                promo.name = "One-Time Big-Time Promo";
            }
            else
            {
                promo = db.Promotion.Find(id);
            }
            
            return Ok(promo);
        }

        // POST api/<controller>
        public IHttpActionResult Post(Promotion promo)
        {
            
                // TODO: Add insert logic here
            promo.date_created = DateTime.Now;
            promo.date_updated = DateTime.Now;
            db.Promotion.Add(promo);
            db.SaveChanges();
            return Ok(promo);
        }

        // PUT api/<controller>/5
        public IHttpActionResult Put(int id, Promotion promo)
        {
            Promotion promot = db.Promotion.Find(id);
            promot = promo;
            promot.date_updated = DateTime.Now;
            db.SaveChanges();
            return Ok(promot);
        }

        // DELETE api/<controller>/5
        public IHttpActionResult Delete(int id)
        {
            Promotion promo = db.Promotion.Find(id);
            db.Promotion.Remove(promo);
            db.SaveChanges();
            return Ok();
        }
    }
}