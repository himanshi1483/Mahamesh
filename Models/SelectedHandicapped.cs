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
        public int TalukaCode { get; set; }
        public int Component { get; set; }
        public string Type { get; set; }
    }

    public class SelectedFemale
    {
        [Key]
        public int Id { get; set; }
        public int ApplicationId { get; set; }
        public string ApplicationNumber { get; set; }
        public int DistCode { get; set; }
        public int TalukaCode { get; set; }
        public int Component { get; set; }
        public string Type { get; set; }
    }

    public class SelectedGeneral
    {
        [Key]
        public int Id { get; set; }
        public int ApplicationId { get; set; }
        public string ApplicationNumber { get; set; }
        public int DistCode { get; set; }
        public int TalukaCode { get; set; }
        public int Component { get; set; }
        public string Type { get; set; }
    }
}