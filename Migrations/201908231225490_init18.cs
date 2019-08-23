namespace Mahamesh.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init18 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DistrictLists", "Dist_Code", c => c.Double(nullable: false));
            AddColumn("dbo.DistrictLists", "SrNo", c => c.Int(nullable: false));
            DropColumn("dbo.DistrictLists", "Tehsil");
            DropColumn("dbo.DistrictLists", "Village");
        }
        
        public override void Down()
        {
            AddColumn("dbo.DistrictLists", "Village", c => c.String());
            AddColumn("dbo.DistrictLists", "Tehsil", c => c.String());
            DropColumn("dbo.DistrictLists", "SrNo");
            DropColumn("dbo.DistrictLists", "Dist_Code");
        }
    }
}
