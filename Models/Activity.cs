using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CRM.Models
{
    public class Activity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int ID { get; set; }

        public int deal_id { get; set; }

        [Column(TypeName = "VARCHAR")]
        public string type { get; set; }

        [Column(TypeName = "VARCHAR")]
        public string stage { get; set; }

        [Column(TypeName = "DATETIME")]
        public DateTime date_created { get; set; }
        
    }
}