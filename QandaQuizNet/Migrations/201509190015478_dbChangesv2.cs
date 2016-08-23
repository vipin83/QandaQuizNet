namespace QandaQuizNet.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dbChangesv2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TopupAccounts", "finalPaymentAmount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.TopupAccounts", "paymentSuccessfulDateTime", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TopupAccounts", "paymentSuccessfulDateTime", c => c.DateTime(nullable: false));
            DropColumn("dbo.TopupAccounts", "finalPaymentAmount");
        }
    }
}
