namespace Mahamesh.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init00171 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DistrictCountdowns",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DistCode = c.Int(nullable: false),
                        EnableDate = c.DateTime(),
                        EnableTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.DistrictCountdowns");
        }
    }
}
