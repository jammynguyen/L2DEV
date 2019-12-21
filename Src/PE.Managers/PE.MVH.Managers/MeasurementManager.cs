using PE.DbEntity.Enums;
using PE.DbEntity.Models;
using PE.DTO.Internal.MVHistory;
using SMF.Core.DC;
using SMF.Module.Core;
using SMF.Module.Notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Entity;
using PE.Interfaces.Managers;
using PE.MVH.Handlers;
using PE.Interfaces.Interfaces.SendOffice;

namespace PE.MVH.Managers
{
  public class MeasurementManager : IMeasurementManager
  {
    #region members

    private IMeasValuesHistoryMeasurementSendOffice _sendOffice;
    private List<MVHFeature> _stepCreatingFeatures;
    private List<long> _lengthChangeFeatures;
    private List<long> _weightChangeFeatures;
   
    // private List<MaterialTriggers> materialTriggers;
    private Dictionary<MVHTrigger, List<MVHTriggersFeature>> _triggersDictionary;

    #endregion
    #region handlers

    private readonly RawMaterialHandler _rawMaterialHandler;
    private readonly MeasurementsHandler _measurementsHandler;
    #endregion
    #region ctor
    public MeasurementManager(IMeasValuesHistoryMeasurementSendOffice sendOffice)
    {
      _sendOffice = sendOffice;
      _rawMaterialHandler = new RawMaterialHandler();
      _measurementsHandler = new MeasurementsHandler();

      // _measurements = new List<DataContractBase>();
      _stepCreatingFeatures = new List<MVHFeature>();
      _lengthChangeFeatures = new List<long>();
      _weightChangeFeatures = new List<long>();
      //materialTriggers = new List<MaterialTriggers>();

      using (PEContext ctx = new PEContext())
      {
        try
        {
         

          _stepCreatingFeatures = ctx.MVHFeatures.Where(w => w.IsNewProcessingStep).Include(i => i.MVHFeaturesEXT).ToList();
          _lengthChangeFeatures = _stepCreatingFeatures.Where(w => w.MVHFeaturesEXT.IsLengthChange == true).Select(s => s.FeatureId).ToList();
          _weightChangeFeatures = _stepCreatingFeatures.Where(w => w.MVHFeaturesEXT.IsWeightChange == true).Select(s => s.FeatureId).ToList();
          
          {
            _triggersDictionary = new Dictionary<MVHTrigger, List<MVHTriggersFeature>>();

            List<MVHTrigger> triggersIds = ctx.MVHTriggers.Where(w => w.IsActive == true).ToList();

            foreach (MVHTrigger trigger in triggersIds)
            {
              _triggersDictionary.Add(trigger, ctx.MVHTriggersFeatures.Where(w => w.FKTriggerId == trigger.TriggerId).ToList());
            }

          }
        }
        catch (Exception)
        {
          NotificationController.RegisterAlarm(Defs.ManagerInitError, $"Manager triggered error on init");
          NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
          throw new Exception();
        }
      }
    }
    #endregion
    #region L1


    public virtual async Task<DataContractBase> ProcessMeasurementAsync<T>(T message) where T : DCMeasData
    {
      using (PEContext ctx = new PEContext())
      {
        try
        {
          if (message.BasId != 0)// if measurement is not consumption
          {
            await CreateGeneaologyAsync(ctx, message, _stepCreatingFeatures, _lengthChangeFeatures, _weightChangeFeatures);
          }
          _measurementsHandler.SaveMeasurement(ctx, message);

          await ctx.SaveChangesAsync();
          await _sendOffice.SendAssetEventAsync(message);
        }
        catch (Exception ex)
        {
          NotificationController.Error($"Exception: {ex.Message}");
        }
      }
      return new DataContractBase();
    }

    /////////// <summary>
    /////////// Send information to other modules that a trigger was hit
    /////////// </summary>
    /////////// <param name="triggerId">L2 id of trigger</param>
    ////////private async Task RequestNextMaterialAsync(FeatureType featureEnumType, long materialId)
    ////////{
    ////////  SendOfficeResult<DataContractBase> result = null;

