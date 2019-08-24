namespace Mahamesh.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init31 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ApplicantRegistrations", "ApplicationNumber", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ApplicantRegistrations", "ApplicationNumber");
        }
    }
}
