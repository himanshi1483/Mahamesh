namespace Mahamesh.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init461 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PreliminaryLists", "LDO_ComponentApproved", c => c.Boolean(nullable: false));
            AddColumn("dbo.PreliminaryLists", "LDO_ComponentRemarks", c => c.String());
            AddColumn("dbo.PreliminaryLists", "LDOName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PreliminaryLists", "LDOName");
            DropColumn("dbo.PreliminaryLists", "LDO_ComponentRemarks");
            DropColumn("dbo.PreliminaryLists", "LDO_ComponentApproved");
        }
    }
}
