using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        public int Component { get; set; }
        public string LDORecommended { get; set; }
        public string LDORemarks { get; set; }
        public DateTime? LDOSubmitDate { get; set; }
        public string LDO_Ip { get; set; }
        public string DAHORecommended { get; set; }
        public string DAHORemarks { get; set; }
        public DateTime? DAHOSubmitDate { get; set; }
        public string DAHO_Ip { get; set; }
        public string DDCRecommended { get; set; }
        public string DDCRemarks { get; set; }
        public DateTime? DDCSubmitDate { get; set; }
        public string DDC_Ip { get; set; }
        public string SGDCRecommended { get; set; }
        public string SGDCRemarks { get; set; }
        public DateTime? SGDCSubmitDate { get; set; }
        public string SGDC_Ip { get; set; }
        public bool SavedByLDO { get; set; }
        public bool SavedByDAHO { get; set; }
        public bool SavedByDDC { get; set; }
        public bool LDO_ComponentApproved { get; set; }
        public string LDO_ComponentRemarks { get; set; }
        public string LDOName { get; set; }
        public string DAHOName { get; set; }
        public string DDCName { get; set; }

    }

    public class ApplicantDocuments
    {
        [Key]
        public int Id { get; set; }
        public int ApplicantID { get; set; }
        public long? AadharNumber { get; set; }
        public int DistCode { get; set; }
        public int TalukaCode { get; set; }
        public int DocNumber { get; set; }
        public string GoogleDocID { get; set; }
        public bool LDOApproved { get; set; }
        public string LDORemarks { get; set; }
    }

    public class LDOConditions
    {
        [Key]
        public int Id { get; set; }
        public int ApplicantID { get; set; }
        public long? AadharNumber { get; set; }
        public int DistCode { get; set; }
        public int TalukaCode { get; set; }
        public int ConditionNo { get; set; }
        public string ApprovalCondition { get; set; }

        public string ApprovalValue { get; set; }
        [NotMapped]
        public string ComponentDesc { get; set; }
        public int DocNumber { get; set; }
        public string DocName { get; set; }
        public string GoogleDocID { get; set; }
        public bool LDOApproved { get; set; }
        public string LDORemarks { get; set; }
    }
}