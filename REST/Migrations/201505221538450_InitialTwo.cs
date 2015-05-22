namespace REST.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialTwo : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Pulses", "ApplicationUserId", "dbo.AspNetUsers");
            DropIndex("dbo.Pulses", new[] { "ApplicationUserId" });
            AlterColumn("dbo.Pulses", "ApplicationUserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Pulses", "ApplicationUserId");
            AddForeignKey("dbo.Pulses", "ApplicationUserId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Pulses", "ApplicationUserId", "dbo.AspNetUsers");
            DropIndex("dbo.Pulses", new[] { "ApplicationUserId" });
            AlterColumn("dbo.Pulses", "ApplicationUserId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Pulses", "ApplicationUserId");
            AddForeignKey("dbo.Pulses", "ApplicationUserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
    }
}
