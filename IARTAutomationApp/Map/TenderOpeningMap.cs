

namespace IARTAutomationApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;
    using System.ComponentModel;

    public partial class TenderOpeningMap
    {
        public int Id { get; set; }
    
        [DisplayName("Name of Representative")]
        [Required(ErrorMessage = "Please Input Name of Representative")]
        public string RepresentativeName { get; set; }
        [DisplayName("Name of Company")]
        [Required(ErrorMessage = "Please Input Name of Company")]
        public string CompanyName { get; set; }
        [DisplayName("Amount Quoted")]
        [Required(ErrorMessage = "Please Input Amount Quoted")]
        public Nullable<decimal> AmountQuoted { get; set; }
        [DisplayName("Completion Period: From")]
        [Required(ErrorMessage = "Please Input Completion Period")]
       
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public Nullable<System.DateTime> CompletionPeriodFrom { get; set; }
        [DisplayName("Completion Period: To")]
        [Required(ErrorMessage = "Please Input Completion Period")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public Nullable<System.DateTime> CompletionPeriodTo { get; set; }
        [DisplayName("Remarks")]
        [Required(ErrorMessage = "Please Input Remarks")]
        public string Remarks { get; set; }
        [DisplayName("LOT NO")]
        [Required(ErrorMessage = "Please Input LOT NO")]
        public string LotNo { get; set; }
        [DisplayName("Project Title")]
        [Required(ErrorMessage = "Please Input Project Title")]
        public string ProjectTitle { get; set; }
        [DisplayName("Year of Project")]
        [Required(ErrorMessage = "Please Select Year of Project")]
        public string YearofProject { get; set; }
        [HiddenInput]
        public System.DateTime CreatedDate { get; set; }
        [HiddenInput]
        public bool IsDeleted { get; set; }
    
        
    }
    [MetadataTypeAttribute(typeof(TenderOpeningMap))]
    partial class TenderOpening
    {

    }
}
