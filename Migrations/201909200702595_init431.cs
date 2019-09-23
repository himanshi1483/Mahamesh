namespace Mahamesh.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init431 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PreliminaryLists", "LDOSubmitDate", c => c.DateTime());
            AddColumn("dbo.PreliminaryLists", "LDO_Ip", c => c.String());
            AddColumn("dbo.PreliminaryLists", "DAHOSubmitDate", c => c.DateTime());
            AddColumn("dbo.PreliminaryLists", "DAHO_Ip", c => c.String());
            AddColumn("dbo.PreliminaryLists", "DDCSubmitDate", c => c.DateTime());
            AddColumn("dbo.PreliminaryLists", "DDC_Ip", c => c.String());
            AddColumn("dbo.PreliminaryLists", "SGDCSubmitDate", c => c.DateTime());
            AddColumn("dbo.PreliminaryLists", "SGDC_Ip", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PreliminaryLists", "SGDC_Ip");
            DropColumn("dbo.PreliminaryLists", "SGDCSubmitDate");
            DropColumn("dbo.PreliminaryLists", "DDC_Ip");
            DropColumn("dbo.PreliminaryLists", "DDCSubmitDate");
            DropColumn("dbo.PreliminaryLists", "DAHO_Ip");
            DropColumn("dbo.PreliminaryLists", "DAHOSubmitDate");
            DropColumn("dbo.PreliminaryLists", "LDO_Ip");
            DropColumn("dbo.PreliminaryLists", "LDOSubmitDate");
        }
    }
}
