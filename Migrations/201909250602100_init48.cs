namespace Mahamesh.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init48 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SelectedFemales", "UploadEnabled", c => c.Boolean(nullable: false));
            AddColumn("dbo.SelectedGenerals", "UploadEnabled", c => c.Boolean(nullable: false));
            AddColumn("dbo.SelectedHandicappeds", "UploadEnabled", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SelectedHandicappeds", "UploadEnabled");
            DropColumn("dbo.SelectedGenerals", "UploadEnabled");
            DropColumn("dbo.SelectedFemales", "UploadEnabled");
        }
    }
}
