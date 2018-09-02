using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IARTAutomationApp.Models
{
    public class StoreMasterMap
    {
        [DisplayName("Store Number")]
        [Required(ErrorMessage = "Please Input Store Number")]
        public int StoreNumber { get; set; }



        [DisplayName("Store Name")]
        [Required(ErrorMessage = "Please Input Store Name"), MinLength(2), MaxLength(30)]
        public String StoreName { get; set; }

        [DisplayName("Store Description")]
        [Required(ErrorMessage = "Please Input Store Description"), MinLength(2), MaxLength(50)]
        public String StoreDesc { get; set; }


        [DisplayName("Store Status")]
        [Required(ErrorMessage = "Please Select a Store Status ")]
        public int StoreStatus { get; set; }

        [DisplayName("Upload Store Image (Size: Width: 300px, Height: 250px)")]
        [Required(ErrorMessage = "Please Upload Store Image")]
        public string StoreImgName { get; set; }

        [HiddenInput]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}", ApplyFormatInEditMode = true)]
        public DateTime CreatedDate { get; set; }

        [HiddenInput]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}", ApplyFormatInEditMode = true)]
        public System.DateTime UpdatedDate { get; set; }

        [HiddenInput]
        public int RecordId { get; set; }

        



    }
    [MetadataTypeAttribute(typeof(StoreMasterMap))]
    partial class StoreMaster
    {

    }
}
