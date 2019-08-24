namespace Mahamesh.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init30 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ApplicantRegistrations", "SubmitDatetime", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ApplicantRegistrations", "SubmitDatetime");
        }
    }
}
