namespace Mahamesh.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init24 : DbMigration
    {
        public override void Up()
        {
            //AlterColumn("dbo.CrippledMasters", "Percentage", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            //AlterColumn("dbo.CrippledMasters", "Percentage", c => c.String());
        }
    }
}
