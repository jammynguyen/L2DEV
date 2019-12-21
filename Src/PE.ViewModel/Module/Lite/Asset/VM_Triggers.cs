using PE.DbEntity.Enums;
using PE.DbEntity.Models;
using PE.HMIWWW.Core.Resources;
using PE.HMIWWW.Core.ViewModel;
using SMF.HMIWWW.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PE.HMIWWW.ViewModel.Module.Lite.Asset
{
    public class VM_Triggers : VM_Base
    {
        [SmfDisplayAttribute(typeof(VM_Triggers), "TriggerCode", "NAME_TriggerCode")]
        [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
        public string TriggerCode { get; set; }

        [SmfDisplayAttribute(typeof(VM_Triggers), "TriggerName", "NAME_TriggerName")]
        [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
        public string TriggerName { get; set; }

        [SmfDisplayAttribute(typeof(VM_Triggers), "TriggerId", "NAME_TriggerId")]
        [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
        public long TriggerId { get; set; }


        [SmfDisplayAttribute(typeof(VM_Triggers), "IsActive", "NAME_IsActive")]
        [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
        public bool IsActive { get; set; }





        public VM_Triggers(MVHTrigger table)
        {
            TriggerCode = table.TriggerCode;
            TriggerName = table.TriggerName;
            TriggerId = table.TriggerId;
            IsActive = table.IsActive;
        }

        public VM_Triggers()
        {


        }



    }
}
