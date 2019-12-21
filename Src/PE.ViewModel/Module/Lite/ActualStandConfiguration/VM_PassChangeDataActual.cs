using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PE.DbEntity.Models;
using PE.HMIWWW.Core.ViewModel;
using PE.HMIWWW.ViewModel.Module.Lite.RollsetManagement;
using SMF.HMIWWW.Attributes;
using SMF.HMIWWW.UnitConverter;

namespace PE.HMIWWW.ViewModel.Module.Lite.ActualStandConfiguration
{
  public class VM_PassChangeDataActual : VM_Base
  {
    #region properties
    public virtual long Id { get; set; }

    [SmfDisplayAttribute(typeof(VM_PassChangeDataActual), "StandNo", "NAME_StandNo")]
    public virtual short StandNo { get; set; }

    [SmfDisplayAttribute(typeof(VM_PassChangeDataActual), "StandStatus", "NAME_Status")]
    public virtual short? StandStatus { get; set; }

    [SmfDisplayAttribute(typeof(VM_PassChangeDataActual), "RollSetId", "NAME_RollSetName")]
    public virtual long? RollSetId { get; set; }

    [SmfDisplayAttribute(typeof(VM_PassChangeDataActual), "RollSetName", "NAME_RollSetName")]
    public virtual string RollSetName { get; set; }

    public virtual long? RollSetHistoryId { get; set; }

    [SmfDisplayAttribute(typeof(VM_PassChangeDataActual), "Mounted", "NAME_Mounted")]
    public virtual DateTime? Mounted { get; set; }

    [SmfDisplayAttribute(typeof(VM_PassChangeDataActual), "CassetteName", "NAME_CassetteName")]
    public virtual string CassetteName { get; set; }

    [SmfDisplayAttribute(typeof(VM_PassChangeDataActual), "CassetteId", "NAME_CassetteName")]
    public virtual long? CassetteId { get; set; }

    [SmfDisplayAttribute(typeof(VM_PassChangeDataActual), "PositionInCassette", "NAME_PositionInCassette")]
    public virtual short? PositionInCassette { get; set; }

    [SmfDisplayAttribute(typeof(VM_PassChangeDataActual), "Arrangement", "NAME_Arrangement")]
    public virtual short? Arrangement { get; set; }

    [SmfDisplayAttribute(typeof(VM_PassChangeDataActual), "AccBilletCntLimit", "NAME_AccBilletCntLimit_ToTable")]
    public virtual long? AccBilletCntLimit { get; set; }

    [SmfDisplayAttribute(typeof(VM_PassChangeDataActual), "AccBilletCnt", "NAME_AccBilletCnt_ToTable")]
    public virtual long AccBilletCnt { get; set; }

    [SmfDisplayAttribute(typeof(VM_PassChangeDataActual), "AccWeight", "NAME_AccWeight_ToTable")]
    [SmfUnitAttribute("UNIT_WeightTons")]
    [SmfFormatAttribute("FORMAT_Weight")]
    public virtual double? AccWeight { get; set; }

    [SmfDisplayAttribute(typeof(VM_PassChangeDataActual), "AccWeightLimit", "NAME_AccWeightLimit_ToTable")]
    [SmfUnitAttribute("UNIT_WeightTons")]
    [SmfFormatAttribute("FORMAT_Weight")]
    public virtual double? AccWeightLimit { get; set; }

    [SmfDisplayAttribute(typeof(VM_PassChangeDataActual), "RollTypeName", "NAME_RollTypeName")]
    public virtual string RollTypeName { get; set; }

