namespace Mahamesh.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init20 : DbMigration
    {
        public override void Up()
        {
            //DropPrimaryKey("dbo.DistrictLists");
            //CreateTable(
            //    "dbo.DistMasters",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            SrNo = c.Int(nullable: false),
            //            DistName = c.String(),
            //            Dist_Code = c.Double(nullable: false),
            //        })
            //    .PrimaryKey(t => t.Id);
            
            //CreateTable(
            //    "dbo.TalMasters",
            //    c => new
            //        {
            //            SrNo = c.Int(nullable: false, identity: true),
            //            TalName = c.String(),
            //            Tal_Code = c.Double(nullable: false),
            //            Dist_Code = c.Double(nullable: false),
            //        })
            //    .PrimaryKey(t => t.SrNo);
            
            //CreateTable(
            //    "dbo.VillageMasters",
            //    c => new
            //        {
            //            SrNo = c.Int(nullable: false, identity: true),
            //            VillageName = c.String(),
            //            Village_Code = c.Double(nullable: false),
            //            Tal_Code = c.Double(nullable: false),
            //        })
            //    .PrimaryKey(t => t.SrNo);
            
            //AlterColumn("dbo.DistrictLists", "SrNo", c => c.Int(nullable: false, identity: true));
            //AddPrimaryKey("dbo.DistrictLists", "SrNo");
            //DropColumn("dbo.DistrictLists", "Id");
        }
        
        public override void Down()
        {
            //AddColumn("dbo.DistrictLists", "Id", c => c.Int(nullable: false, identity: true));
            //DropPrimaryKey("dbo.DistrictLists");
            //AlterColumn("dbo.DistrictLists", "SrNo", c => c.Int(nullable: false));
            //DropTable("dbo.VillageMasters");
            //DropTable("dbo.TalMasters");
            //DropTable("dbo.DistMasters");
            //AddPrimaryKey("dbo.DistrictLists", "Id");
        }
    }
}
