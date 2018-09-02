using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IARTAutomationApp.ViewModels
{
    public class GraphDataTender
    {
        public string label { get; set; }
        public int value { get; set; }

        public GraphDataTender(string label, int value)
        {
            this.label = label;
            this.value = value;
        }

        public GraphDataTender()
        {
        }
    }
}