using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Mahamesh.Models
{
    public class TenderModel
    {
        [Key]
        public int TenderId { get; set; }
        [Display(Name = "Title")]
        public string TenderTitle { get; set; }
        [Display(Name = "Description")]
        public string TenderDescription { get; set; }
        [Display(Name = "Date")]
        public DateTime? TenderDate { get; set; }
        public string TenderDocument { get; set; }
        public string DocumentName { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        [NotMapped]
        public List<TenderModel> TenderList { get; set; }
    }
}