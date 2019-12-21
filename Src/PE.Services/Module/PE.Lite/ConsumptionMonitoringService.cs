using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using PE.DbEntity.Models;
using PE.HMIWWW.Core.Resources;
using PE.HMIWWW.Core.Service;
using PE.HMIWWW.Core.ViewModel;
using PE.HMIWWW.Services.Module.PE.Lite.Interfaces;
using System.Linq;
using System.Web.Mvc;
using System.Data.Entity;
using System.IO;
using PE.DTO.Internal.ZebraPrinter;
using SMF.Module.Core;
using PE.HMIWWW.Core.Communication;
using System.Threading.Tasks;
using System;
using SMF.RepositoryPatternExt;
using SMF.Module.Notification;
using System.Reflection;
using System.Text;
using System.Drawing;
using PE.HMIWWW.ViewModel.Module.Lite.ConsumptionMonitoring;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace PE.HMIWWW.Services.Module.PE.Lite
{
  public class ConsumptionMonitoringService : BaseService, IConsumptionMonitoringService
  {
    public DataSourceResult GetFeaturesList(ModelStateDictionary modelState, DataSourceRequest request)
    {
      using (PEContext ctx = new PEContext())
      {
        IQueryable<V_FeaturesMap> features = ctx.V_FeaturesMap
          .Where(x => x.EnumFeatureType == 9)
          .AsQueryable();
        return features.ToDataSourceResult<V_FeaturesMap, VM_Feature>(request, modelState, data => new VM_Feature(data));
      }
    }

    public VM_Feature GetFeatureDetails(ModelStateDictionary modelState, long featureId)
    {
      VM_Feature result = null;

      if (featureId <= 0)
      {
        AddModelStateError(modelState, ResourceController.GetErrorText("BadRequestParameters"));
      }
      // Validate entry data
      if (!modelState.IsValid)
      {
        return result;
      }

      using (PEContext ctx = new PEContext())
      {
        V_FeaturesMap feature = ctx.V_FeaturesMap
          .Where(x => x.FeatureId == featureId)
          .Single();

        result = new VM_Feature(feature);
      }

      return result;
    }

    public DataSourceResult GetMeasurementData(ModelStateDictionary modelState, DataSourceRequest request, long featureId, DateTime dateFrom, DateTime dateTo)
    {
      int MAX_POINTS_CONST = 500;

      VM_Measurement[] result_array = new VM_Measurement[MAX_POINTS_CONST];

      using (PEContext ctx = new PEContext())
      {
        List<V_Measurements> measurements = ctx.V_Measurements
          .Where(x => x.FeatureId == featureId && x.MeasurementTime >= dateFrom && x.MeasurementTime <= dateTo && x.IsValid == true)
          .OrderBy(x => x.MeasurementTime)
          .ToList();

        int MeasurementsCount = measurements.Count();

        if (MeasurementsCount > MAX_POINTS_CONST)
        {
          double SamplingRatio = (double)MeasurementsCount / (double)MAX_POINTS_CONST;

          double skipMeasurementsCounter = 0;
          double groupSize = 0;
          for (int groupCounter = 0; groupCounter < MAX_POINTS_CONST; skipMeasurementsCounter += groupSize)
          {
            groupSize = (SamplingRatio * (groupCounter + 1)) - Math.Floor(skipMeasurementsCounter);

            List<V_Measurements> group = measurements.Skip((int)Math.Floor(skipMeasurementsCounter)).Take((int)Math.Floor(groupSize)).ToList();
            
            result_array[groupCounter++] = new VM_Measurement(new V_Measurements
            {
              MeasurementValueAvg = Math.Round(group.Select(x => x.MeasurementValueAvg).Average(), 2),
              MeasurementTime = new DateTime((long)Math.Round(new long[] { group.First().MeasurementTime.Ticks, group.Last().MeasurementTime.Ticks }.Average())),
            });
          }
        }
        else
        {
            result_array = measurements.Select(x => new VM_Measurement(x))
            .ToArray();
        }
      }
      return result_array.ToDataSourceResult(request);
    }
  }
}
