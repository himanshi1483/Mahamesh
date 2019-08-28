namespace Mahamesh.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init21 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.DistMasters");
            AlterColumn("dbo.DistMasters", "SrNo", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.DistMasters", "SrNo");
            DropColumn("dbo.DistMasters", "Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.DistMasters", "Id", c => c.Int(nullable: false, identity: true));
            DropPrimaryKey("dbo.DistMasters");
            AlterColumn("dbo.DistMasters", "SrNo", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.DistMasters", "Id");
        }
    }
}
