namespace Mahamesh.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init6 : DbMigration
    {
        public override void Up()
        {
            //DropForeignKey("dbo.MediaGalleryModels", "MediaGalleryModel_MediaId", "dbo.MediaGalleryModels");
            //DropIndex("dbo.MediaGalleryModels", new[] { "MediaGalleryModel_MediaId" });
            //DropColumn("dbo.MediaGalleryModels", "MediaGalleryModel_MediaId");
        }
        
        public override void Down()
        {
            //AddColumn("dbo.MediaGalleryModels", "MediaGalleryModel_MediaId", c => c.Int());
            //CreateIndex("dbo.MediaGalleryModels", "MediaGalleryModel_MediaId");
            //AddForeignKey("dbo.MediaGalleryModels", "MediaGalleryModel_MediaId", "dbo.MediaGalleryModels", "MediaId");
        }
    }
}
