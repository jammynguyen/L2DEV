using PE.DbEntity.Models;
using PE.HMIWWW.Core.ViewModel;
using SMF.HMIWWW.Attributes;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PE.HMIWWW.ViewModel.Module.Lite.ConsumptionMonitoring
{
  public class VM_Measurement : VM_Base
  {
    [SmfDisplay(typeof(VM_Measurement), "MeasurementTime", "NAME_MeasurementTime")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public DateTime MeasurementTime { get; set; }

    [SmfDisplay(typeof(VM_Measurement), "MeasurementValueAvg", "NAME_MeasurementValueAvg")]
    [DisplayFormat(NullDisplayText = "-;", HtmlEncode = false)]
    public double MeasurementValueAvg { get; set; }

    public VM_Measurement()
    {
    }

    public VM_Measurement(V_Measurements measurement)
    {
      this.MeasurementValueAvg = measurement.MeasurementValueAvg;
      this.MeasurementTime = measurement.MeasurementTime;

      ConvertToLocal(this);

    }
  }
}
