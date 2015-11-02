namespace REST.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Events",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CreatorId = c.String(nullable: false),
                        Name = c.String(nullable: false, maxLength: 450),
                        Description = c.String(),
                        Min = c.Int(nullable: false),
                        Max = c.Int(nullable: false),
                        StartDateTimeEvent = c.DateTime(nullable: false),
                        EventDuration = c.Int(nullable: false),
                        Duration = c.Int(nullable: false),
                        Target = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true);
            
            AddColumn("dbo.AspNetUsers", "Event_Id", c => c.Int());
            CreateIndex("dbo.AspNetUsers", "Event_Id");
            AddForeignKey("dbo.AspNetUsers", "Event_Id", "dbo.Events", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "Event_Id", "dbo.Events");
            DropIndex("dbo.AspNetUsers", new[] { "Event_Id" });
            DropIndex("dbo.Events", new[] { "Name" });
            DropColumn("dbo.AspNetUsers", "Event_Id");
            DropTable("dbo.Events");
        }
    }
}
