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
    
    public partial class DimFFInstance
    {
        public int id { get; set; }
        public string DataSourceName { get; set; }
        public string HostName { get; set; }
        public string DatabaseName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ProjectName { get; set; }
        public string DataFilePrefix { get; set; }
        public Nullable<int> ReplicationDelayMinute { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<int> KeyModule { get; set; }
        public string ITContactName { get; set; }
        public string ITPhone { get; set; }
        public string ITEmail { get; set; }
        public string QAContactName { get; set; }
        public string QAPhone { get; set; }
        public string QAEmail { get; set; }
        public string SiteName { get; set; }
        public string BaanCoNo { get; set; }
    
        public virtual DimModule DimModule { get; set; }
    }
}
