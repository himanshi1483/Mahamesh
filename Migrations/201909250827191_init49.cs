namespace Mahamesh.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init49 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PreliminaryLists", "Component", c => c.Int(nullable: false));
            AddColumn("dbo.PreliminaryLists", "DAHOName", c => c.String());
            AddColumn("dbo.PreliminaryLists", "DDCName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PreliminaryLists", "DDCName");
            DropColumn("dbo.PreliminaryLists", "DAHOName");
            DropColumn("dbo.PreliminaryLists", "Component");
        }
    }
}
