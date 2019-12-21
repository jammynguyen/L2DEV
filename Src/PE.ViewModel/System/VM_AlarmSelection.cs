using PE.HMIWWW.Core.ViewModel;
using SMF.DbEntity.Model;
using SMF.HMIWWW.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PE.HMIWWW.ViewModel.System
{
    public class VM_AlarmSelection : VM_Base
    {
        public VM_AlarmSelection() { }

        public VM_AlarmSelection(Alarm alarm)
        {
            AlarmOwner = alarm.AlarmOwner;
            ToConfirm = alarm.ToConfirm;
        }
        public VM_AlarmSelection(string alarmOwner, bool? toConfirm)
        {
            AlarmOwner = alarmOwner;
            ToConfirm = toConfirm;
        }

        [SmfDisplayAttribute(typeof(VM_AlarmItem), "ToConfirm", "NAME_ToConfirm")]
        public virtual bool? ToConfirm { get; set; }

        [SmfDisplayAttribute(typeof(VM_AlarmItem), "AlarmOwner", "NAME_AlarmOwner")]
        public virtual String AlarmOwner { get; set; }
    }
}
