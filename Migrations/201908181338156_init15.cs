namespace Mahamesh.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init15 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MediaFolders", "MediaType", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.MediaFolders", "MediaType");
        }
    }
}
