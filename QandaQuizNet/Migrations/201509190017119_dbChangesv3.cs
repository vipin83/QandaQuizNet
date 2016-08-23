namespace QandaQuizNet.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dbChangesv3 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TopupAccounts", "finalPaymentAmount", c => c.Decimal(precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TopupAccounts", "finalPaymentAmount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