    ////////  try
    ////////  {
    ////////    result = await _sendOffice.RequestNextMaterialAsync(new DCFeatureRelatedOperationData() { FeatureType = featureEnumType, RawMaterialId = materialId });

    ////////    if (result.OperationSuccess)
    ////////      NotificationController.Info("RequestNextMaterialAsync to PPL - success");
    ////////    else
    ////////      NotificationController.Error("SendTrRequestNextMaterialAsyncigger to PPL  - error");
    ////////  }
    ////////  catch
    ////////  {
    ////////    NotificationController.Error("RequestNextMaterialAsync to PPL  - error");
    ////////  }
    ////////}

    #endregion






    ///// <summary>
    ///// Check if any material triggerd specified in database is done
    ///// </summary>
    ///// <param name="message"></param>
    ///// <returns></returns>
    //private async Task ThrowTriggersAsync(DCMeasData message, long featureId, int numer)
    //{
    //  MaterialTriggers matTrigger = new MaterialTriggers(message.BasId, _triggersDictionary);


    //  foreach (Triggers trigger in matTrigger.triggers)
    //  {
    //    bool wasChanged = false;
    //    foreach (TriggersFeature feature in trigger.triggerFeatures)
    //    {
    //      if (feature.triggerFeature.FKFeatureId == featureId && feature.triggerFeature.PassNo == message.PassNumber)
    //      {
    //        feature.status = true;
    //        wasChanged = true;
    //      }
    //    }

    //    //check only if there was 1 or more status change
    //    if (wasChanged == true)
    //    {
    //      //check if realtions are met
    //      if (CheckTriggerRelations(trigger))
    //      {
    //        await ThrowTriggerAsync(trigger.triggerEnum, matTrigger.materialId);
    //      }
    //    }
    //  }

    //  NotificationController.Info($" TASK IS OVER = {numer}");
    //}

    /// <summary>
    /// Check if concrete trigger parameters are filled
    /// </summary>
    /// <param name="trigger"></param>
    /// <returns></returns>
    //private bool CheckTriggerRelations(Triggers trigger)
    //{
    //  foreach (TriggersFeature feature in trigger.triggerFeatures)
    //  {
    //    if (feature.status == false)
    //    {
    //      return false;
    //    }
    //  }

    //  return true;
    //}

    ///// <summary>
    ///// Send information to other modules that a trigger was hit
    ///// </summary>
    ///// <param name="triggerId">L2 id of trigger</param>
    //private async Task ThrowTriggerAsync(TriggerType triggerEnumType, long materialId)
    //{
    //  SendOfficeResult<DataContractBase> result = null;

    //  try
    //  {
    //    result = await _sendOffice.SendTriggerToWBMAsync(new DCTriggerData() { triggerType = triggerEnumType, materialId = materialId });

    //    if (result.OperationSuccess)
    //      NotificationController.Info("SendTrigger to WBF - success");
    //    else
    //      NotificationController.Error("SendTrigger to WBF  - error");
    //  }
    //  catch
    //  {
    //    NotificationController.Error("SendTrigger to WBF  - error");
    //  }

    //  try
    //  {
    //    result = await _sendOffice.SendTriggerToTRKAsync(new DCTriggerData() { triggerType = triggerEnumType, materialId = materialId });

    //    if (result.OperationSuccess)
    //      NotificationController.Info("SendTrigger to TRK - success");
    //    else
    //      NotificationController.Error("SendTrigger to TRK  - error");
    //  }
    //  catch
    //  {
    //    NotificationController.Error("SendTrigger to TRK  - error");
    //  }

    //  try
    //  {
    //    result = await _sendOffice.SendTriggerToDLSAsync(new DCTriggerData() { triggerType = triggerEnumType, materialId = materialId });

