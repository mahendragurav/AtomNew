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
    
    public partial class DimBusinessPartner
    {
        public int id { get; set; }
        public string BusinessPartnerName { get; set; }
        public string BPCode { get; set; }
        public Nullable<int> KeySite { get; set; }
        public Nullable<int> IsActive { get; set; }
        public Nullable<int> KeyBuilding { get; set; }
    }
}