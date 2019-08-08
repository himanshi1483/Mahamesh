namespace Mahamesh.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.NewsModels", "UpdatedDate", c => c.DateTime());
            AlterColumn("dbo.PressInformationModels", "UpdatedDate", c => c.DateTime());
            AlterColumn("dbo.TenderModels", "UpdatedDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TenderModels", "UpdatedDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.PressInformationModels", "UpdatedDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.NewsModels", "UpdatedDate", c => c.DateTime(nullable: false));
        }
    }
}
