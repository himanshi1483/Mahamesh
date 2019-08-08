using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mahamesh.Models
{
    public class NewsModel
    {
        [Key]
        public int NewsId { get; set; }
        [Display(Name ="Title")]
        public string NewsTitle { get; set; }
        [Display(Name = "Description")]
        public string NewsDescription { get; set; }
        [Display(Name = "Date")]
        public DateTime? NewsDate { get; set; }
        public string ImageLocation { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}