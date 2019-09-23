namespace Mahamesh.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init422 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ApplicantDocuments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ApplicantID = c.Int(nullable: false),
                        AadharNumber = c.Long(),
                        DistCode = c.Int(nullable: false),
                        TalukaCode = c.Int(nullable: false),
                        DocNumber = c.Int(nullable: false),
                        GoogleDocID = c.String(),
                        LDOApproved = c.Boolean(nullable: false),
                        LDORemarks = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.PreliminaryLists", "LDORecommended", c => c.Boolean(nullable: false));
            AddColumn("dbo.PreliminaryLists", "LDORemarks", c => c.String());
            AddColumn("dbo.PreliminaryLists", "DAHORecommended", c => c.Boolean(nullable: false));
            AddColumn("dbo.PreliminaryLists", "DAHORemarks", c => c.String());
            AddColumn("dbo.PreliminaryLists", "DDCRecommended", c => c.Boolean(nullable: false));
            AddColumn("dbo.PreliminaryLists", "DDCRemarks", c => c.String());
            AddColumn("dbo.PreliminaryLists", "SGDCRecommended", c => c.Boolean(nullable: false));
            AddColumn("dbo.PreliminaryLists", "SGDCRemarks", c => c.String());
            DropColumn("dbo.PreliminaryLists", "DocumentVerified");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PreliminaryLists", "DocumentVerified", c => c.Boolean(nullable: false));
            DropColumn("dbo.PreliminaryLists", "SGDCRemarks");
            DropColumn("dbo.PreliminaryLists", "SGDCRecommended");
            DropColumn("dbo.PreliminaryLists", "DDCRemarks");
            DropColumn("dbo.PreliminaryLists", "DDCRecommended");
            DropColumn("dbo.PreliminaryLists", "DAHORemarks");
            DropColumn("dbo.PreliminaryLists", "DAHORecommended");
            DropColumn("dbo.PreliminaryLists", "LDORemarks");
            DropColumn("dbo.PreliminaryLists", "LDORecommended");
            DropTable("dbo.ApplicantDocuments");
        }
    }
}
