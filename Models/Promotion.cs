using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CRM.Models
{
    public class Promotion
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int ID { get; set; }

        [Column(TypeName = "VARCHAR")]
        public string code { get; set; }

        [Column(TypeName = "VARCHAR")]
        public string name { get; set; }

        [Column(TypeName = "TEXT")]
        public string description { get; set; }

        [Column(TypeName = "TEXT")]
        public string ext_desc { get; set; }

        [Column(TypeName = "DATE")]
        public DateTime start { get; set; }

        [Column(TypeName = "DATE")]
        public DateTime end { get; set; }

        
        public int active { get; set; }

        [Column(TypeName = "DATETIME")]
        public DateTime date_created { get; set; }
        [Column(TypeName = "DATETIME")]
        public DateTime date_updated { get; set; }
    }
}