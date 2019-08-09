namespace Mahamesh.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init9 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.MediaGalleryModels", "MediaType", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.MediaGalleryModels", "MediaType", c => c.String());
        }
    }
}
