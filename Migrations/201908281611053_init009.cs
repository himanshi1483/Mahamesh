namespace Mahamesh.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init009 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DistMasters", "District_Mr", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.DistMasters", "District_Mr");
        }
    }
}
