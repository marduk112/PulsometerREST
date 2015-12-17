namespace REST.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Events", "StepsNumber", c => c.Int(nullable: false));
            AddColumn("dbo.Events", "EndDateTimeEvent", c => c.DateTime(nullable: false));
            DropColumn("dbo.Events", "Min");
            DropColumn("dbo.Events", "Max");
            DropColumn("dbo.Events", "EventDuration");
            DropColumn("dbo.Events", "Duration");
            DropColumn("dbo.Events", "Target");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Events", "Target", c => c.Int(nullable: false));
            AddColumn("dbo.Events", "Duration", c => c.Int(nullable: false));
            AddColumn("dbo.Events", "EventDuration", c => c.Int(nullable: false));
            AddColumn("dbo.Events", "Max", c => c.Int(nullable: false));
            AddColumn("dbo.Events", "Min", c => c.Int(nullable: false));
            DropColumn("dbo.Events", "EndDateTimeEvent");
            DropColumn("dbo.Events", "StepsNumber");
        }
    }
}
