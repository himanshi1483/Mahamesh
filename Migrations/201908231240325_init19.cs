namespace Mahamesh.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init19 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DistrictLists", "DistName", c => c.String());
            DropColumn("dbo.DistrictLists", "DistrictName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.DistrictLists", "DistrictName", c => c.String());
            DropColumn("dbo.DistrictLists", "DistName");
        }
    }
}
