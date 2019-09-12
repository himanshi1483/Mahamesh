namespace Mahamesh.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init411 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ApplicantRegistrations", "FolderId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ApplicantRegistrations", "FolderId");
        }
    }
}
