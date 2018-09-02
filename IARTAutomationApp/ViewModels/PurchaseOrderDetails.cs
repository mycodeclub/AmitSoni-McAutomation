using IARTAutomationApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IARTAutomationApp.ViewModels
{
    public class PurchaseOrderDetails
    {
        public PurchaseOrder porder { get; set; }
        public ItemMaster item { get; set; }

        public VendorMaster vendor { get; set; }

        public string empName { get; set; }
    }
}