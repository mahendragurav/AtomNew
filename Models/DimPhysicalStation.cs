//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ATOMv0.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class DimPhysicalStation
    {
        public int id { get; set; }
        public string StationName { get; set; }
        public string HostAlias { get; set; }
        public string CPUMegaHertz { get; set; }
        public string MemorySize { get; set; }
        public Nullable<int> KeyStationType { get; set; }
        public string Status { get; set; }
        public string FFInstanceID { get; set; }
        public Nullable<int> LineID { get; set; }
    }
}
