namespace MultiSearch.API.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SearchRequests",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Query = c.String(),
                        Date = c.DateTimeOffset(nullable: false, precision: 7, defaultValueSql:"GETUTCDATE()"),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SearchResults",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Url = c.String(),
                        RequestId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SearchRequests", t => t.RequestId, cascadeDelete: true)
                .Index(t => t.RequestId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SearchResults", "RequestId", "dbo.SearchRequests");
            DropIndex("dbo.SearchResults", new[] { "RequestId" });
            DropTable("dbo.SearchResults");
            DropTable("dbo.SearchRequests");
        }
    }
}
