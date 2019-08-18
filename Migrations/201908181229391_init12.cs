namespace Mahamesh.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init12 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MediaGalleryModels", "MediaFolder", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.MediaGalleryModels", "MediaFolder");
        }
    }
}
