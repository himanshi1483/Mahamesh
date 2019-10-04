namespace Mahamesh.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init46 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ApplicantRegistrations", "DocReceiptGenerated", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ApplicantRegistrations", "DocReceiptGenerated");
        }
    }
}
