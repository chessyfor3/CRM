using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CRM.Models;
namespace CRM.Controllers
{
    public class ActController : ApiController
    {
        private DataContext db = new DataContext();
        // GET api/<controller>
        public List<ActApi> Get()
        {
            List<ActApi> act = new List<ActApi>();
            var activities = db.Activity.ToList();
            foreach (var activity in activities)
            {
                ActApi temp = new ActApi();
                Deal deal = db.Deal.Find(activity.deal_id);
                temp.ID = activity.ID;
                temp.deal_title = deal.title;
                temp.agent = db.User.Find(deal.agent_id).name;
                temp.customer = db.Customer.Find(deal.customer_id).name;
                temp.stage = activity.stage;
                temp.type = activity.type;
                temp.date_created = activity.date_created.ToShortDateString() + " " + activity.date_created.ToLongDateString();
                act.Add(temp);
            }
            return act;
        }

        // GET api/<controller>/5
        public IHttpActionResult Get(int id)
        {
            Activity activity = db.Activity.Find(id);
            Deal deal = db.Deal.Find(activity.deal_id);
            ActApi temp = new ActApi();
            temp.ID = activity.ID;
            temp.deal_title = deal.title;
            temp.agent = db.User.Find(deal.agent_id).name;
            temp.customer = db.Customer.Find(deal.customer_id).name;
            temp.stage = activity.stage;
            temp.type = activity.type;
            temp.date_created = activity.date_created.ToShortDateString() + " " + activity.date_created.ToLongDateString();
            return Ok(temp);
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}