namespace Mahamesh.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init8 : DbMigration
    {
        public override void Up()
        {
            //AlterColumn("dbo.MediaGalleryModels", "MediaType", c => c.String());
        }
        
        public override void Down()
        {
            //AlterColumn("dbo.MediaGalleryModels", "MediaType", c => c.Int(nullable: false));
        }
    }
}
