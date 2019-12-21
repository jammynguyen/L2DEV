using PE.DbEntity.Models;
using PE.HMIWWW.Core.ViewModel;
using SMF.HMIWWW.Attributes;
using SMF.HMIWWW.UnitConverter;
using System.ComponentModel.DataAnnotations;

namespace PE.HMIWWW.ViewModel.System
{
  public class VM_L3TransferTableGeneral : VM_Base
  {
    #region properties
    [Editable(false)]
    [SmfDisplayAttribute(typeof(VM_L3TransferTableGeneral), "TableName", "NAME_TableName")]
    public virtual string TableName { get; set; }

    [Editable(false)]
    [SmfDisplayAttribute(typeof(VM_L3TransferTableGeneral), "New", "ENUM_COMMSTATUS_New")]
    public virtual long? New { get; set; }

    [Editable(false)]
    [SmfDisplayAttribute(typeof(VM_L3TransferTableGeneral), "BeingProcessed", "ENUM_COMMSTATUS_BeingProcessed")]
    public virtual long? BeingProcessed { get; set; }

    [Editable(false)]
    [SmfDisplayAttribute(typeof(VM_L3TransferTableGeneral), "OK", "ENUM_COMMSTATUS_OK")]
    public virtual long? OK { get; set; }

    [Editable(false)]
    [SmfDisplayAttribute(typeof(VM_L3TransferTableGeneral), "ValidationError", "ENUM_COMMSTATUS_ValidationError")]
    public virtual long? ValidationError { get; set; }

    [Editable(false)]
    [SmfDisplayAttribute(typeof(VM_L3TransferTableGeneral), "ProcessingError", "ENUM_COMMSTATUS_ProcessingError")]
    public virtual long? ProcessingError { get; set; }
    #endregion

    #region ctor
    public VM_L3TransferTableGeneral() { }
    public VM_L3TransferTableGeneral(V_L3L2TransferTablesSummary l3L2TransferTablesSummary)
    {
      this.TableName = l3L2TransferTablesSummary.TransferTableName;
      this.New = l3L2TransferTablesSummary.StatusNew;
      this.BeingProcessed = l3L2TransferTablesSummary.StatusInProc;
      this.OK = l3L2TransferTablesSummary.StatusOK;
      this.ValidationError = l3L2TransferTablesSummary.StatusValErr;
      this.ProcessingError = l3L2TransferTablesSummary.StatusProcErr;

      UnitConverterHelper.ConvertToLocal(this);
    }
    #endregion
  }
}
