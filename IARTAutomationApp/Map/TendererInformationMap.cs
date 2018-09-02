 

namespace IARTAutomationApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel;
    using System.Web.Mvc;

    public partial class TendererInformationMap
    {
       
    
        public int Id { get; set; }
       
        [DisplayName("Name of Company")]
        [Required(ErrorMessage = "Please Input Name of Company")]
        public string CompanyName { get; set; }
        [DisplayName("LOT NO")]
        [Required(ErrorMessage = "Please Input LOT NO")]
        public string LotNo { get; set; }
        [DisplayName("Project Title")]
        [Required(ErrorMessage = "Please Input Project Title")]
        public string ProjectTitle { get; set; }
        [DisplayName("Name of Representative")]
        [Required(ErrorMessage = "Please Input Name of Representative")]
        public string RepresentativeName { get; set; }
        [DisplayName("Phone Number(s)")]
        [Required(ErrorMessage = "Please Input Phone Number(s)")]
        [DataType (DataType.PhoneNumber)]
        public string PhoneNo { get; set; }
        [DisplayName("Date of Submission of Bid Document")]
        [Required(ErrorMessage = "Please Input Date of Submission of Bid Document")]
       
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public Nullable<System.DateTime> SubmissionDate { get; set; }
        [DisplayName("Year of Project")]
        [Required(ErrorMessage = "Please Input Year of Project")]
        public string YearofProject { get; set; }
        [HiddenInput]
        public System.DateTime CreatedDate { get; set; }
        [HiddenInput]
        public bool IsDeleted { get; set; }
    
     
    }
    [MetadataTypeAttribute(typeof(TendererInformationMap))]
    partial class TendererInformation
    {

    }
}
