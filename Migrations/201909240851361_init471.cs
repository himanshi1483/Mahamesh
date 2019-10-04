namespace Mahamesh.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init471 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.ApplicantDocuments", newName: "LDOConditions");
            AddColumn("dbo.LDOConditions", "ConditionNo", c => c.Int(nullable: false));
            AddColumn("dbo.LDOConditions", "ApprovalCondition", c => c.String());
            AddColumn("dbo.LDOConditions", "ApprovalValue", c => c.String());
            AddColumn("dbo.LDOConditions", "DocName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.LDOConditions", "DocName");
            DropColumn("dbo.LDOConditions", "ApprovalValue");
            DropColumn("dbo.LDOConditions", "ApprovalCondition");
            DropColumn("dbo.LDOConditions", "ConditionNo");
            RenameTable(name: "dbo.LDOConditions", newName: "ApplicantDocuments");
        }
    }
}
