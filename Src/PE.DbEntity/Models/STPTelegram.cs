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
    
    public partial class STPTelegram
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public STPTelegram()
        {
            this.STPTelegramValues = new HashSet<STPTelegramValue>();
        }
    
        public long TelegramId { get; set; }
        public long FKTelegramStructureId { get; set; }
        public string TelegramCode { get; set; }
        public string TelegramName { get; set; }
        public string TelegramDescription { get; set; }
        public Nullable<short> Port { get; set; }
        public string TcpIp { get; set; }
        public Nullable<System.DateTime> LastSent { get; set; }
        public Nullable<short> Id { get; set; }
    
        public virtual STPTelegramStructure STPTelegramStructure { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<STPTelegramValue> STPTelegramValues { get; set; }
    }
}
