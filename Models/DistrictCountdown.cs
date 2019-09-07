using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mahamesh.Models
{
    public class DistrictCountdown
    {
        [Key]
        public int Id { get; set; }
        public int DistCode { get; set; }
        public DateTime? EnableDate { get; set; }
        public DateTime? EnableTime { get; set; }
    }
}