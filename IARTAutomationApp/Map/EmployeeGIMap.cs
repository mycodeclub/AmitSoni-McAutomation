using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.Reflection;
using System.ComponentModel.DataAnnotations.Schema;

namespace IARTAutomationApp.Models
{
    public class EmployeeGIMap
    {
        public int EmployeeGIId { get; set; }
        [DisplayName("Employee Code")]
        [Required(ErrorMessage = "Please Select a Employee Code")]

        public int EmployeeCode { get; set; }
        [DisplayName("Rank")]
        [Required(ErrorMessage = "Please Input Employee's Rank")]

        public string Rank { get; set; }


        [DisplayName("File No")]
        [Required(ErrorMessage = "Please Select a FIle No")]
        public string File_No { get; set; }
        [DisplayName("Grade Level")]
        [Required(ErrorMessage = "Please Select a Grade Level")]

        public string Grade_Level { get; set; }
        [DisplayName("Step")]
        [Required(ErrorMessage = "Please Select a Step")]

        public string Step { get; set; }
        [DisplayName("Cadre/Job Schedule")]
         [Required(ErrorMessage = "Please Select a Cadre/Job Schedule")]
         public string Cadre { get; set; }

        [DisplayName("Title")]
         [Required(ErrorMessage = "Please Select a Title")]
        public string Title { get; set; }

        [DisplayName("First Name"), MinLength(2), MaxLength(30)]

        [Required(ErrorMessage = "Please Input a First Name ")]
            
        public string First_Name { get; set; }
        //[Required(ErrorMessage = "Please Input a Middle Name ")]
 
        //[DisplayName("Middle Name"), MinLength(2), MaxLength(30)]
        public string Middle_Name { get; set; }
        [Required(ErrorMessage = "Please Input a Surname ")]
 
        [DisplayName("Surname"), MinLength(2), MaxLength(30)]
        public string Surname { get; set; }
        [DisplayName("Sex")]

        [Required(ErrorMessage = "Please Select a Sex ")]
 
        public string Sex { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Please Select a Date Of Birth ")]

        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}", ApplyFormatInEditMode = true, NullDisplayText = "+DateTime.Now.AddYears(-18).Date.ToString()+")]
        [DisplayName("Date Of Birth")]
        public Nullable<System.DateTime> DateOfBirth { get; set; }
        [DisplayName("Place Of Birth")]
        [Required(ErrorMessage = "Please Input a Place Of Birth")]

        public string PlaceOfBirth { get; set; }
        [Required(ErrorMessage = "Please Input a Marital Status ")]

        [DisplayName("Marital Status")]
        public string Marital_Status { get; set; }
        [Required(ErrorMessage = "Please Input a Maiden Name ")]

        [DisplayName("Maiden Name")]
        public string Maiden_Name { get; set; }
        
        [DisplayName("Spouse Name")]

       

        public string Spouse_Name { get; set; }
        [Required(ErrorMessage = "Please Select a State Of Origin ")]

        [DisplayName("State Of Origin")]
        public string StateOfOrigin { get; set; }
        [Required(ErrorMessage = "Please Select a LGA ")]

        [DisplayName("LGA")]
        public string LGA { get; set; }
        [Required(ErrorMessage = "Please Input a Home Town ")]

        [DisplayName("Home Town")]
        public string Home_Town { get; set; }
        [DisplayName("Religion")]
        [Required(ErrorMessage = "Please Input a Religion ")]

        public string Religion { get; set; }
        [Required(ErrorMessage = "Please Input a Contact Home Address ")]

        [DisplayName("Contact Home Address")]
        public string ContactHomeAddress { get; set; }
        [Required(ErrorMessage = "Please select a First Appointment Date ")]

        [DisplayName("First Appointment Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public Nullable<System.DateTime> FirstAppointmentDate { get; set; }
        [Required(ErrorMessage = "Please Input a First Appointment Location ")]

        [DisplayName("First Appointment Location")]
        public string FirstAppointmentLocation { get; set; }
        [Required(ErrorMessage = "Please select a Confirmation Date ")]

        [DisplayName("Confirmation Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public Nullable<System.DateTime> ConfirmationDate { get; set; }
        [DisplayName("Date Of Retirement")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        [Required(ErrorMessage = "Please select a Date Of Retirement ")]
        public Nullable<System.DateTime>  DateOfRetirement { get; set; }

        [Required(ErrorMessage = "Please select a Date Of Last Promotion ")]

        [DisplayName("Last Promotion Date")]
        [ValidatesDatePromotion]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public Nullable<System.DateTime> LastPromotionDate { get; set; }
        //[DisplayName("Department")]
        //[Required(ErrorMessage = "Please Input Department ")]

        //public string Department { get; set; }
        [Required(ErrorMessage = "Please Input a Unit ")]

        [DisplayName("Unit Of Research")]
        public string Unit_Research { get; set; }

       
        public string Programmes { get; set; }
        public string Unit_Services { get; set; }


        [Required(ErrorMessage = "Please Input a Section ")]

        [DisplayName("Section")]
        public string Section { get; set; }
        //[Required(ErrorMessage = "Please Input a Ministry Agency ")]

       
        [DisplayName("Leave Days")]
        public Nullable<int> LeaveDays { get; set; }
        [DisplayName("Leave from Date")]
        [DataType(DataType.Date)]
     

        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public Nullable<System.DateTime> Leave_fromDate { get; set; }
        [ValidatesDate]
        [DisplayName("Leave To Date")]
        [DataType(DataType.Date)]
        
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public Nullable<System.DateTime> Leave_ToDate { get; set; }
        [NotMapped]
        [DisplayName("Upload Passport Photo Of Employee")]
        [DataType(DataType.Upload)]
        //[RegularExpression(@"([a-zA-Z0-9\s_\\.\-:])+(.png|.jpg|.gif)$", ErrorMessage = "Only Image files allowed.")]
        public HttpPostedFileBase EmployeePhotoImage { get; set; }
      

   
        public string EmployeePhoto { get; set; }
        [DisplayName("Station Of Deployment")]
        [Required(ErrorMessage = "Please select a Station Of Deployment")]

        public string StationOfDeployment { get; set; }
        [HiddenInput]
        public bool IsDeleted { get; set; }
        [HiddenInput]
        public System.DateTime CreatedDate { get; set; }


    }

    [MetadataType(typeof(EmployeeGIMap))]
    partial class EmployeeGI
    {

    }
}