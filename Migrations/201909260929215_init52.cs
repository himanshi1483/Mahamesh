namespace Mahamesh.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init52 : DbMigration
    {
        public override void Up()
        {
            //CreateTable(
            //    "dbo.BeneficiarySelectedList2018",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            Comp_No = c.Int(nullable: false),
            //            Dist = c.String(),
            //            Taluka = c.String(),
            //            Village = c.String(),
            //            App_Id = c.Int(nullable: false),
            //            ApplicantName = c.String(),
            //            Ph = c.String(),
            //            DOB = c.Int(nullable: false),
            //            Age = c.Int(nullable: false),
            //            Gender = c.String(),
            //            Caste = c.String(),
            //            Subcaste = c.String(),
            //            No_Of_Sheep = c.Int(nullable: false),
            //            Aadhar = c.Long(nullable: false),
            //            Crippled = c.String(),
            //            Cripple_Percent = c.Int(nullable: false),
            //            Bachat_Gat = c.String(),
            //            SavingAcName = c.String(),
            //            Reg_No = c.String(),
            //            Company = c.String(),
            //        })
            //    .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
           // DropTable("dbo.BeneficiarySelectedList2018");
        }
    }
}
