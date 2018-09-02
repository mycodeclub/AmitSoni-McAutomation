using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IARTAutomationApp.ViewModels
{
    public class GraphData
    {
        public string label { get; set; }
        public int value { get; set; }

        public GraphData(string label, int value)
        {
            this.label = label;
            this.value = value;
        }

        public GraphData()
        {
        }
    }
}