using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mahamesh.Models
{
    public class ApplicationListsViewModel
    {
        public List<ApplicationListsViewModel> ApplicantsListByDist { get; set; }
        public List<ApplicationListsViewModel> ApplicantsListByTal { get; set; }
        public List<ApplicationListsViewModel> ApplicantsListByComp { get; set; }
        public int DistrictCode { get; set; }
        public string DistrictName { get; set; }
        public int TalukaCode { get; set; }
        public string TalukaName { get; set; }
        public string CompNumbers { get; set; }
        public List<Components> Components { get; set; }
        public int CountByDistrict { get; set; }
        public int CountByTaluka { get; set; }
        public int CountByComponent1 { get; set; }
        public int CountByComponent2 { get; set; }
        public int CountByComponent3 { get; set; }
        public int CountByComponent4 { get; set; }
        public int CountByComponent5 { get; set; }
        public int CountByComponent6 { get; set; }
        public int CountByComponent7 { get; set; }
        public int CountByComponent8 { get; set; }
        public int CountByComponent9 { get; set; }
        public int CountByComponent10 { get; set; }
        public int CountByComponent11 { get; set; }
        public int CountByComponent12 { get; set; }
        public int CountByComponent13 { get; set; }
        public int CountByComponent14 { get; set; }
        public int CountByComponent15 { get; set; }
    }
}