    //    if (result.OperationSuccess)
    //      NotificationController.Info("SendTrigger to DLS - success");
    //    else
    //      NotificationController.Error("SendTrigger to DLS  - error");
    //  }
    //  catch
    //  {
    //    NotificationController.Error("SendTrigger to TRK  - error");
    //  }



    //}

    ///////// <summary>
    ///////// When module receive package of meas, list of materials have to be checked,
    ///////// if in package is a material which is not stored in module it will be added to list
    ///////// </summary>
    ///////// <param name="listOfMaterialsInPackage"></param>
    //////private void FillMaterialsTriggersList(List<long> listOfMaterialsInPackage)
    //////{
    //////  foreach (long materialId in listOfMaterialsInPackage)
    //////  {
    //////    bool isAdded = false;
    //////    foreach (MaterialTriggers element in materialTriggers)
    //////    {
    //////      if (element.materialId == materialId)
    //////      {
    //////        isAdded = true;
    //////        break;
    //////      }
    //////    }
    //////    if (!isAdded)
    //////    {
    //////      //if material is not added to list add it then
    //////      materialTriggers.Add();
    //////    }
    //////  }
    //////}


    /// <summary>
    /// Creating of steps for given material for specified in manager step creating features
    /// </summary>
    /// <param name="ctx"></param>
    /// <param name="stepCreators"></param>
    private async Task CreateGeneaologyAsync(PEContext ctx, DCMeasData message, List<MVHFeature> stepCreatingFeatures, List<long> lengthChagneFeatures, List<long> weightChangeFeatures)
    {
      IEnumerable<MVHFeature> CheckMVHFeature = stepCreatingFeatures.Where(s => s.FeatureCode == message.EventCode);
      if (CheckMVHFeature.Any())
      {
        MVHRawMaterial rawMaterial = await _rawMaterialHandler.FindRawMaterialByExternalIdAsync(ctx, message.BasId, true);
        MVHRawMaterialsStep new_step = _rawMaterialHandler.CreateRawMaterialStep(ctx, rawMaterial, message, lengthChagneFeatures, weightChangeFeatures);
        rawMaterial.MVHRawMaterialsSteps.Add(new_step);
        _rawMaterialHandler.UpdateOverallStep(rawMaterial);
      }
    }

    /// <summary>
    /// INTERNAL TRIGGER DATA STRUCTURE FOR MODULE
    /// </summary>

    //#region data structure
    //public class MaterialTriggers
    //{
    //  public long materialId;
    //  public List<Triggers> triggers;

    //  public MaterialTriggers(long materialId, Dictionary<MVHTrigger, List<MVHTriggersFeature>> triggersDictionary)
    //  {
    //    this.materialId = materialId;
    //    triggers = new List<Triggers>();
    //    foreach (KeyValuePair<MVHTrigger, List<MVHTriggersFeature>> element in triggersDictionary)
    //    {
    //      triggers.Add(new Triggers(element));
    //    }
    //  }
    //}
    //public class Triggers
    //{
    //  public long triggerId { get; set; }
    //  public TriggerType triggerEnum { get; set; }
    //  public List<TriggersFeature> triggerFeatures { get; set; }

    //  public Triggers(KeyValuePair<MVHTrigger, List<MVHTriggersFeature>> element)
    //  {
    //    this.triggerId = element.Key.TriggerId;
    //    this.triggerEnum = (TriggerType)element.Key.EnumTriggerType;
    //    triggerFeatures = new List<TriggersFeature>();
    //    foreach (MVHTriggersFeature triggerFeature in element.Value)
    //    {
    //      triggerFeatures.Add(new TriggersFeature(triggerFeature));
    //    }
    //  }
    //}

    //public class TriggersFeature
    //{

    //  public MVHTriggersFeature triggerFeature { get; set; }
    //  public bool status { get; set; }

    //  public TriggersFeature(MVHTriggersFeature triggerFeature)
    //  {
    //    this.triggerFeature = triggerFeature;
    //    status = false;
    //  }
    //}

    //#endregion
  }

}
