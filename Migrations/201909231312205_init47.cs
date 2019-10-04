namespace Mahamesh.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init47 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MasterRoles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MasterUserName = c.String(),
                        MasterPassword = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.MasterRoles");
        }
    }
}
