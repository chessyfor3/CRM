using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using MySql.Data.EntityFramework;

namespace CRM.Models
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class DataContext : DbContext

    {
        public DataContext()
        : base("name=dbConnection") //This 'testConnection' should be equal to the connection string name on Web.config.
        {
            //this.Configuration.ValidateOnSaveEnabled = false;
        }
        public DbSet<Promotion> Promotion { get; set; }
        public DbSet<Deal> Deal { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<Activity> Activity { get; set; }
        public DbSet<Task> Task { get; set; }
    }
}