using PE.DbEntity.Enums;
using PE.DbEntity.Models;
using PE.DTO.Internal.MVHistory;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PE.MVH.Handlers
{
  public class MeasurementsHandler
  {

    public async Task<long> ConvertExternalFeatureCodesToInternal(PEContext ctx, DCMeasData message)
    {
      long baseFeature = 0;

      baseFeature =
                                   await ctx.MVHFeatures
                                        .Where(w => w.FeatureCode == message.EventCode)
                                        .Select(s => s.FeatureId)
                                        .SingleAsync();
                              
      return baseFeature;
    }
    public async Task<MVHFeature> GetFeature(PEContext ctx, int featureCode)
    {
      return await ctx.MVHFeatures.Where(w => w.FeatureCode == featureCode).SingleOrDefaultAsync();
    }
    public MVHFeature GetFeatureOfType(PEContext ctx, FeatureType featureType)
    {
      return  ctx.MVHFeatures.Where(w => w.EnumFeatureType == featureType).SingleOrDefault();
    }
    public async Task<FeatureType> GetFeatureType(PEContext ctx, DCMeasData message)
    {
      MVHFeature feature = await GetFeature(ctx, message.EventCode);
      if (feature != null)
        return feature.EnumFeatureType;
      else
        return FeatureType.Undefined;
    }

    /// <summary>
    /// Function process each element of MeasDataParcel and convert external id on internal
    /// </summary>
    /// <param name="ctx"></param>
    /// <param name="message">DCMeasDataParcel</param>
    /// <returns></returns>
    public List<long> ConvertExternalIdsOnInternal(PEContext ctx, ref DCMeasDataParcel message)
    {
      List<long> listOfmaterialsInPackage = new List<long>();
      Dictionary<long, long> dictionaryOfMaterialsIds = new Dictionary<long, long>();

      try
      {
        foreach (DCMeasDataSample meas in message.Measurements)
        {
          if (dictionaryOfMaterialsIds.ContainsKey(meas.BasId))
          {
            dictionaryOfMaterialsIds.TryGetValue(meas.BasId, out long internalRawMaterialId);
            meas.BasId = (uint)internalRawMaterialId;
          }
          else
          {
            long? internalRawMaterialId = ctx.MVHRawMaterials.Where(w => w.ExternalRawMaterialId == meas.BasId).Select(s => s.RawMaterialId).Single();
            dictionaryOfMaterialsIds.Add(meas.BasId, internalRawMaterialId.Value);
            meas.BasId = (uint)internalRawMaterialId.Value;
            listOfmaterialsInPackage.Add(meas.BasId);
          }
        }
      }
      catch (Exception)
      {
        throw new Exception() { Source = "ConvertMeasurementsExternalRawMaterialsIdOnInternal" };
      }
      dictionaryOfMaterialsIds.Clear();
      return listOfmaterialsInPackage;
    }



    public void SaveMeasurement(PEContext ctx, DCMeasData message)
    {
     
      ctx.MVHMeasurements.Add(ProcessMeasurementBeforeSave(ctx, message, ConvertExternalFeatureCodesToInternal(ctx, message).Result));
    }

    /// <summary>
    /// Convert DCMeasDataParcel into database elements structure
    /// </summary>
    /// <param name="ctx"></param>
    /// <param name="message"></param>
    /// <returns></returns>
    public MVHMeasurement ProcessMeasurementBeforeSave(PEContext ctx, DCMeasData message, long FKFeatureId)
    {
      MVHMeasurement measurement = new MVHMeasurement()
      {
        CreatedTs = DateTime.Now,
        IsLastPass = message.IsLastPass,
        FKFeatureId = /*message.BaseFeature*/FKFeatureId,
        PassNo = (short)message.PassNumber,
        IsValid = Convert.ToBoolean(message.Valid),
        ValueAvg = Math.Round(Convert.ToDouble(message.Avg), 2),
        ValueMax = Math.Round(Convert.ToDouble(message.Max), 2),
        ValueMin = Math.Round(Convert.ToDouble(message.Min), 2),
        //FKRawMaterialId = message.BasId,
        //FKRawMaterialId = internalMaterialId,
      };

			if(message.BasId!=0)// in case if measurement is material related
      {
        long internalMaterialId = ctx.MVHRawMaterials.Where(w => w.ExternalRawMaterialId == message.BasId).Select(s => s.RawMaterialId).Single();
        measurement.FKRawMaterialId = internalMaterialId;
      }

      if (message is DCMeasDataSample)
      {
        measurement.FirstMeasurement = (message as DCMeasDataSample).DateTimeOfFirstSample;
        measurement.NoOfSamples = (short)(message as DCMeasDataSample).NumberOfSamples;
      }
      return measurement;
    }
  }
}
