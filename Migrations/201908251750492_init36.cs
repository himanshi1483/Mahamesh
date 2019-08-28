namespace Mahamesh.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init36 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ApplicantRegistrations", "CompNumber", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ApplicantRegistrations", "CompNumber");
        }
    }
}
