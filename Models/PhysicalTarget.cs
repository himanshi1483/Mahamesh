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
       // public int TempOrigin { get; set; }
        public double PermGrant { get; set; }
        //public double TempGrant { get; set; }

    }

    public class Comp1PhysicalTargetTaluka
    {
        [Key]
        public int Id { get; set; }
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

    public class Comp2PhysicalTarget
    {
        [Key]
        public int Id { get; set; }
        public int SrNo { get; set; }

        public int ComponentNumber { get; set; }
        public string DistrictName { get; set; }
        public long NoOfSheep { get; set; }
        public decimal PercentageOfSheep { get; set; }
        public int PermOrigin { get; set; }
        //public int TempOrigin { get; set; }
        public double PermGrant { get; set; }
        //public double TempGrant { get; set; }

    }

    public class Comp2PhysicalTargetTaluka
    {
        [Key]
        public int Id { get; set; }
        public int SrNo { get; set; }
        public int ComponentNumber { get; set; }
        public string DistrictName { get; set; }
        public string TalukaName { get; set; }
        public long NoOfSheep { get; set; }
        public decimal PercentageOfSheep { get; set; }
        public int PermOrigin { get; set; }
        //public int TempOrigin { get; set; }
        public double PermGrant { get; set; }
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
        public int PermBeneficiary1Origin { get; set; }
        public int PermBeneficiary2Origin { get; set; }
        public int TempBeneficiary1Origin { get; set; }
        public int TempBeneficiary2Origin { get; set; }
        public double PermGrantBeneficiary1 { get; set; }
        public double PermGrantBeneficiary2 { get; set; }
        public double TempGrantBeneficiary1 { get; set; }
        public double TempGrantBeneficiary2 { get; set; }

    }

    public class Comp3PhysicalTargetTaluka
    {
        [Key]
        public int Id { get; set; }
        public int SrNo { get; set; }

        public int ComponentNumber { get; set; }
        public string DistrictName { get; set; }
        public string TalukaName { get; set; }
        public long NoOfSheep { get; set; }
        public decimal PercentageOfSheep { get; set; }
        public int PermBeneficiary1Origin { get; set; }
        public int PermBeneficiary2Origin { get; set; }
        public int TempBeneficiary1Origin { get; set; }
        public int TempBeneficiary2Origin { get; set; }
        public double PermGrantBeneficiary1 { get; set; }
        public double PermGrantBeneficiary2 { get; set; }
        public double TempGrantBeneficiary1 { get; set; }
        public double TempGrantBeneficiary2 { get; set; }

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
        public int PermOrigin { get; set; }
        public int TempOrigin { get; set; }
        public double PermGrant { get; set; }
        public double TempGrant { get; set; }

    }

    public class Comp4PhysicalTargetTaluka
    {
        [Key]
        public int Id { get; set; }
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