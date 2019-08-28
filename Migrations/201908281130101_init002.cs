namespace Mahamesh.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init002 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Comp1PhysicalTarget", newName: "Comp1Target");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.Comp1Target", newName: "Comp1PhysicalTarget");
        }
    }
}
