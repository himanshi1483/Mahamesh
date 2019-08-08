using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mahamesh.Models
{
    public class FeedbackModel
    {
        [Key]
        public int FeedbackId { get; set; }
        [Display(Name = "Title")]
        public string FeedbackTitle { get; set; }
        [Display(Name = "Description")]
        public string FeedbackDescription { get; set; }
        [Display(Name = "Date")]
        public DateTime? FeedbackDate { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}