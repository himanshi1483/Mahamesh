namespace Mahamesh.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init0074 : DbMigration
    {
        public override void Up()
        {
            //AlterColumn("dbo.Comp1Target", "PermOrigin", c => c.Int(nullable: false));
            //AlterColumn("dbo.Comp1Target", "TempOrigin", c => c.Int(nullable: false));
            //AlterColumn("dbo.Comp1Target", "PermGrant", c => c.Double(nullable: false));
            //AlterColumn("dbo.Comp1Target", "TempGrant", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            //AlterColumn("dbo.Comp1Target", "TempGrant", c => c.Long(nullable: false));
            //AlterColumn("dbo.Comp1Target", "PermGrant", c => c.Long(nullable: false));
            //AlterColumn("dbo.Comp1Target", "TempOrigin", c => c.Long(nullable: false));
            //AlterColumn("dbo.Comp1Target", "PermOrigin", c => c.Long(nullable: false));
        }
    }
}
