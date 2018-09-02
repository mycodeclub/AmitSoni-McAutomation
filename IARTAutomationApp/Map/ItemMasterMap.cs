using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IARTAutomationApp.Models
{
    public class ItemMasterMap
    {
        [HiddenInput]
        public int RecordId { get; set; }

        [DisplayName("Store")]
        [Required(ErrorMessage = "Please Select Store")]
        public Nullable<int> StoreId { get; set; }

        [DisplayName("Class")]
        [Required(ErrorMessage = "Please Select Class")]
        public Nullable<int> ClassId { get; set; }

        [DisplayName("Item Name")]
        public string ItemName { get; set; }

        [DisplayName("Item Category")]
        [Required(ErrorMessage = "Please Input Item Category")]
        public string ItemCat { get; set; }

        [DisplayName("UOM")]
        [Required(ErrorMessage = "Please Select UOM")]
        public Nullable<int> UomId { get; set; }

        [DisplayName("Vendor")]
        [Required(ErrorMessage = "Please Select Vendor")]
        public Nullable<int> VendorId { get; set; }

        [DisplayName("Item Price")]
        [Required(ErrorMessage = "Please Input Price")]
        public string ItemRate { get; set; }

        [DisplayName("Item Tax Details")]
        [Required(ErrorMessage = "Please Input Item Tax Details")]
        public string ItemTax { get; set; }

        [DisplayName("Item Image(Size: Width: 300px, Height: 250px)")]
        [Required(ErrorMessage = "Please Select Item Image")]
        public string ItemImage { get; set; }

        [DisplayName("Item Status")]
        [Required(ErrorMessage = "Please Select Item Status")]
        public Nullable<int> StatusId { get; set; }

        [DisplayName("Item Description")]
        [Required(ErrorMessage = "Please Input Item Description")]
        public string ItemDesc { get; set; }

        [HiddenInput]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public System.DateTime CreatedDate { get; set; }

        [HiddenInput]
        public Nullable<System.DateTime> UpdatedDate { get; set; }


    }
    [MetadataTypeAttribute(typeof(ItemMasterMap))]
    partial class ItemMaster
    {

    }
}