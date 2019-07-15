using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CRM.Models
{
    public class Deal
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int ID { get; set; }

        [Column(TypeName = "VARCHAR")]
        public string title { get; set; }

        [Column(TypeName = "TEXT")]
        public string description { get; set; }

        [Column(TypeName = "FLOAT")]
        public float value { get; set; }

        [Column(TypeName = "VARCHAR")]
        public string stage { get; set; }

        [Column(TypeName = "FLOAT")]
        public float closing_probability { get; set; }

        [Column(TypeName = "DATE")]
        public DateTime expected_closing { get; set; }  

        

        [Column(TypeName = "VARCHAR")]
        public string promo_code { get; set; }

        public int agent_id { get; set; }

        public int customer_id { get; set; }

        public int win { get; set; }

        public int deleted { get; set; }

        [Column(TypeName = "DATETIME")]
        public DateTime date_created { get; set; }
        [Column(TypeName = "DATETIME")]
        public DateTime date_updated { get; set; }

    }
}