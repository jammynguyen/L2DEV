//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PE.DbEntity.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class HMIUserConfiguration
    {
        public long ConfigurationId { get; set; }
        public string FKUserId { get; set; }
        public string UserName { get; set; }
        public string ConfigurationName { get; set; }
        public string ConfigurationContent { get; set; }
        public string ConfigurationType { get; set; }
    }
}
