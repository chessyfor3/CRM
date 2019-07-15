using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CRM.Models
{
    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int ID { get; set; }

        
        [Column(TypeName = "VARCHAR")]
        public string firstname { get; set; }

        
        [Column(TypeName = "VARCHAR")]
        public string lastname { get; set; }

        [Column(TypeName = "VARCHAR")]
        public string job_title { get; set; }


        [Column(TypeName = "VARCHAR")]
        public string email { get; set; }

        [Column(TypeName = "VARCHAR")]
        public string phone { get; set; }

        [Column(TypeName = "VARCHAR")]
        public string street { get; set; }
        [Column(TypeName = "VARCHAR")]
        public string city { get; set; }
        [Column(TypeName = "VARCHAR")]
        public string zip { get; set; }

        [Column(TypeName = "VARCHAR")]
        public string company_id { get; set; }


        [Column(TypeName = "VARCHAR")]
        public string username { get; set; }
        
        [Column(TypeName = "TEXT")]
        public string password { get; set; }

        [Column(TypeName = "DATETIME")]
        public DateTime date_created { get; set; }
        
        public string name
        {
            get
            {
                return String.Format("{0} {1}", firstname, lastname);
            }
        }
        public string address
        {
            get
            {
                return String.Format("{0}, {1} {2}", street, city, zip);
            }
        }
    }
}