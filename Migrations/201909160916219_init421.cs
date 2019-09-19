namespace Mahamesh.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init421 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PreliminaryLists",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ApplicantID = c.Int(nullable: false),
                        AadharNumber = c.Long(),
                        DistCode = c.Int(nullable: false),
                        TalukaCode = c.Int(nullable: false),
                        DocumentVerified = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.PreliminaryLists");
        }
    }
}
