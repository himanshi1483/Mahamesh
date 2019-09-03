namespace Mahamesh.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init010 : DbMigration
    {
        public override void Up()
        {//
           // RenameTable(name: "dbo.OfficerLoginModels", newName: "OfficerLogins");
           // AddColumn("dbo.OfficerLogins", "district", c => c.Int(nullable: false));
            //DropColumn("dbo.OfficerLogins", "districy");
        }
        
        public override void Down()
        {
           /// AddColumn("dbo.OfficerLogins", "districy", c => c.Int(nullable: false));
           // DropColumn("dbo.OfficerLogins", "district");
           // RenameTable(name: "dbo.OfficerLogins", newName: "OfficerLoginModels");
        }
    }
}
