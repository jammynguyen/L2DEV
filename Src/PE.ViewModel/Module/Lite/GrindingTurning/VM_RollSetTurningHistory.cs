using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PE.DbEntity.Models;
using PE.HMIWWW.Core.ViewModel;
using SMF.HMIWWW.Attributes;
using SMF.HMIWWW.UnitConverter;

namespace PE.HMIWWW.ViewModel.Module.Lite.GrindingTurning
{
  public class VM_RollSetTurningHistory : VM_Base
  {
    #region properties
    [SmfDisplayAttribute(typeof(VM_RollSetTurningHistory), "RollSetId", "NAME_RollSetName")]
    public virtual long RollSetId { get; set; }

    [SmfDisplayAttribute(typeof(VM_RollSetTurningHistory), "RollSetName", "NAME_RollSetName")]
    public virtual string RollSetName { get; set; }

    [SmfDisplayAttribute(typeof(VM_RollSetTurningHistory), "RollSetStatus", "NAME_RollsetStatus")]
    public virtual short RollSetStatus { get; set; }

    [SmfDisplayAttribute(typeof(VM_RollSetTurningHistory), "RollSetType", "NAME_RollsetType")]
    public virtual short RollSetType { get; set; }

    public virtual long RollSetHistoryId { get; set; }

    [SmfDisplayAttribute(typeof(VM_RollSetTurningHistory), "Mounted", "NAME_Mounted")]
    public virtual DateTime? Mounted { get; set; }

    [SmfDisplayAttribute(typeof(VM_RollSetTurningHistory), "Dismounted", "NAME_Dismounted")]
    public virtual DateTime? Dismounted { get; set; }

    [SmfDisplayAttribute(typeof(VM_RollSetTurningHistory), "RollSetHistoryStatus", "NAME_RollsetHistoryStatus")]
    public virtual short RollSetHistoryStatus { get; set; }

    public virtual long ActualUpperRollId { get; set; }

    public virtual long ActualBottomRollId { get; set; }

    public virtual long ActualThirdRollId { get; set; }

    public virtual List<VM_GroovesRoll> GrooveActualRollUpper { get; set; }

    public virtual List<VM_GroovesRoll> GrooveActualRollBottom { get; set; }

    public virtual List<VM_GroovesRoll> GrooveActualRollThird { get; set; }

    [SmfDisplayAttribute(typeof(VM_RollSetTurningHistory), "ActualUpperRollName", "NAME_RollUpperName")]
    public virtual string ActualUpperRollName { get; set; }

    [SmfDisplayAttribute(typeof(VM_RollSetTurningHistory), "ActualBottomRollName", "NAME_RollBottomName")]
    public virtual string ActualBottomRollName { get; set; }

    [SmfDisplayAttribute(typeof(VM_RollSetTurningHistory), "ActualThirdRollName", "NAME_RollThirdName")]
    public virtual string ActualThirdRollName { get; set; }

    [SmfDisplayAttribute(typeof(VM_RollSetTurningHistory), "ActualUpperDiameter", "NAME_DiameterUpper")]
    [SmfFormatAttribute("FORMAT_Diameter")]
    [SmfUnitAttribute("UNIT_Diameter")]
    public virtual double ActualUpperDiameter { get; set; }

    [SmfDisplayAttribute(typeof(VM_RollSetTurningHistory), "ActualBottomDiameter", "NAME_DiameterBottom")]
    [SmfFormatAttribute("FORMAT_Diameter")]
    [SmfUnitAttribute("UNIT_Diameter")]
    public virtual double ActualBottomDiameter { get; set; }

    [SmfDisplayAttribute(typeof(VM_RollSetTurningHistory), "ActualThirdDiameter", "NAME_DiameterThird")]
    [SmfFormatAttribute("FORMAT_Diameter")]
    [SmfUnitAttribute("UNIT_Diameter")]
    public virtual double ActualThirdDiameter { get; set; }

    [SmfDisplayAttribute(typeof(VM_RollSetTurningHistory), "AccWeight", "NAME_AccWeight")]
    [SmfUnitAttribute("UNIT_WeightTons")]
    [SmfFormatAttribute("FORMAT_Weight")]
    public double AccWeight { get; set; }

    [SmfDisplayAttribute(typeof(VM_RollSetTurningHistory), "AccBilletCnt", "NAME_AccBilletCnt")]
    public long AccBilletCnt { get; set; }

    [SmfDisplayAttribute(typeof(VM_RollSetTurningHistory), "GrooveTemplateName", "NAME_GrooveTemplateName")]
    public string GrooveTemplateName { get; set; }

    [SmfDisplayAttribute(typeof(VM_RollSetTurningHistory), "RollSetHistoryName", "NAME_RollSetHistoryName")]
    public string RollSetHistoryName { get; set; }

