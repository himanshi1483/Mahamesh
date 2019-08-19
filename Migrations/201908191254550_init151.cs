namespace Mahamesh.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init151 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.NewsModels", "DocumentName", c => c.String());
            AddColumn("dbo.PressInformationModels", "DocumentName", c => c.String());
            AddColumn("dbo.TenderModels", "DocumentName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.TenderModels", "DocumentName");
            DropColumn("dbo.PressInformationModels", "DocumentName");
            DropColumn("dbo.NewsModels", "DocumentName");
        }
    }
}
