namespace Mahamesh.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init41 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ApplicantRegistrations", "TrainingCertificate", c => c.String());
            AddColumn("dbo.ApplicantRegistrations", "ShedCertificate", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ApplicantRegistrations", "ShedCertificate");
            DropColumn("dbo.ApplicantRegistrations", "TrainingCertificate");
        }
    }
}
