namespace Mahamesh.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init4 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MediaGalleryModels",
                c => new
                    {
                        MediaId = c.Int(nullable: false, identity: true),
                        MediaType = c.String(),
                        MediaName = c.String(),
                        MediaLocation = c.String(),
                        Caption = c.String(),
                        CreatedBy = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedBy = c.String(),
                        UpdatedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.MediaId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.MediaGalleryModels");
        }
    }
}
