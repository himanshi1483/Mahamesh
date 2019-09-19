using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mahamesh.Models
{
    public class PreliminaryList
    {
        [Key]
        public int Id { get; set; }
        public int ApplicantID { get; set; }
        public long? AadharNumber { get; set; }
        public int DistCode { get; set; }
        public int TalukaCode { get; set; }
        public bool DocumentVerified { get; set; }
    }
}