namespace REST.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Events", "CreatorId", "dbo.AspNetUsers");
            DropIndex("dbo.Events", new[] { "CreatorId" });
            AlterColumn("dbo.Events", "CreatorId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Events", "CreatorId");
            AddForeignKey("dbo.Events", "CreatorId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Events", "CreatorId", "dbo.AspNetUsers");
            DropIndex("dbo.Events", new[] { "CreatorId" });
            AlterColumn("dbo.Events", "CreatorId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Events", "CreatorId");
            AddForeignKey("dbo.Events", "CreatorId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
    }
}
