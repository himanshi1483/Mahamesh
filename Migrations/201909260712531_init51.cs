namespace Mahamesh.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init51 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.PreliminaryLists", "LDORecommended", c => c.String());
            AlterColumn("dbo.PreliminaryLists", "DAHORecommended", c => c.String());
            AlterColumn("dbo.PreliminaryLists", "DDCRecommended", c => c.String());
            AlterColumn("dbo.PreliminaryLists", "SGDCRecommended", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PreliminaryLists", "SGDCRecommended", c => c.Boolean(nullable: false));
            AlterColumn("dbo.PreliminaryLists", "DDCRecommended", c => c.Boolean(nullable: false));
            AlterColumn("dbo.PreliminaryLists", "DAHORecommended", c => c.Boolean(nullable: false));
            AlterColumn("dbo.PreliminaryLists", "LDORecommended", c => c.Boolean(nullable: false));
        }
    }
}
