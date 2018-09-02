using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IARTAutomationApp.Models
{
    public class PurchaseOrderMap
    {
        [HiddenInput]
        public int RecordId { get; set; }

        [DisplayName("Purchase Order Number")]
        [Required(ErrorMessage = "Please Input Purchase Order Number")]
        public string OrderNo { get; set; }

        [DisplayName("Vendor Name")]
        [Required(ErrorMessage = "Please Select Vendor Name")]
        public Nullable<int> VendorId { get; set; }

        [DisplayName("Item Name")]
        [Required(ErrorMessage = "Please Select Item Name")]
        public Nullable<int> ItemId { get; set; }

        [DisplayName("Item Quantity")]
        [Required(ErrorMessage = "Please Input Item Quantity")]
        public string ItemQunt { get; set; }

        [DisplayName("Item Rate")]
        [Required(ErrorMessage = "Please Input Item Rate")]
        public string ItemTax { get; set; }

        [DisplayName("Delivery/Shipping Address")]
        [Required(ErrorMessage = "Please Input Delivery/Shipping Address")]
        public string DeliLoc { get; set; }

        [DisplayName("Terms")]
        [Required(ErrorMessage = "Please Input Terms")]
        public string Terms { get; set; }

        [HiddenInput]
        public Nullable<int> StatusId { get; set; }

        [DisplayName("Item Description")]
        [Required(ErrorMessage = "Please Input Item Description")]
        public string ItemDesc { get; set; }

        [HiddenInput]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}", ApplyFormatInEditMode = true)]
        public System.DateTime CreatedDate { get; set; }

        [HiddenInput]
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        
    }
    [MetadataTypeAttribute(typeof(PurchaseOrderMap))]
    partial class PurchaseOrder
    {

    }
}