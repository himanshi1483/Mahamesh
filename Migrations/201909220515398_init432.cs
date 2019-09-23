namespace Mahamesh.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init432 : DbMigration
    {
        public override void Up()
        {
          //  AddColumn("dbo.ApplicantRegistrations", "OldId", c => c.Int(nullable: false));
            AddColumn("dbo.SelectedGenerals", "IsValid", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.SelectedGenerals", "IsValid");
           // DropColumn("dbo.ApplicantRegistrations", "OldId");
        }
    }
}
