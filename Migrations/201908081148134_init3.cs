namespace Mahamesh.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init3 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FeedbackModels",
                c => new
                    {
                        FeedbackId = c.Int(nullable: false, identity: true),
                        FeedbackTitle = c.String(),
                        FeedbackDescription = c.String(),
                        FeedbackDate = c.DateTime(),
                        Email = c.String(),
                        Name = c.String(),
                        PhoneNumber = c.String(),
                        CreatedBy = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedBy = c.String(),
                        UpdatedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.FeedbackId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.FeedbackModels");
        }
    }
}
