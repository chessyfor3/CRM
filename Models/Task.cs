using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CRM.Models
{
    public class Task
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int ID { get; set; }
        public int user_id { get; set; }
        [Column(TypeName = "VARCHAR")]
        public string title { get; set; }
        [Column(TypeName = "TEXT")]
        public string description { get; set; }
        [Column(TypeName = "DATETIME")]
        public DateTime date { get; set; }
        [Column(TypeName ="DATETIME")]
        public DateTime date_created { get; set; }
        [Column(TypeName = "DATETIME")]
        public DateTime date_updated { get; set; }
        public int finished { get; set; }
        public int deleted { get; set; }
    }
}