using PE.DbEntity.Model;
using PE.DbEntity.Models;
using PE.HMIWWW.Core.Resources;
using PE.HMIWWW.Core.ViewModel;
using SMF.DbEntity.Dto;
using SMF.DbEntity.Model;
//using SMF.HMI.Module.AlarmStorage;
using SMF.HMIWWW.Attributes;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace PE.HMIWWW.ViewModel.System
{

    public class VM_AlarmShortItem: VM_Base
    {
        public VM_AlarmShortItem() { }

        public VM_AlarmShortItem(SMF.DbEntity.Model.Alarm alarm)
        {
            Id = alarm.AlarmId;
            Date = alarm.AlarmDate.ToString("yyyy-MM-dd hh:mm:ss");
            Message = alarm.Message;
            Source = alarm.AlarmOwner;
            Confirmation = alarm.Confirmation;
            ConfirmationDate = alarm.ConfirmationDate;
            ConfirmedBy = alarm.ConfirmedBy;
            Category = alarm.AlarmCategoryName;
            AlarmType = alarm.AlarmType;
            // Type = alarm.AlarmType;
            Type = ResourceController.GetEnumValue("AlarmType", alarm.AlarmType);
            // AlarmDateJs = alarm.AlarmDate.ToString();
            ToConfirm = alarm.ToConfirm;
        }
        public VM_AlarmShortItem(V_NewestAlarmsList alarm)
        {
          Id = alarm.AlarmId;
          Date = alarm.AlarmDate.ToString(new CultureInfo("it-IT"));
          Message = alarm.Message;
          Source = alarm.AlarmOwner;
          Confirmation = alarm.Confirmation;
          ConfirmationDate = alarm.ConfirmationDate;
          ConfirmedBy = alarm.ConfirmedBy;
          Category = alarm.AlarmCategoryName;
          AlarmType = alarm.AlarmType;
          //Type = alarm.AlarmType;
          Type = ResourceController.GetEnumValue("AlarmType", alarm.AlarmType);
          // AlarmDateJs = alarm.AlarmDate.ToString();
          ToConfirm = alarm.ToConfirm;
          RowNumber = Convert.ToInt64(alarm.RowNumber);
        }
    

        [SmfDisplayAttribute(typeof(VM_AlarmItem), "Id", "NAME_Id")]
        public virtual Int64 Id { get; set; }

        [SmfDisplayAttribute(typeof(VM_AlarmItem), "Date", "NAME_AlarmDate")]
        public virtual string Date { get; set; }

        [SmfDisplayAttribute(typeof(VM_AlarmItem), "Message", "NAME_Message")]
        public virtual String Message { get; set; }

        [SmfDisplayAttribute(typeof(VM_AlarmItem), "Source", "NAME_AlarmOwner")]
        public virtual String Source { get; set; }

        [SmfDisplayAttribute(typeof(VM_AlarmItem), "Confirmation", "NAME_Confirmation")]
        public virtual Boolean? Confirmation { get; set; }

        [SmfDisplayAttribute(typeof(VM_AlarmItem), "ConfirmationDate", "NAME_ConfirmationDate")]
        public virtual DateTime? ConfirmationDate { get; set; }

        [SmfDisplayAttribute(typeof(VM_AlarmItem), "ConfirmedBy", "NAME_ConfirmedBy")]
        public virtual String ConfirmedBy { get; set; }

        //[SmfDisplayAttribute(typeof(VM_AlarmItem), "AlarmTypeId", "NAME_AlarmTypeId")]
        //public virtual int AlarmTypeId { get; set; }

        [SmfDisplayAttribute(typeof(VM_AlarmItem), "Category", "NAME_AlarmCategoryName")]
        public virtual string Category { get; set; }

        [SmfDisplayAttribute(typeof(VM_AlarmItem), "Type", "NAME_AlarmType")]
        public virtual string Type { get; set; }

        [SmfDisplayAttribute(typeof(VM_AlarmItem), "ToConfirm", "NAME_ToConfirm")]
        public virtual bool ToConfirm { get; set; }
        public virtual long RowNumber { get; set; }
        public virtual int AlarmType { get; set; }

    // public virtual String AlarmDateJs { get; set; }

  }
}
