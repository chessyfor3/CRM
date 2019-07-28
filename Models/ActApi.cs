using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRM.Models
{
    public class ActApi
    {
        public int ID { get; set; }

        public string deal_title { get; set; }

        public string type { get; set; }

        public string stage { get; set; }

        public string date_created { get; set; }
        public string customer { get; set; }
        public string agent { get; set; }
    }
}