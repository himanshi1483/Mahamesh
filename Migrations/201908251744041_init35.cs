namespace Mahamesh.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init35 : DbMigration
    {
        public override void Up()
        {
           // DropColumn("dbo.ApplicantRegistrations", "CompNumber");
        }
        
        public override void Down()
        {
            //AddColumn("dbo.ApplicantRegistrations", "CompNumber", c => c.String());
        }
    }
}
