namespace QandaQuizNet.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dbChangesv4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "accountBalance", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "accountBalance");
        }
    }
}
