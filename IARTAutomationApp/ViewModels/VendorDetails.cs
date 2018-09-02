using IARTAutomationApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IARTAutomationApp.ViewModels
{
    public class VendorDetails
    {
        public VendorMaster vendor { get; set; }
        public StatusMaster status { get; set; }

        public COUNTRYLIST country { get; set; }

        public string state { get; set; }
        public string city { get; set; }

        public string empName { get; set; }
    }
}