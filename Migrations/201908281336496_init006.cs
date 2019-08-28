namespace Mahamesh.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init006 : DbMigration
    {
        public override void Up()
        {
            //RenameTable(name: "dbo.Comp1PhysicalTargetTaluka", newName: "Comp1TalukaTarget");
            //RenameTable(name: "dbo.Comp2PhysicalTarget", newName: "CompTarget2");
            //RenameTable(name: "dbo.Comp2PhysicalTargetTaluka", newName: "Comp2TargetTaluka");
            //RenameTable(name: "dbo.Comp3PhysicalTargetTaluka", newName: "Comp3TargetTaluka");
            //RenameTable(name: "dbo.Comp4PhysicalTargetTaluka", newName: "Comp4TargetTaluka");
        }
        
        public override void Down()
        {
            //RenameTable(name: "dbo.Comp4TargetTaluka", newName: "Comp4PhysicalTargetTaluka");
            //RenameTable(name: "dbo.Comp3TargetTaluka", newName: "Comp3PhysicalTargetTaluka");
            //RenameTable(name: "dbo.Comp2TargetTaluka", newName: "Comp2PhysicalTargetTaluka");
            //RenameTable(name: "dbo.CompTarget2", newName: "Comp2PhysicalTarget");
            //RenameTable(name: "dbo.Comp1TalukaTarget", newName: "Comp1PhysicalTargetTaluka");
        }
    }
}
