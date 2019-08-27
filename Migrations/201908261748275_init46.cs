namespace Mahamesh.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init46 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comp1PhysicalTarget",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    SrNo = c.Int(nullable: false),
                    ComponentNumber = c.Int(nullable: false),
                    DistrictName = c.String(),
                    NoOfSheep = c.Long(nullable: false),
                    PercentageOfSheep = c.Decimal(nullable: false, precision: 18, scale: 2),
                    PermOrigin = c.Int(nullable: false),
                    PermGrant = c.Double(nullable: false),
                })
                .PrimaryKey(t => t.Id);
            CreateTable(
                "dbo.Comp1PhysicalTargetTaluka",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SrNo = c.Int(nullable: false),
                        ComponentNumber = c.Int(nullable: false),
                        DistrictName = c.String(),
                        TalukaName = c.String(),
                        NoOfSheep = c.Long(nullable: false),
                        PercentageOfSheep = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PermOrigin = c.Int(nullable: false),
                        TempOrigin = c.Int(nullable: false),
                        PermGrant = c.Double(nullable: false),
                        TempGrant = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Comp2PhysicalTarget",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SrNo = c.Int(nullable: false),
                        ComponentNumber = c.Int(nullable: false),
                        DistrictName = c.String(),
                        NoOfSheep = c.Long(nullable: false),
                        PercentageOfSheep = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PermOrigin = c.Int(nullable: false),
                        PermGrant = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Comp2PhysicalTargetTaluka",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SrNo = c.Int(nullable: false),
                        ComponentNumber = c.Int(nullable: false),
                        DistrictName = c.String(),
                        TalukaName = c.String(),
                        NoOfSheep = c.Long(nullable: false),
                        PercentageOfSheep = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PermOrigin = c.Int(nullable: false),
                        PermGrant = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Comp3PhysicalTarget",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SrNo = c.Int(nullable: false),
                        ComponentNumber = c.Int(nullable: false),
                        DistrictName = c.String(),
                        NoOfSheep = c.Long(nullable: false),
                        PercentageOfSheep = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PermBeneficiary1Origin = c.Int(nullable: false),
                        PermBeneficiary2Origin = c.Int(nullable: false),
                        TempBeneficiary1Origin = c.Int(nullable: false),
                        TempBeneficiary2Origin = c.Int(nullable: false),
                        PermGrantBeneficiary1 = c.Double(nullable: false),
                        PermGrantBeneficiary2 = c.Double(nullable: false),
                        TempGrantBeneficiary1 = c.Double(nullable: false),
                        TempGrantBeneficiary2 = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Comp3PhysicalTargetTaluka",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SrNo = c.Int(nullable: false),
                        ComponentNumber = c.Int(nullable: false),
                        DistrictName = c.String(),
                        TalukaName = c.String(),
                        NoOfSheep = c.Long(nullable: false),
                        PercentageOfSheep = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PermBeneficiary1Origin = c.Int(nullable: false),
                        PermBeneficiary2Origin = c.Int(nullable: false),
                        TempBeneficiary1Origin = c.Int(nullable: false),
                        TempBeneficiary2Origin = c.Int(nullable: false),
                        PermGrantBeneficiary1 = c.Double(nullable: false),
                        PermGrantBeneficiary2 = c.Double(nullable: false),
                        TempGrantBeneficiary1 = c.Double(nullable: false),
                        TempGrantBeneficiary2 = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Comp4PhysicalTarget",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SrNo = c.Int(nullable: false),
                        ComponentNumber = c.Int(nullable: false),
                        DistrictName = c.String(),
                        NoOfSheep = c.Long(nullable: false),
                        PercentageOfSheep = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PermOrigin = c.Int(nullable: false),
                        TempOrigin = c.Int(nullable: false),
                        PermGrant = c.Double(nullable: false),
                        TempGrant = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Comp4PhysicalTargetTaluka",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SrNo = c.Int(nullable: false),
                        ComponentNumber = c.Int(nullable: false),
                        DistrictName = c.String(),
                        TalukaName = c.String(),
                        NoOfSheep = c.Long(nullable: false),
                        PercentageOfSheep = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PermOrigin = c.Int(nullable: false),
                        TempOrigin = c.Int(nullable: false),
                        PermGrant = c.Double(nullable: false),
                        TempGrant = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Comp4PhysicalTargetTaluka");
            DropTable("dbo.Comp4PhysicalTarget");
            DropTable("dbo.Comp3PhysicalTargetTaluka");
            DropTable("dbo.Comp3PhysicalTarget");
            DropTable("dbo.Comp2PhysicalTargetTaluka");
            DropTable("dbo.Comp2PhysicalTarget");
            DropTable("dbo.Comp1PhysicalTargetTaluka");
            DropTable("dbo.Comp1PhysicalTarget");
        }
    }
}
