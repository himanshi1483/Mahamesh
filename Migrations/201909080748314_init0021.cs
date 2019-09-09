namespace Mahamesh.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init0021 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SelectedFemales", "CreatedOn", c => c.DateTime(nullable: false));
            AddColumn("dbo.SelectedGenerals", "CreatedOn", c => c.DateTime(nullable: false));
            AddColumn("dbo.SelectedHandicappeds", "CreatedOn", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SelectedHandicappeds", "CreatedOn");
            DropColumn("dbo.SelectedGenerals", "CreatedOn");
            DropColumn("dbo.SelectedFemales", "CreatedOn");
        }
    }
}
