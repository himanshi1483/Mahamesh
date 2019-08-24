namespace Mahamesh.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init26 : DbMigration
    {
        public override void Up()
        {
            //CreateTable(
            //    "dbo.AcreMasters",
            //    c => new
            //        {
            //            id = c.Int(nullable: false, identity: true),
            //            Acre = c.Int(),
            //        })
            //    .PrimaryKey(t => t.id);
            
            //CreateTable(
            //    "dbo.DurationWaterAvailableForIrrigations",
            //    c => new
            //        {
            //            ID = c.Int(nullable: false, identity: true),
            //            DurationName = c.String(),
            //        })
            //    .PrimaryKey(t => t.ID);
            
            //CreateTable(
            //    "dbo.TypeExistingCastles",
            //    c => new
            //        {
            //            ID = c.Int(nullable: false, identity: true),
            //            TypeName = c.String(),
            //        })
            //    .PrimaryKey(t => t.ID);
            
            //CreateTable(
            //    "dbo.WaterSources",
            //    c => new
            //        {
            //            ID = c.Int(nullable: false, identity: true),
            //            SourceName = c.String(),
            //        })
            //    .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            //DropTable("dbo.WaterSources");
            //DropTable("dbo.TypeExistingCastles");
            //DropTable("dbo.DurationWaterAvailableForIrrigations");
            //DropTable("dbo.AcreMasters");
        }
    }
}
