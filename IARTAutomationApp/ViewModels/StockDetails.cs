using IARTAutomationApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IARTAutomationApp.ViewModels
{
    public class StockDetails
    {

        public ItemMaster item { get; set; }
        public StoreMaster store { get; set; }
        public VendorMaster vendor { get; set; }
        public StockMaster stock { get; set; }

        public ClassMaster classMas { get; set; }
        public UomMaster uom { get; set; }

        public PurchaseOrder po { get; set; }

        public string empName { get; set; }
    }
}