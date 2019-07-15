using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.ComponentModel;
namespace CRM.Models
{
    public class Customer
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int ID { get; set; }

        [Column(TypeName = "VARCHAR")]
        public string name { get; set; }

        [Column(TypeName = "VARCHAR")]
        public string business_type { get; set; }

        [Column(TypeName = "VARCHAR")]
        public string industry { get; set; }

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
        public string customer_type { get; set; }

        [Column(TypeName = "DATETIME")]
        public DateTime date_created { get; set; }


        public int deleted { get; set; }
        public string address
        {
            get
            {
                
                return String.Format("{0}, {1} {2}", street, city, zip);
            }
        }
    }
}