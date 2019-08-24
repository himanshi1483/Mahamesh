namespace Mahamesh.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init29 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ApplicantRegistrations", "UserIP", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ApplicantRegistrations", "UserIP");
        }
    }
}
