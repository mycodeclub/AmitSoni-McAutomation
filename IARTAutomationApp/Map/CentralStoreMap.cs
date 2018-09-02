 
namespace IARTAutomationApp.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class CentralStoreMap
    {
        public int Id { get; set; }
        public string Item { get; set; }
        public string Class { get; set; }
        public string Price { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