    [SmfDisplayAttribute(typeof(VM_RollSetOverviewFull), "RollSetType", "NAME_RollsetType")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual short? RollSetType { get; set; }

    [SmfDisplayAttribute(typeof(VM_PassChangeDataActual), "RollSetHistoryStatus", "NAME_RollsetHistoryStatus")]
    public virtual short RollSetHistoryStatus { get; set; }

    [SmfDisplayAttribute(typeof(VM_PassChangeDataActual), "ActualDiameter", "NAME_DiameterActual")]
    [SmfFormatAttribute("FORMAT_Diameter")]
    //[SmfRangeAttribute(typeof(VM_PassChangeDataActual), "ActualDiameter", "RANGE_MIN_Diameter", "RANGE_MAX_Diameter")]
    [SmfUnitAttribute("UNIT_Diameter")]
    public virtual double? ActualDiameter { get; set; }

    [SmfDisplayAttribute(typeof(VM_PassChangeDataActual), "GrooveNumber", "NAME_AccWeight")]
    public virtual int GrooveNumber { get; set; }

    [SmfDisplayAttribute(typeof(VM_PassChangeDataActual), "GrooveTemplateId", "NAME_GrooveTemplateId")]
    public virtual long GrooveTemplateId { get; set; }

    [SmfDisplayAttribute(typeof(VM_PassChangeDataActual), "GrooveTemplateName", "NAME_GrooveTemplateName")]
    public virtual string GrooveTemplateName { get; set; }

    [SmfDisplayAttribute(typeof(VM_PassChangeDataActual), "GrooveStatus", "NAME_GrooveStatus")]
    public virtual short GrooveStatus { get; set; }

    [SmfDisplayAttribute(typeof(VM_PassChangeDataActual), "AccBilletCntRatio", "NAME_AccBilletCntRatio_ToTable")]
    [SmfFormatAttribute("FORMAT_Percent")]
    [SmfUnitAttribute("UNIT_Percent")]
    public virtual double? AccBilletCntRatio { get; set; }

    [SmfDisplayAttribute(typeof(VM_PassChangeDataActual), "AccWeightRatio", "NAME_AccWeightRatio_ToTable")]
    [SmfFormatAttribute("FORMAT_Percent")]
    [SmfUnitAttribute("UNIT_Percent")]
    public virtual double? AccWeightRatio { get; set; }

    [SmfDisplayAttribute(typeof(VM_PassChangeDataActual), "EstimatedPassChangeTime", "NAME_EstimatedProdDate")]
    public DateTime? EstimatedPassChangeTime { get; set; }

    [SmfDisplayAttribute(typeof(VM_PassChangeDataActual), "Position", "NAME_Position")]
    public virtual short? Position { get; set; }

    #endregion
    #region ctor
    public VM_PassChangeDataActual()
    {
    }

    public VM_PassChangeDataActual(V_PassChangeDataActual pcda)
    {
      this.Id = pcda.StandId;
      this.StandNo = pcda.StandNo;
      this.StandStatus = (short?)pcda.StandStatus;
      this.RollSetId = pcda.RollSetId;
      this.RollSetName = (pcda.RollSetName != null) ? pcda.RollSetName : "";
      this.RollSetHistoryId = pcda.RollSetHistoryId;
      this.Mounted = pcda.Mounted;
      this.CassetteName = (pcda.CassetteName != null) ? pcda.CassetteName : "";
      this.CassetteId = (CassetteName != "") ? pcda.CassetteId : 0;
      this.PositionInCassette = pcda.PositionInCassette;
      this.Arrangement = (short?)pcda.Arrangement;
      this.AccBilletCnt = pcda.AccBilletCnt;
      this.AccBilletCntLimit = pcda.AccBilletCntLimit;
      this.AccWeight = pcda.AccWeight;
      this.AccWeightLimit = pcda.AccWeightLimit;
      this.RollTypeName = pcda.RollTypeName;
      this.RollSetHistoryStatus = pcda.Status;
      this.ActualDiameter = pcda.ActualDiameter;
      this.GrooveNumber = pcda.GrooveNumber;
      this.GrooveTemplateId = pcda.GrooveTemplateId;
      this.GrooveTemplateName = pcda.GrooveTemplateName;
      this.GrooveStatus = pcda.GrooveStatus;
      this.AccBilletCntRatio = (pcda.AccBilletCntLimit ?? 0.0) == 0 ? 0.0 : pcda.AccBilletCnt / pcda.AccBilletCntLimit.Value;
      this.AccWeightRatio = (pcda.AccWeightLimit ?? 0.0) == 0 ? 0.0 : pcda.AccWeight / pcda.AccWeightLimit.Value;
      this.Position = pcda.Position;
      this.RollSetType = pcda.RollSetType;


      UnitConverterHelper.ConvertToLocal<VM_PassChangeDataActual>(this);
    }
    #endregion
    #region public methods
    //not used in PE.Lite
    //public void CountEstimatedPassChangeTime(IList<VProductionSchedule> prodSchedule, long StdProdFinishTime, long StdProdTime, long StdBilletWeght, DateTime? actualDelayStart, long? actualDelayCatalogueId)
    //{
    //  double weightToChange = 0;
    //  if (this.AccWeightLimit != null && this.AccWeightLimit > 0 && this.AccWeightLimit > this.AccWeight)
    //  {
    //    weightToChange = (this.AccWeightLimit ?? 0.0) - this.AccWeight ?? 0;
    //  }

    //  long AverageBilletWeight = StdBilletWeght;
    //  long AverageBilletProductionTime = StdProdTime + StdProdFinishTime;

    //  bool flagFirstRecordProcessed = false;
    //  //long accumulatedScheduledDelayTime = 0;
    //  DateTime previousEstimatedProductReady = DateTime.Now;
    //  DateTime estimatedProductReady = DateTime.Now;

    //  if (actualDelayStart != null && actualDelayCatalogueId != null)
    //  {
    //    TimeSpan ts = DateTime.Now - (actualDelayStart ?? DateTime.Now);
    //    //accumulatedScheduledDelayTime -= (long)ts.TotalSeconds;
    //    //previousEstimatedProductReady = 
    //  }
    //  foreach (VProductionSchedule rec in prodSchedule)
    //  {
    //    //if difference ends, change groove
    //    weightToChange -= rec.Weight ?? AverageBilletWeight;
    //    if (weightToChange < 0)
    //    {
    //      this.EstimatedPassChangeTime = estimatedProductReady;
    //      return;
    //    }

    //    if (rec.BilletId != null) //regular material
    //    {
    //      switch (rec.Status)
    //      {
    //        case (short)PE.Core.Constants.MaterialStatus.Discharged:
    //        case (short)PE.Core.Constants.MaterialStatus.InMill:
    //        case (short)PE.Core.Constants.MaterialStatus.Rolled:
    //          {
    //            estimatedProductReady = DateTime.Now.AddSeconds(StdProdFinishTime);
    //            break;
    //          }
    //        case (short)PE.Core.Constants.MaterialStatus.Charged:
    //          {
    //            //calculate product ready time
    //            if (rec.Charged != null)
    //            {
    //              //calculate time difference between now and charged time
    //              TimeSpan FromChargeTillNow = DateTime.Now - (rec.Charged ?? DateTime.Now);
    //              //if billet charged for longer than max heating time

    //              //calculate estimated product ready time
    //              if (FromChargeTillNow.TotalSeconds > rec.TotalHeatingTime)
    //              {
    //                estimatedProductReady = previousEstimatedProductReady.AddSeconds(rec.StdProductionTime ?? 0.0);
    //              }
    //              else
    //              {
    //                long remainingHeatingTime = ((long)rec.TotalHeatingTime) - (long)FromChargeTillNow.TotalSeconds;
    //                if (remainingHeatingTime < 0)
    //                {
    //                  //delay lasts longer than scheduled, use std prod time
    //                  estimatedProductReady = previousEstimatedProductReady.AddSeconds(rec.StdProductionTime ?? 0.0);
    //                }
    //                else
    //                {
    //                  //material must still stay some time in furnace
    //                  //v1 from now + remamining heating time
    //                  DateTime dt1 = DateTime.Now.AddSeconds(remainingHeatingTime);
    //                  //v2 previousEstimatedProductReady + std production time
    //                  DateTime dt2 = previousEstimatedProductReady.AddSeconds(rec.StdProductionTime ?? 0);
    //                  //take late time for final estimated production time
    //                  if (dt1 > dt2)
    //                    estimatedProductReady = dt1;
    //                  else
    //                    estimatedProductReady = dt2;

    //                  //estimatedProductReady = previousEstimatedProductReady.AddSeconds(remainingHeatingTime).AddSeconds(rec.StdProductionTime ?? 0).AddSeconds(-prevReheatingTime);
    //                }
    //                //prevReheatingTime = remainingHeatingTime;
    //              }
    //            }
    //            else
    //            {
    //              estimatedProductReady = previousEstimatedProductReady.AddSeconds(rec.StdProductionTime ?? 0.0);
    //            }
    //            flagFirstRecordProcessed = true;
    //            break;
    //          }
    //        case (short)PE.Core.Constants.MaterialStatus.OnGrill:
    //        case (short)PE.Core.Constants.MaterialStatus.Unassigned:
    //        case (short)PE.Core.Constants.MaterialStatus.InBilletStorage:
    //          {
    //            if (flagFirstRecordProcessed)
    //            {
    //              estimatedProductReady = previousEstimatedProductReady.AddSeconds(rec.StdProductionTime ?? 0.0);
    //            }
    //            else
    //            {
    //              estimatedProductReady = previousEstimatedProductReady.AddSeconds(rec.StdProductionTime ?? 0.0).AddSeconds(rec.TotalHeatingTime ?? 0);
    //            }
    //            break;
    //          }
    //        default:
    //          {
    //            break;
    //          }
    //      }
    //    }
    //    else //scheduled delay
    //    {
    //      if (flagFirstRecordProcessed == false)
    //      {
    //        if (actualDelayStart != null && actualDelayCatalogueId != null)
    //        {
    //          TimeSpan ts = DateTime.Now - (actualDelayStart ?? DateTime.Now);
    //          long remainingStdDelayTime = (rec.StdDelayTime ?? 0) - (long)ts.TotalSeconds;
    //          if (remainingStdDelayTime < 0)
    //          {
    //            //delay lasts longer than scheduled, use std prod time
    //            estimatedProductReady = DateTime.Now.AddSeconds(rec.StdProductionTime ?? 0.0);
    //          }
    //          else
    //          {
    //            //delay lasts shorter than expected
    //            estimatedProductReady = DateTime.Now.AddSeconds(remainingStdDelayTime);
    //          }
    //        }
    //        else
    //        {
    //          estimatedProductReady = previousEstimatedProductReady.AddSeconds(rec.StdDelayTime ?? 0);
    //        }
    //      }
    //      else
    //      {
    //        estimatedProductReady = previousEstimatedProductReady.AddSeconds(rec.StdDelayTime ?? 0);
    //      }
    //      flagFirstRecordProcessed = true;
    //    }
    //    //calculate estimated production time
    //    estimatedProductReady = estimatedProductReady.AddSeconds(StdProdFinishTime);
    //    previousEstimatedProductReady = estimatedProductReady;
    //  }

    //  //calculate time based on average datas

    //  estimatedProductReady = estimatedProductReady.AddSeconds(((weightToChange / AverageBilletWeight) * AverageBilletProductionTime));

    //  this.EstimatedPassChangeTime = estimatedProductReady;
    //}
    #endregion
  }
}
