namespace Mahamesh.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init0091 : DbMigration
    {
        public override void Up()
        {
            //CreateTable(
            //    "dbo.OfficerLogins",
            //    c => new
            //    {
            //        ID = c.Int(nullable: false, identity: true),
            //        designation = c.String(),
            //        districy = c.Int(nullable: false),
            //        taluka = c.Int(nullable: false),
            //        Username = c.String(),
            //        pwd = c.String(),
            //        ResetPwd = c.String(),
            //    })
            //    .PrimaryKey(t => t.ID);

        }
        
        public override void Down()
        {
            //DropTable("dbo.OfficerLogins");
        }
    }
}
