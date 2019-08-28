namespace Mahamesh.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init23 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CasteUnderNTCs",
                c => new
                {
                    ID = c.Int(nullable: false, identity: true),
                    Caste = c.String(),
                })
                .PrimaryKey(t => t.ID);

            CreateTable(
                "dbo.CrippledMasters",
                c => new
                {
                    ID = c.Int(nullable: false, identity: true),
                    Percentage = c.String(),
                })
                .PrimaryKey(t => t.ID);

            CreateTable(
                "dbo.NoOfSheepMasters",
                c => new
                {
                    id = c.Int(nullable: false, identity: true),
                    NoOfSheep = c.String(),
                })
                .PrimaryKey(t => t.id);

        }
        
        public override void Down()
        {
            DropTable("dbo.NoOfSheepMasters");
            DropTable("dbo.CrippledMasters");
            DropTable("dbo.CasteUnderNTCs");
        }
    }
}
