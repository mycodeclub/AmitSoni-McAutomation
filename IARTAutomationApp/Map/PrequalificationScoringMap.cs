 

namespace IARTAutomationApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Web;
    using System.Web.Mvc;

    public partial class PrequalificationScoringMap
    {
        public int Id { get; set; }
      
        [DisplayName("Name of Company")]
        [Required(ErrorMessage = "Please Input Name of Company")]
        public string CompanyName { get; set; }
        [DisplayName("Project Title")]
        [Required(ErrorMessage = "Please Input Project Title")]
        public string ProjectTitle { get; set; }
        [DisplayName("Lot No")]
        [Required(ErrorMessage = "Please Input Lot No")]
        public string LotNo { get; set; }
        [DisplayName("Name Of Contractor")]
        [Required(ErrorMessage = "Please Input Name Of Contractor")]
        public string ContractorName { get; set; }
        [DisplayName("Evidence Of Registration With CAC")]
        [Required(ErrorMessage = "Please Select Evidence Of Registration With CAC")]
        public string EvidofReg_Cac { get; set; }
        [DisplayName("Tax Clearance Certificate")]
        [Required(ErrorMessage = "Please Select Tax Clearance Certificate")]
        public string TaxClearanceCertificate { get; set; }
        [DisplayName("Evidence Of Registration With Bureau Of Public Procurement")]
        [Required(ErrorMessage = "Please Select Evidence Of Registration")]
        public string EvidofReg_Bureau { get; set; }
        [DisplayName("Auditted Account")]
        [Required(ErrorMessage = "Please Input Auditted Account")]
        public string AudittedAccount { get; set; }
        [DisplayName("Clearance Certificate From ITF")]
        [Required(ErrorMessage = "Please Input Clearance Certificate From ITF")]
        public string ClearanceCert_Itf { get; set; }
        [DisplayName("Clearance Certificate From PENCOM")]
        [Required(ErrorMessage = "Please Input Clearance Certificate From PENCOM")]
        public string ClearanceCert_Pencom { get; set; }
        [DisplayName("Clearance Certificate From NSITF")]
        [Required(ErrorMessage = "Please Input Clearance Certificate From PENCOM")]
        public string ClearanceCert_Nsitf { get; set; }
        [DisplayName("Technical & Admin Staff Strenth")]
        [Required(ErrorMessage = "Please Input Technical & Admin Staff Strenth")]
        public string StaffStrength { get; set; }
        [DisplayName("Current Financial Status Of Company")]
        [Required(ErrorMessage = "Please Input Current Financial Status Of Company")]
        public string CurrentFinStatus { get; set; }
        [DisplayName("List Of Equipment")]
        [Required(ErrorMessage = "Please Input List Of Equipment")]
        public string EquipmentList { get; set; }
        [DisplayName("Evidence Of Similar Jobs Previouly Executed")]
        [Required(ErrorMessage = "Please Input Evidence Of Similar Jobs Previouly Executed")]
        public string EvidPreSimJob { get; set; }
        [DisplayName("Experience/Technical Competence")]
        [Required(ErrorMessage = "Please Input Experience")]
        public string ExpCompt { get; set; }
        [DisplayName("Final Assessment Score")]
        [Required(ErrorMessage = "Please Input FinalScore")]
        public string FinalScore { get; set; }
        [HiddenInput]
        public System.DateTime CreatedDate { get; set; }
        [HiddenInput]
        public bool IsDeleted { get; set; }
    
     
    }
    [MetadataTypeAttribute(typeof(PrequalificationScoringMap))]
    partial class PrequalificationScoring
    {

    }
}
