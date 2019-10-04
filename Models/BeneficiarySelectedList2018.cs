using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mahamesh.Models
{
    public class BeneficiarySelectedList2018
    {
        [Key]
        public int Id { get; set; }
        public int Comp_No { get; set; }
        public string Dist { get; set; }
        public string Taluka { get; set; }
        public string Village { get; set; }
        public int App_Id { get; set; }
        public string ApplicantName { get; set; }
        public string Ph { get; set; }
        public int  DOB { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public string Caste { get; set; }
        public string Subcaste { get; set; }
        public int No_Of_Sheep { get; set; }
        public long Aadhar { get; set; }
        public string Crippled { get; set; }
        public int Cripple_Percent { get; set; }
        public string Bachat_Gat { get; set; }
        public string SavingAcName { get; set; }
        public string Reg_No { get; set; }
        public string Company { get; set; }

   
    }
}