    #endregion
    #region ctor
    public VM_RollSetTurningHistory()
    {
    }
    public VM_RollSetTurningHistory(V_RollSetOverview rso, IList<V_RollHistory> upperGroove, IList<V_RollHistory> bottomGroove, IList<V_RollHistory> thirdGroove)
    {
      this.RollSetId = rso.RollSetId;
      this.RollSetName = rso.RollSetName;
      this.RollSetStatus = (short)rso.RollSetStatus;
      this.RollSetType = rso.RollSetType;
      this.RollSetHistoryId = (short)rso.RollSetHistoryId;
      this.RollSetHistoryStatus = (short)rso.RollSetHistoryStatus;
      this.Mounted = rso.Mounted;
      this.Dismounted = rso.Dismounted;
      this.ActualUpperRollId = rso.RollIdUpper ?? 0;
      this.ActualBottomRollId = rso.RollIdBottom ?? 0;
      this.ActualThirdRollId = rso.RollIdThird ?? 0;
      this.ActualUpperRollName = rso.RollNameUpper;
      this.ActualBottomRollName = rso.RollNameBottom;
      this.ActualThirdRollName = rso.RollNameThird;
      //this.ActualUpperDiameter = upperGroove.DiameterUpper ?? 0;
      //this.ActualBottomDiameter = rso.DiameterBottom ?? 0;
      //this.ActualThirdDiameter = rso.DiameterThird ?? 0;
      this.GrooveActualRollUpper = new List<VM_GroovesRoll>();
      foreach (V_RollHistory upperHistoryListElement in upperGroove)
      {
        this.GrooveActualRollUpper.Add(new VM_GroovesRoll(upperHistoryListElement));
        this.ActualUpperDiameter = upperHistoryListElement.ActualDiameter;
      }
      this.GrooveActualRollBottom = new List<VM_GroovesRoll>();
      foreach (V_RollHistory bottomHistoryListElement in bottomGroove)
      {
        this.GrooveActualRollBottom.Add(new VM_GroovesRoll(bottomHistoryListElement));
        this.ActualBottomDiameter = bottomHistoryListElement.ActualDiameter;
      }
      this.GrooveActualRollThird = new List<VM_GroovesRoll>();
      foreach (V_RollHistory thirdHistoryListElement in thirdGroove)
      {
        this.GrooveActualRollThird.Add(new VM_GroovesRoll(thirdHistoryListElement));
        this.ActualThirdDiameter = thirdHistoryListElement.ActualDiameter;
      }
      this.GrooveTemplateName = "";
      this.RollSetHistoryName = "";
      UnitConverterHelper.ConvertToLocal<VM_RollSetTurningHistory>(this);
    }

    public VM_RollSetTurningHistory(V_RollSetOverview rso, IList<V_RollHistory> upperGroove, IList<V_RollHistory> bottomGroove)
    {
      this.RollSetId = rso.RollSetId;
      this.RollSetName = rso.RollSetName;
      this.RollSetStatus = (short)rso.RollSetStatus;
      this.RollSetType = rso.RollSetType;
      this.RollSetHistoryId = (short)rso.RollSetHistoryId;
      this.RollSetHistoryStatus = (short)rso.RollSetHistoryStatus;
      this.Mounted = rso.Mounted;
      this.Dismounted = rso.Dismounted;
      this.ActualUpperRollId = rso.RollIdUpper ?? 0;
      this.ActualBottomRollId = rso.RollIdBottom ?? 0;
      this.ActualThirdRollId = rso.RollIdThird ?? 0;
      this.ActualUpperRollName = rso.RollNameUpper;
      this.ActualBottomRollName = rso.RollNameBottom;
      this.ActualThirdRollName = rso.RollNameThird;
      //this.ActualUpperDiameter = upperGroove.DiameterUpper ?? 0;
      //this.ActualBottomDiameter = rso.DiameterBottom ?? 0;
      //this.ActualThirdDiameter = rso.DiameterThird ?? 0;
      this.GrooveActualRollUpper = new List<VM_GroovesRoll>();
      foreach (V_RollHistory upperHistoryListElement in upperGroove)
      {
        this.GrooveActualRollUpper.Add(new VM_GroovesRoll(upperHistoryListElement));
        this.ActualUpperDiameter = upperHistoryListElement.ActualDiameter;
      }
      this.GrooveActualRollBottom = new List<VM_GroovesRoll>();
      foreach (V_RollHistory bottomHistoryListElement in bottomGroove)
      {
        this.GrooveActualRollBottom.Add(new VM_GroovesRoll(bottomHistoryListElement));
        this.ActualBottomDiameter = bottomHistoryListElement.ActualDiameter;
      }
      this.GrooveActualRollThird = new List<VM_GroovesRoll>();

      this.GrooveTemplateName = "";
      this.RollSetHistoryName = "";
      UnitConverterHelper.ConvertToLocal<VM_RollSetTurningHistory>(this);
    }

