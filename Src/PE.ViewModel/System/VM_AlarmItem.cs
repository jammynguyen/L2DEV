using PE.HMIWWW.Core.Resources;
using PE.HMIWWW.Core.ViewModel;
using SMF.DbEntity.Dto;
using SMF.DbEntity.Model;
//using SMF.HMI.Module.AlarmStorage;
using SMF.HMIWWW.Attributes;
using System;
using System.Collections.Generic;

namespace PE.HMIWWW.ViewModel.System
{

    public class VM_AlarmItem: VM_Base
    {
        public VM_AlarmItem() { }

        public VM_AlarmItem(Alarm alarm)
        {
            Id = alarm.AlarmId;
            AlarmDate = alarm.AlarmDate;
            Message = alarm.Message;
            AlarmOwner = alarm.AlarmOwner;
            Confirmation = alarm.Confirmation;
            ConfirmationDate = alarm.ConfirmationDate;
            ConfirmedBy = alarm.ConfirmedBy;
            AlarmCategoryName = alarm.AlarmCategoryName;
            AlarmType = alarm.AlarmType;
          //  AlarmType = ResourceController.GetEnumValue("AlarmType", (int)alarm.AlarmType);
           // AlarmDateJs = alarm.AlarmDate.ToString();
            ToConfirm = alarm.ToConfirm;
        }

        [SmfDisplayAttribute(typeof(VM_AlarmItem), "Id", "NAME_Id")]
        public virtual Int64 Id { get; set; }

        [SmfDisplayAttribute(typeof(VM_AlarmItem), "AlarmDate", "NAME_AlarmDate")]
        public virtual DateTime AlarmDate { get; set; }

        [SmfDisplayAttribute(typeof(VM_AlarmItem), "Message", "NAME_Message")]
        public virtual String Message { get; set; }

        [SmfDisplayAttribute(typeof(VM_AlarmItem), "AlarmOwner", "NAME_AlarmOwner")]
        public virtual String AlarmOwner { get; set; }

        [SmfDisplayAttribute(typeof(VM_AlarmItem), "Confirmation", "NAME_Confirmation")]
        public virtual Boolean? Confirmation { get; set; }

        [SmfDisplayAttribute(typeof(VM_AlarmItem), "ConfirmationDate", "NAME_ConfirmationDate")]
        public virtual DateTime? ConfirmationDate { get; set; }

        [SmfDisplayAttribute(typeof(VM_AlarmItem), "ConfirmedBy", "NAME_ConfirmedBy")]
        public virtual String ConfirmedBy { get; set; }

        //[SmfDisplayAttribute(typeof(VM_AlarmItem), "AlarmTypeId", "NAME_AlarmTypeId")]
        //public virtual int AlarmTypeId { get; set; }

        [SmfDisplayAttribute(typeof(VM_AlarmItem), "AlarmCategoryName", "NAME_AlarmCategoryName")]
        public virtual string AlarmCategoryName { get; set; }

        [SmfDisplayAttribute(typeof(VM_AlarmItem), "AlarmType", "NAME_AlarmType")]
        public virtual int AlarmType { get; set; }

        [SmfDisplayAttribute(typeof(VM_AlarmItem), "ToConfirm", "NAME_ToConfirm")]
        public virtual bool ToConfirm { get; set; }

       // public virtual String AlarmDateJs { get; set; }

    }
}
