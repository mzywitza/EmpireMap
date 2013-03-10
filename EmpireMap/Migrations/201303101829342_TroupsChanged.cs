namespace EmpireMap.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TroupsChanged : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Troups", "Deff", c => c.Int(nullable: false));
            AddColumn("dbo.Troups", "EnhancedDeff", c => c.Int(nullable: false));
            AddColumn("dbo.Troups", "Off", c => c.Int(nullable: false));
            AddColumn("dbo.Troups", "EnhancedOff", c => c.Int(nullable: false));
            DropColumn("dbo.Troups", "TroupCount");
            DropColumn("dbo.Troups", "IsDeff");
            DropColumn("dbo.Troups", "IsVeteran");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Troups", "IsVeteran", c => c.Boolean(nullable: false));
            AddColumn("dbo.Troups", "IsDeff", c => c.Boolean(nullable: false));
            AddColumn("dbo.Troups", "TroupCount", c => c.Int(nullable: false));
            DropColumn("dbo.Troups", "EnhancedOff");
            DropColumn("dbo.Troups", "Off");
            DropColumn("dbo.Troups", "EnhancedDeff");
            DropColumn("dbo.Troups", "Deff");
        }
    }
}
