using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IARTAutomationApp.ViewModels
{
    public class StoreInventoryDetails
    {
        [DisplayName("Store Name")]
        public string storeName { get; set; }

        [DisplayName("Store Number")]
        public int storeNumber { get; set; }

        [DisplayName("Store Description")]
        public string storeDesc { get; set; }

        [DisplayName("Store Status")]
        public string storeStatus { get; set; }

        public int? empId { get; set; }
        public string empName { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}", ApplyFormatInEditMode = true)]
        public DateTime createdDate { get; set; }

        [DisplayName("Total Items")]
        public int storeItemCount { get; set; }
    }
}