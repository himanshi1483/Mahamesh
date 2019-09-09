namespace Mahamesh.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init0020 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SelectedFemales", "Name", c => c.String());
            AddColumn("dbo.SelectedGenerals", "Name", c => c.String());
            AddColumn("dbo.SelectedHandicappeds", "Name", c => c.String());
            AlterColumn("dbo.SelectedFemales", "PhNo", c => c.Long());
            AlterColumn("dbo.SelectedGenerals", "PhNo", c => c.Long());
            AlterColumn("dbo.SelectedHandicappeds", "PhNo", c => c.Long());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.SelectedHandicappeds", "PhNo", c => c.String());
            AlterColumn("dbo.SelectedGenerals", "PhNo", c => c.String());
            AlterColumn("dbo.SelectedFemales", "PhNo", c => c.String());
            DropColumn("dbo.SelectedHandicappeds", "Name");
            DropColumn("dbo.SelectedGenerals", "Name");
            DropColumn("dbo.SelectedFemales", "Name");
        }
    }
}
