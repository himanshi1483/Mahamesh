namespace Mahamesh.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init008 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DistrictLists", "District_Mr", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.DistrictLists", "District_Mr");
        }
    }
}
