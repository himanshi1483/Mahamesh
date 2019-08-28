namespace Mahamesh.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init0071 : DbMigration
    {
        public override void Up()
        {
            //DropPrimaryKey("dbo.Comp1TalukaTarget");
            //DropPrimaryKey("dbo.CompTarget2");
            //DropPrimaryKey("dbo.Comp2TargetTaluka");
            //DropPrimaryKey("dbo.Comp3TargetTaluka");
            //DropPrimaryKey("dbo.Comp4TargetTaluka");
            //AlterColumn("dbo.Comp1TalukaTarget", "SrNo", c => c.Int(nullable: false, identity: true));
            //AlterColumn("dbo.CompTarget2", "SrNo", c => c.Int(nullable: false, identity: true));
            //AlterColumn("dbo.Comp2TargetTaluka", "SrNo", c => c.Int(nullable: false, identity: true));
            //AlterColumn("dbo.Comp3TargetTaluka", "SrNo", c => c.Int(nullable: false, identity: true));
            //AlterColumn("dbo.Comp4TargetTaluka", "SrNo", c => c.Int(nullable: false, identity: true));
            //AddPrimaryKey("dbo.Comp1TalukaTarget", "SrNo");
            //AddPrimaryKey("dbo.CompTarget2", "SrNo");
            //AddPrimaryKey("dbo.Comp2TargetTaluka", "SrNo");
            //AddPrimaryKey("dbo.Comp3TargetTaluka", "SrNo");
            //AddPrimaryKey("dbo.Comp4TargetTaluka", "SrNo");
            //DropColumn("dbo.Comp1TalukaTarget", "Id");
            //DropColumn("dbo.CompTarget2", "Id");
            //DropColumn("dbo.Comp2TargetTaluka", "Id");
            //DropColumn("dbo.Comp3TargetTaluka", "Id");
            //DropColumn("dbo.Comp4TargetTaluka", "Id");
        }
        
        public override void Down()
        {
            //AddColumn("dbo.Comp4TargetTaluka", "Id", c => c.Int(nullable: false, identity: true));
            //AddColumn("dbo.Comp3TargetTaluka", "Id", c => c.Int(nullable: false, identity: true));
            //AddColumn("dbo.Comp2TargetTaluka", "Id", c => c.Int(nullable: false, identity: true));
            //AddColumn("dbo.CompTarget2", "Id", c => c.Int(nullable: false, identity: true));
            //AddColumn("dbo.Comp1TalukaTarget", "Id", c => c.Int(nullable: false, identity: true));
            //DropPrimaryKey("dbo.Comp4TargetTaluka");
            //DropPrimaryKey("dbo.Comp3TargetTaluka");
            //DropPrimaryKey("dbo.Comp2TargetTaluka");
            //DropPrimaryKey("dbo.CompTarget2");
            //DropPrimaryKey("dbo.Comp1TalukaTarget");
            //AlterColumn("dbo.Comp4TargetTaluka", "SrNo", c => c.Int(nullable: false));
            //AlterColumn("dbo.Comp3TargetTaluka", "SrNo", c => c.Int(nullable: false));
            //AlterColumn("dbo.Comp2TargetTaluka", "SrNo", c => c.Int(nullable: false));
            //AlterColumn("dbo.CompTarget2", "SrNo", c => c.Int(nullable: false));
            //AlterColumn("dbo.Comp1TalukaTarget", "SrNo", c => c.Int(nullable: false));
            //AddPrimaryKey("dbo.Comp4TargetTaluka", "Id");
            //AddPrimaryKey("dbo.Comp3TargetTaluka", "Id");
            //AddPrimaryKey("dbo.Comp2TargetTaluka", "Id");
            //AddPrimaryKey("dbo.CompTarget2", "Id");
            //AddPrimaryKey("dbo.Comp1TalukaTarget", "Id");
        }
    }
}
