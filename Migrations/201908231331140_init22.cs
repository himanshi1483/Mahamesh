namespace Mahamesh.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init22 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.DistMasters", "Dist_Code", c => c.Int(nullable: false));
            AlterColumn("dbo.TalMasters", "Tal_Code", c => c.Int(nullable: false));
            AlterColumn("dbo.TalMasters", "Dist_Code", c => c.Int(nullable: false));
            AlterColumn("dbo.VillageMasters", "Village_Code", c => c.Int(nullable: false));
            AlterColumn("dbo.VillageMasters", "Tal_Code", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.VillageMasters", "Tal_Code", c => c.Double(nullable: false));
            AlterColumn("dbo.VillageMasters", "Village_Code", c => c.Double(nullable: false));
            AlterColumn("dbo.TalMasters", "Dist_Code", c => c.Double(nullable: false));
            AlterColumn("dbo.TalMasters", "Tal_Code", c => c.Double(nullable: false));
            AlterColumn("dbo.DistMasters", "Dist_Code", c => c.Double(nullable: false));
        }
    }
}
