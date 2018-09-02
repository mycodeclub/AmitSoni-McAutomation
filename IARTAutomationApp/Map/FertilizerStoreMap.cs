 

namespace IARTAutomationApp.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class FertilizerStoreMap
    {
        public int Id { get; set; }
        public string Item { get; set; }
        public string Class { get; set; }
        public Nullable<decimal> Price { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
