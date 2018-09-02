 

namespace IARTAutomationApp.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class StationaryStoreMap
    {
        public int Id { get; set; }
        public string Item { get; set; }
        public string Class { get; set; }
        public decimal Price { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
