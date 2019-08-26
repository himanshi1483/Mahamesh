namespace Mahamesh.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init38 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ApplicationDurations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StartDatetime = c.DateTime(nullable: false),
                        EndDatetime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ApplicationDurations");
        }
    }
}
