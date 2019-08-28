using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mahamesh.Models
{
    public class Comp1Target
    {
        [Key]
        public int Id { get; set; }
        public int SrNo { get; set; }

        public int ComponentNumber { get; set; }
        public string DistrictName { get; set; }
        public long NoOfSheep { get; set; }
        public decimal PercentageOfSheep { get; set; }
        public int PermOrigin { get; set; }
       public int TempOrigin { get; set; }
        public double PermGrant { get; set; }
        public double TempGrant { get; set; }

    }

    public class Comp1TalukaTarget
    {
        [Key]
       // public int Id { get; set; }
        public int SrNo { get; set; }

        public int ComponentNumber { get; set; }
        public string DistrictName { get; set; }
        public string TalukaName { get; set; }
        public long NoOfSheep { get; set; }
        public decimal PercentageOfSheep { get; set; }
        public Int64 PermOrigin { get; set; }
        public long TempOrigin { get; set; }
        public long PermGrant { get; set; }
        public long TempGrant { get; set; }

    }

    public class CompTarget2
    {
       [Key]
      ///  public int Id { get; set; }
        public int SrNo { get; set; }

        public int ComponentNumber { get; set; }
        public string DistrictName { get; set; }
        public long NoOfSheep { get; set; }
        public decimal PercentageOfSheep { get; set; }
        public int PermOrigin { get; set; }
        //public int TempOrigin { get; set; }
        public long PermGrant { get; set; }
        //public double TempGrant { get; set; }

    }

    public class Comp2TargetTaluka
    {
        [Key]
        //public int Id { get; set; }
        public int SrNo { get; set; }
        public int ComponentNumber { get; set; }
        public string DistrictName { get; set; }
        public string TalukaName { get; set; }
        public long NoOfSheep { get; set; }
        public decimal PercentageOfSheep { get; set; }
        public long PermOrigin { get; set; }
        //public int TempOrigin { get; set; }
        public long PermGrant { get; set; }
        //public double TempGrant { get; set; }

    }

    public class Comp3PhysicalTarget
    {
        [Key]
        public int Id { get; set; }
        public int SrNo { get; set; }

        public int ComponentNumber { get; set; }
        public string DistrictName { get; set; }
        public long NoOfSheep { get; set; }
        public decimal PercentageOfSheep { get; set; }
        public long PermBeneficiary1Origin { get; set; }
        public long PermBeneficiary2Origin { get; set; }
        public long TempBeneficiary1Origin { get; set; }
        public long TempBeneficiary2Origin { get; set; }
        public long PermGrantBeneficiary1 { get; set; }
        public long PermGrantBeneficiary2 { get; set; }
        public long TempGrantBeneficiary1 { get; set; }
        public long TempGrantBeneficiary2 { get; set; }

    }

    public class Comp3TargetTaluka
    {
        [Key]
        //public int Id { get; set; }
        public int SrNo { get; set; }

        public int ComponentNumber { get; set; }
        public string DistrictName { get; set; }
        public string TalukaName { get; set; }
        public long NoOfSheep { get; set; }
        public decimal PercentageOfSheep { get; set; }
        public long PermBeneficiary1Origin { get; set; }
        public long PermBeneficiary2Origin { get; set; }
        public long TempBeneficiary1Origin { get; set; }
        public long TempBeneficiary2Origin { get; set; }
        public long PermGrantBeneficiary1 { get; set; }
        public long PermGrantBeneficiary2 { get; set; }
        public long TempGrantBeneficiary1 { get; set; }
        public long TempGrantBeneficiary2 { get; set; }

    }

    public class Comp4PhysicalTarget
    {
        [Key]
        public int Id { get; set; }
        public int SrNo { get; set; }

        public int ComponentNumber { get; set; }
        public string DistrictName { get; set; }
        public long NoOfSheep { get; set; }
        public decimal PercentageOfSheep { get; set; }
        public long PermOrigin { get; set; }
        public long TempOrigin { get; set; }
        public long PermGrant { get; set; }
        public long TempGrant { get; set; }

    }

    public class Comp4TargetTaluka
    {
        [Key]
        //public int Id { get; set; }
        public int SrNo { get; set; }

        public int ComponentNumber { get; set; }
        public string DistrictName { get; set; }
        public string TalukaName { get; set; }
        public long NoOfSheep { get; set; }
        public decimal PercentageOfSheep { get; set; }
        public int PermOrigin { get; set; }
        public int TempOrigin { get; set; }
        public double PermGrant { get; set; }
        public double TempGrant { get; set; }

    }

}