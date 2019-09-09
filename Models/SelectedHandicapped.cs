using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mahamesh.Models
{
    public class SelectedHandicapped
    {
        [Key]
        public int Id { get; set; }
        public int ApplicationId { get; set; }
        public string ApplicationNumber { get; set; }
        public int DistCode { get; set; }
        public string District_Mr { get; set; }
        public int TalukaCode { get; set; }
        public string Taluka_Mr { get; set; }
        public string VillageName { get; set; }
        public string Name { get; set; }
        public long? PhNo { get; set; }
        public long AadharNo { get; set; }
        public string Gender{ get; set; }
        public string ApplicantCrippled { get; set; }
        public int Component { get; set; }
        public string Type { get; set; }
        public DateTime CreatedOn { get; set; }
    }

    public class SelectedFemale
    {
        [Key]
        public int Id { get; set; }
        public int ApplicationId { get; set; }
        public string ApplicationNumber { get; set; }
        public int DistCode { get; set; }
        public string District_Mr { get; set; }
        public int TalukaCode { get; set; }
        public string Taluka_Mr { get; set; }
        public string VillageName { get; set; }
        public string Name { get; set; }

        public long? PhNo { get; set; }
        public long AadharNo { get; set; }
        public string Gender { get; set; }
        public string ApplicantCrippled { get; set; }
        public int Component { get; set; }
        public string Type { get; set; }
        public DateTime CreatedOn { get; set; }
    }

    public class SelectedGeneral
    {
        [Key]
        public int Id { get; set; }
        public int ApplicationId { get; set; }
        public string ApplicationNumber { get; set; }
        public int DistCode { get; set; }
        public string District_Mr { get; set; }
        public int TalukaCode { get; set; }
        public string Taluka_Mr { get; set; }
        public string VillageName { get; set; }
        public long? PhNo { get; set; }
        public string Name { get; set; }

        public long AadharNo { get; set; }
        public string Gender { get; set; }
        public string ApplicantCrippled { get; set; }
        public int Component { get; set; }
        public string Type { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}