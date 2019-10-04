namespace Mahamesh.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init450 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PreliminaryLists", "SavedByLDO", c => c.Boolean(nullable: false));
            AddColumn("dbo.PreliminaryLists", "SavedByDAHO", c => c.Boolean(nullable: false));
            AddColumn("dbo.PreliminaryLists", "SavedByDDC", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PreliminaryLists", "SavedByDDC");
            DropColumn("dbo.PreliminaryLists", "SavedByDAHO");
            DropColumn("dbo.PreliminaryLists", "SavedByLDO");
        }
    }
}
