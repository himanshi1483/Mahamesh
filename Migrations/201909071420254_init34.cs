namespace Mahamesh.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init34 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SelectedFemales", "DistCode", c => c.Int(nullable: false));
            AddColumn("dbo.SelectedFemales", "TalukaCode", c => c.Int(nullable: false));
            AddColumn("dbo.SelectedFemales", "Component", c => c.Int(nullable: false));
            AddColumn("dbo.SelectedGenerals", "DistCode", c => c.Int(nullable: false));
            AddColumn("dbo.SelectedGenerals", "TalukaCode", c => c.Int(nullable: false));
            AddColumn("dbo.SelectedGenerals", "Component", c => c.Int(nullable: false));
            AddColumn("dbo.SelectedHandicappeds", "DistCode", c => c.Int(nullable: false));
            AddColumn("dbo.SelectedHandicappeds", "TalukaCode", c => c.Int(nullable: false));
            AddColumn("dbo.SelectedHandicappeds", "Component", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SelectedHandicappeds", "Component");
            DropColumn("dbo.SelectedHandicappeds", "TalukaCode");
            DropColumn("dbo.SelectedHandicappeds", "DistCode");
            DropColumn("dbo.SelectedGenerals", "Component");
            DropColumn("dbo.SelectedGenerals", "TalukaCode");
            DropColumn("dbo.SelectedGenerals", "DistCode");
            DropColumn("dbo.SelectedFemales", "Component");
            DropColumn("dbo.SelectedFemales", "TalukaCode");
            DropColumn("dbo.SelectedFemales", "DistCode");
        }
    }
}
