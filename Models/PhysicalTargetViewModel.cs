using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mahamesh.Models
{
    public class PhysicalTargetViewModel
    {
        public List<Comp1Target> Comp1TargetList { get; set; }
        public List<Comp1TalukaTarget> Comp1TalukaList { get; set; }
        public List<CompTarget2> Comp2TargetList { get; set; }
        public List<Comp2TargetTaluka> Comp2TalukaList { get; set; }
        public List<Comp3PhysicalTarget> Comp3TargetList { get; set; }
        public List<Comp3TargetTaluka> Comp3TalukaList { get; set; }
        public List<Comp4PhysicalTarget> Comp4TargetList { get; set; }
        public List<Comp4TargetTaluka> Comp4TalukaList { get; set; }
        public int SrNo { get; set; }

        public string Component { get; set; }
        public string DistrictName { get; set; }
        public string TalukaName { get; set; }
     
    }
}