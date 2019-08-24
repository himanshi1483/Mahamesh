using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mahamesh.Models
{
    public class DistrictList
    {
        [Key]
        public int SrNo { get; set; }
        public string DistName { get; set; }
        public double Dist_Code { get; set; }
       
    }

    public class DistMaster
    {
        [Key]
        public int SrNo { get; set; }
        public string DistName { get; set; }
        public int Dist_Code { get; set; }

    }

    public class VillageMaster
    {
        [Key]
        public int SrNo { get; set; }
        public string VillageName { get; set; }
        public int Village_Code { get; set; }
        public int Tal_Code { get; set; }

    }

    public class TalMaster
    {
        [Key]
        public int SrNo { get; set; }
        public string TalName { get; set; }
        public int Tal_Code { get; set; }
        public int Dist_Code { get; set; }
    }

    public class CasteUnderNTC
    {
        [Key]
        public int ID { get; set; }
        public string Caste { get; set; }
        
    }

    public class NoOfSheepMaster
    {
        [Key]
        public int id { get; set; }
        public string NoOfSheep { get; set; }

    }

    public class CrippledMaster
    {
        [Key]
        public int ID { get; set; }
        public int Percentage { get; set; }

    }

    public class AcreMaster
    {
        [Key]
        public int id { get; set; }
        public decimal? Acre { get; set; }

    }

    public class WaterSource
    {
        [Key]
        public int ID { get; set; }
        public string SourceName { get; set; }

    }

    public class TypeExistingCastle 
    {
        [Key]
        public int ID { get; set; }
        public string TypeName { get; set; }

    }

    public class DurationWaterAvailableForIrrigation
    {
        [Key]
        public int ID { get; set; }
        public string DurationName { get; set; }

    }
}