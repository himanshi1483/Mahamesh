using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.DynamicData;

namespace Mahamesh.Models
{
    [TableName("OfficerLogin")]
    public class OfficerLogin
    {
        [Key]
        public int ID { get; set; }
        public string desgination { get; set; }
        public int district { get; set; }
        [NotMapped]
        public string DistName { get; set; }
        public int taluka { get; set; }
        [NotMapped]
        public string TalukaName { get; set; }
        [NotMapped]
        public string Component { get; set; }

        public string Username { get; set; }
        public string pwd { get; set; }
        [NotMapped]
        public string confirmpwd { get; set; }
        public string ResetPwd { get; set; }
        //public string Reset_Pwd { get; set; }
        [NotMapped]
        public TimeSpan RemainingTime { get; set; }
        [NotMapped]
        public DistrictCountdown Timer { get; set; }
        [NotMapped]
        public List<DistrictCountdown> TimerList { get; set; }
        [NotMapped]
        public List<OfficerLogin> OfficerList { get; set; }
        [NotMapped]
        public string ChangedBy { get; set; }

        [NotMapped]
        public PhysicalTargetViewModel TargetModel { get; set; }

        [NotMapped]
        public TargetViewModel NewTarget { get; set; }

        [NotMapped]
        public SelectedListViewModel SelectedList { get; set; }
        [NotMapped]
        public SelectedListViewModel SelectedList1 { get; set; }

        [NotMapped]
        public SelectedListViewModel SelectedList2 { get; set; }

        [NotMapped]
        public List<TalMaster> TalukaList { get; set; }

        [NotMapped]
        public int TCount { get; set; }

        [NotMapped]
        public bool isGenerated { get; set; }
        [NotMapped]
        public DateTime CurrentTime { get; set; }
    }
}