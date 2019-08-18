namespace Mahamesh.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init14 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MediaFolders",
                c => new
                    {
                        FolderId = c.Int(nullable: false, identity: true),
                        FolderName = c.String(),
                    })
                .PrimaryKey(t => t.FolderId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.MediaFolders");
        }
    }
}
