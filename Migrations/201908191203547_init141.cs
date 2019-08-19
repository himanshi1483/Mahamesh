namespace Mahamesh.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init141 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.NewsModels", "NewsDocument", c => c.String());
            DropColumn("dbo.NewsModels", "ImageLocation");
        }
        
        public override void Down()
        {
            AddColumn("dbo.NewsModels", "ImageLocation", c => c.String());
            DropColumn("dbo.NewsModels", "NewsDocument");
        }
    }
}
