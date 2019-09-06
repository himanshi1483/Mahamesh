namespace Mahamesh.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init0016 : DbMigration
    {
        public override void Up()
        {
            //CreateTable(
            //    "dbo.DistrictTargets",
            //    c => new
            //        {
            //            SrNo = c.Int(nullable: false, identity: true),
            //            Name_of_District = c.String(),
            //            Component_No_1 = c.Int(nullable: false),
            //            Component_No_2 = c.Int(nullable: false),
            //            Component_No_3_7 = c.Int(nullable: false),
            //            Component_No_8 = c.Int(nullable: false),
            //            Component_No_9 = c.Int(nullable: false),
            //            Component_No_10 = c.Int(nullable: false),
            //            Component_No_11 = c.Int(nullable: false),
            //            Component_No_12 = c.Int(nullable: false),
            //            Component_No_13 = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => t.SrNo);
            
            //CreateTable(
            //    "dbo.TalukaTargets",
            //    c => new
            //        {
            //            SrNo = c.Int(nullable: false, identity: true),
            //            Name_of_District = c.String(),
            //            Name_Of_Taluka = c.String(),
            //            Component_No_1 = c.Int(nullable: false),
            //            Component_No_2 = c.Int(nullable: false),
            //            Component_No_3_7 = c.Int(nullable: false),
            //            Component_No_8 = c.Int(nullable: false),
            //            Component_No_9 = c.Int(nullable: false),
            //            Component_No_10 = c.Int(nullable: false),
            //            Component_No_11 = c.Int(nullable: false),
            //            Component_No_12 = c.Int(nullable: false),
            //            Component_No_13 = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => t.SrNo);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.TalukaTargets");
            DropTable("dbo.DistrictTargets");
        }
    }
}
