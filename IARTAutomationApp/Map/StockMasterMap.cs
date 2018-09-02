using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IARTAutomationApp.Models
{
    public class StockMasterMap
    {
        [HiddenInput]
        public int RecordId { get; set; }

        [DisplayName("Store")]
        [Required(ErrorMessage = "Please Select Store")]
        public Nullable<int> StoreId { get; set; }

        [DisplayName("Class")]
        [Required(ErrorMessage = "Please Select Class")]
        public Nullable<int> ClassId { get; set; }

        [DisplayName("Item")]
        [Required(ErrorMessage = "Please Select Item")]
        public Nullable<int> ItemId { get; set; }

        [DisplayName("Batch Number")]
        [Required(ErrorMessage = "Please Input Batch Number")]
        public string BatchNo { get; set; }

        [DisplayName("Quantity")]
        [Required(ErrorMessage = "Please Input Qunatity")]
        public string Quantity { get; set; }

        [DisplayName("UOM")]
        [Required(ErrorMessage = "Please Select UOM")]
        public Nullable<int> UomId { get; set; }


        [DisplayName("Vendor")]
        [Required(ErrorMessage = "Please Select Vendor")]
        public Nullable<int> VendorId { get; set; }

        [DisplayName("Purchase Invoice Number")]
        [Required(ErrorMessage = "Please Select Purchase Invoice Number")]
        public string PInvoiceNo { get; set; }

        [DisplayName("Reciving Date")]
        [Required(ErrorMessage = "Please Select Reciving Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public Nullable<System.DateTime> ReciveDate { get; set; }


        [DisplayName("Total Price")]
        [Required(ErrorMessage = "Please Input Total Price")]
        public string TotalPrice { get; set; }

        [HiddenInput]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public System.DateTime CreatedDate { get; set; }

        [HiddenInput]
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        


    }
    [MetadataTypeAttribute(typeof(StockMasterMap))]
    partial class StockMaster
    {

    }
}