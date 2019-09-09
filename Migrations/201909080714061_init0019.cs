namespace Mahamesh.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init0019 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SelectedFemales", "District_Mr", c => c.String());
            AddColumn("dbo.SelectedFemales", "Taluka_Mr", c => c.String());
            AddColumn("dbo.SelectedFemales", "VillageName", c => c.String());
            AddColumn("dbo.SelectedFemales", "PhNo", c => c.String());
            AddColumn("dbo.SelectedFemales", "AadharNo", c => c.Long(nullable: false));
            AddColumn("dbo.SelectedFemales", "Gender", c => c.String());
            AddColumn("dbo.SelectedFemales", "ApplicantCrippled", c => c.String());
            AddColumn("dbo.SelectedGenerals", "District_Mr", c => c.String());
            AddColumn("dbo.SelectedGenerals", "Taluka_Mr", c => c.String());
            AddColumn("dbo.SelectedGenerals", "VillageName", c => c.String());
            AddColumn("dbo.SelectedGenerals", "PhNo", c => c.String());
            AddColumn("dbo.SelectedGenerals", "AadharNo", c => c.Long(nullable: false));
            AddColumn("dbo.SelectedGenerals", "Gender", c => c.String());
            AddColumn("dbo.SelectedGenerals", "ApplicantCrippled", c => c.String());
            AddColumn("dbo.SelectedHandicappeds", "District_Mr", c => c.String());
            AddColumn("dbo.SelectedHandicappeds", "Taluka_Mr", c => c.String());
            AddColumn("dbo.SelectedHandicappeds", "VillageName", c => c.String());
            AddColumn("dbo.SelectedHandicappeds", "PhNo", c => c.String());
            AddColumn("dbo.SelectedHandicappeds", "AadharNo", c => c.Long(nullable: false));
            AddColumn("dbo.SelectedHandicappeds", "Gender", c => c.String());
            AddColumn("dbo.SelectedHandicappeds", "ApplicantCrippled", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.SelectedHandicappeds", "ApplicantCrippled");
            DropColumn("dbo.SelectedHandicappeds", "Gender");
            DropColumn("dbo.SelectedHandicappeds", "AadharNo");
            DropColumn("dbo.SelectedHandicappeds", "PhNo");
            DropColumn("dbo.SelectedHandicappeds", "VillageName");
            DropColumn("dbo.SelectedHandicappeds", "Taluka_Mr");
            DropColumn("dbo.SelectedHandicappeds", "District_Mr");
            DropColumn("dbo.SelectedGenerals", "ApplicantCrippled");
            DropColumn("dbo.SelectedGenerals", "Gender");
            DropColumn("dbo.SelectedGenerals", "AadharNo");
            DropColumn("dbo.SelectedGenerals", "PhNo");
            DropColumn("dbo.SelectedGenerals", "VillageName");
            DropColumn("dbo.SelectedGenerals", "Taluka_Mr");
            DropColumn("dbo.SelectedGenerals", "District_Mr");
            DropColumn("dbo.SelectedFemales", "ApplicantCrippled");
            DropColumn("dbo.SelectedFemales", "Gender");
            DropColumn("dbo.SelectedFemales", "AadharNo");
            DropColumn("dbo.SelectedFemales", "PhNo");
            DropColumn("dbo.SelectedFemales", "VillageName");
            DropColumn("dbo.SelectedFemales", "Taluka_Mr");
            DropColumn("dbo.SelectedFemales", "District_Mr");
        }
    }
}
