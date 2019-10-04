namespace Mahamesh.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init45 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ApplicantRegistrations", "OldId", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ApplicantRegistrations", "OldId", c => c.Int(nullable: false));
        }
    }
}
