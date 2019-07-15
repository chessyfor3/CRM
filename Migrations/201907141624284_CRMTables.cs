namespace CRM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CRMTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Activities",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        deal_id = c.Int(nullable: false),
                        type = c.String(maxLength: 255, unicode: false),
                        stage = c.String(maxLength: 255, unicode: false),
                        date_created = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        name = c.String(maxLength: 255, unicode: false),
                        business_type = c.String(maxLength: 255, unicode: false),
                        industry = c.String(maxLength: 255, unicode: false),
                        email = c.String(maxLength: 255, unicode: false),
                        phone = c.String(maxLength: 255, unicode: false),
                        street = c.String(maxLength: 255, unicode: false),
                        city = c.String(maxLength: 255, unicode: false),
                        zip = c.String(maxLength: 255, unicode: false),
                        customer_type = c.String(maxLength: 255, unicode: false),
                        date_created = c.DateTime(nullable: false, precision: 0),
                        deleted = c.Int(nullable: false, defaultValueSql: "0"),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Deals",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        title = c.String(maxLength: 255, unicode: false),
                        description = c.String(unicode: false, storeType: "text"),
                        value = c.Single(nullable: false),
                        stage = c.String(maxLength: 255, unicode: false),
                        closing_probability = c.Single(nullable: false),
                        expected_closing = c.DateTime(nullable: false, storeType: "date"),
                        promo_code = c.String(maxLength: 255, unicode: false),
                        agent_id = c.Int(nullable: false),
                        customer_id = c.Int(nullable: false),
                        win = c.Int(nullable: false, defaultValueSql: "0"),
                        deleted = c.Int(nullable: false, defaultValueSql: "0"),
                        date_created = c.DateTime(nullable: false, precision: 0),
                        date_updated = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Promotions",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        code = c.String(maxLength: 255, unicode: false),
                        name = c.String(maxLength: 255, unicode: false),
                        description = c.String(unicode: false, storeType: "text"),
                        ext_desc = c.String(unicode: false, storeType: "text"),
                        start = c.DateTime(nullable: false, storeType: "date"),
                        end = c.DateTime(nullable: false, storeType: "date"),
                        active = c.Int(nullable: false, defaultValueSql: "0"),
                        date_created = c.DateTime(nullable: false, precision: 0),
                        date_updated = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Tasks",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        user_id = c.Int(nullable: false),
                        title = c.String(maxLength: 255, unicode: false),
                        description = c.String(unicode: false, storeType: "text"),
                        date = c.DateTime(nullable: false, precision: 0),
                        date_created = c.DateTime(nullable: false, precision: 0),
                        date_updated = c.DateTime(nullable: false, precision: 0),
                        finished = c.Int(nullable: false, defaultValueSql: "0"),
                        deleted = c.Int(nullable: false, defaultValueSql:"0"),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        firstname = c.String(maxLength: 255, unicode: false),
                        lastname = c.String(maxLength: 255, unicode: false),
                        job_title = c.String(maxLength: 255, unicode: false),
                        email = c.String(maxLength: 255, unicode: false),
                        phone = c.String(maxLength: 255, unicode: false),
                        street = c.String(maxLength: 255, unicode: false),
                        city = c.String(maxLength: 255, unicode: false),
                        zip = c.String(maxLength: 255, unicode: false),
                        company_id = c.String(maxLength: 255, unicode: false),
                        username = c.String(maxLength: 255, unicode: false),
                        password = c.String(unicode: false, storeType: "text"),
                        date_created = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Users");
            DropTable("dbo.Tasks");
            DropTable("dbo.Promotions");
            DropTable("dbo.Deals");
            DropTable("dbo.Customers");
            DropTable("dbo.Activities");
        }
    }
}