    public VM_RollSetTurningHistory(V_RollsetOverviewNewest rso, IList<V_RollHistory> upperGroove, IList<V_RollHistory> bottomGroove, IList<V_RollHistory> thirdGroove)
    {
      this.RollSetId = rso.RollSetId;
      this.RollSetName = rso.RollSetName;
      this.RollSetStatus = (short)rso.RollSetStatus;
      this.RollSetType = rso.RollSetType;
      this.RollSetHistoryId = (short)rso.RollSetHistoryId;
      this.RollSetHistoryStatus = (short)rso.RollSetHistoryStatus;
      this.Mounted = rso.Mounted;
      this.Dismounted = rso.Dismounted;
      this.ActualUpperRollId = rso.RollIdUpper ?? 0;
      this.ActualBottomRollId = rso.RollIdBottom ?? 0;
      this.ActualThirdRollId = rso.RollIdThird ?? 0;
      this.ActualUpperRollName = rso.RollNameUpper;
      this.ActualBottomRollName = rso.RollNameBottom;
      this.ActualThirdRollName = rso.RollNameThird;
      this.ActualUpperDiameter = rso.DiameterUpper ?? 0;
      this.ActualBottomDiameter = rso.DiameterBottom ?? 0;
      this.ActualThirdDiameter = rso.DiameterThird ?? 0;
      this.GrooveActualRollUpper = new List<VM_GroovesRoll>();
      foreach (V_RollHistory upperHistoryListElement in upperGroove)
      {
        this.GrooveActualRollUpper.Add(new VM_GroovesRoll(upperHistoryListElement));
      }
      this.GrooveActualRollBottom = new List<VM_GroovesRoll>();
      foreach (V_RollHistory bottomHistoryListElement in bottomGroove)
      {
        this.GrooveActualRollBottom.Add(new VM_GroovesRoll(bottomHistoryListElement));
      }
      this.GrooveActualRollThird = new List<VM_GroovesRoll>();
      foreach (V_RollHistory thirdHistoryListElement in thirdGroove)
      {
        this.GrooveActualRollThird.Add(new VM_GroovesRoll(thirdHistoryListElement));
      }
      this.GrooveTemplateName = "";
      this.RollSetHistoryName = "";
      UnitConverterHelper.ConvertToLocal<VM_RollSetTurningHistory>(this);
    }

    public VM_RollSetTurningHistory(V_RollsetOverviewNewest rso, IList<V_RollHistory> upperGroove, IList<V_RollHistory> bottomGroove)
    {
      this.RollSetId = rso.RollSetId;
      this.RollSetName = rso.RollSetName;
      this.RollSetStatus = (short)rso.RollSetStatus;
      this.RollSetType = rso.RollSetType;
      this.RollSetHistoryId = (short)rso.RollSetHistoryId;
      this.RollSetHistoryStatus = (short)rso.RollSetHistoryStatus;
      this.Mounted = rso.Mounted;
      this.Dismounted = rso.Dismounted;
      this.ActualUpperRollId = rso.RollIdUpper ?? 0;
      this.ActualBottomRollId = rso.RollIdBottom ?? 0;
      this.ActualThirdRollId = rso.RollIdThird ?? 0;
      this.ActualUpperRollName = rso.RollNameUpper;
      this.ActualBottomRollName = rso.RollNameBottom;
      this.ActualThirdRollName = rso.RollNameThird;
      this.ActualUpperDiameter = rso.DiameterUpper ?? 0;
      this.ActualBottomDiameter = rso.DiameterBottom ?? 0;
      this.ActualThirdDiameter = rso.DiameterThird ?? 0;
      this.GrooveActualRollUpper = new List<VM_GroovesRoll>();
      foreach (V_RollHistory upperHistoryListElement in upperGroove)
      {
        this.GrooveActualRollUpper.Add(new VM_GroovesRoll(upperHistoryListElement));
      }
      this.GrooveActualRollBottom = new List<VM_GroovesRoll>();
      foreach (V_RollHistory bottomHistoryListElement in bottomGroove)
      {
        this.GrooveActualRollBottom.Add(new VM_GroovesRoll(bottomHistoryListElement));
      }
      this.GrooveActualRollThird = new List<VM_GroovesRoll>();

      this.GrooveTemplateName = "";
      this.RollSetHistoryName = "";
      UnitConverterHelper.ConvertToLocal<VM_RollSetTurningHistory>(this);
    }
    #endregion
  }
}
