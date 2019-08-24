namespace Mahamesh.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init28 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ApplicantRegistrations", "FormSubmitted", c => c.Boolean(nullable: false));
            DropColumn("dbo.ApplicantRegistrations", "ApplicationID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ApplicantRegistrations", "ApplicationID", c => c.Int(nullable: false));
            DropColumn("dbo.ApplicantRegistrations", "FormSubmitted");
        }
    }
}
