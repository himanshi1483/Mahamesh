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

        public string Username { get; set; }
        public string pwd { get; set; }
        [NotMapped]
        public string confirmpwd { get; set; }
        public string ResetPwd { get; set; }
        //public string Reset_Pwd { get; set; }

        [NotMapped]
        public List<OfficerLogin> OfficerList { get; set; }
        [NotMapped]
        public string ChangedBy { get; set; }
    }
}