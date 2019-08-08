using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mahamesh.Models
{
    public class PressInformationModel
    {
        [Key]
        public int InformationId { get; set; }
        [Display(Name = "Title")]
        public string InformationTitle { get; set; }
        [Display(Name = "Description")]
        public string InformationDescription { get; set; }
        [Display(Name = "Date")]
        public DateTime? InformationDate { get; set; }
        public string InformationDocument { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}