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
    
    public partial class DimFacility
    {
        public DimFacility()
        {
            this.DimBuildings = new HashSet<DimBuilding>();
        }
    
        public int id { get; set; }
        public string SiteName { get; set; }
        public Nullable<int> KeyCountry { get; set; }
        public Nullable<int> IsActive { get; set; }
    
        public virtual ICollection<DimBuilding> DimBuildings { get; set; }
        public virtual DimCountry DimCountry { get; set; }
    }
}
