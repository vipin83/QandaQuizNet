namespace QandaQuizNet.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LogEntryTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LogEntry",
                c => new
                    {
                        LogEntryId = c.Int(nullable: false, identity: true),
                        LogEntryDateTime = c.DateTime(nullable: false), 
                        LogEntryDescription = c.String(),
                    })
                .PrimaryKey(t => t.LogEntryId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.LogEntry");
        }
    }
}
