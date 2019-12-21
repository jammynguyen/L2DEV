using PE.DbEntity.Enums;
using PE.DbEntity.Models;
using PE.DTO.Internal.MVHistory;
using PE.DTO.Internal.Schedule;
using SMF.Core.DC;
using SMF.Module.Core;
using SMF.Module.Notification;
using System;
using System.Data.Entity.Infrastructure;
using System.Linq;
using PE.DTO.Internal.ProdManager;
using PE.Common;
using System.Threading.Tasks;
using SMF.RepositoryPatternExt;
using PE.Interfaces.Managers;
using PE.MVH.Handlers;
using PE.Interfaces.Interfaces.SendOffice;
using System.Data.Entity;
using PE.DTO.Internal.Delay;

namespace PE.MVH.Managers
{
  public class RawMaterialManager : IRawMaterialManager
  {
    #region members

    private readonly IMeasValuesHistoryRawMaterialSendOffice _sendOffice;
    private MVHFeature _featureForL3MaterialAssignment;// feature for L3 material assignment
    private MVHFeature _featureForWRProductCreation;
    private MVHFeature _featureDelayHeadEnters;
    private MVHFeature _featureDelayTailLeaves;

    #endregion
    #region handlers

    private readonly RawMaterialHandler _rawMaterialHandler;
    private readonly MeasurementsHandler _measurementsHandler;
    #endregion
    #region ctor
    public RawMaterialManager(IMeasValuesHistoryRawMaterialSendOffice sendOffice)
    {
      _sendOffice = sendOffice;
      _rawMaterialHandler = new RawMaterialHandler();
      _measurementsHandler = new MeasurementsHandler();

      using (PEContext ctx = new PEContext())
      {
        try
        {
          _featureForL3MaterialAssignment = _measurementsHandler.GetFeatureOfType(ctx, FeatureType.L3MaterialAssignment);
          _featureForWRProductCreation = _measurementsHandler.GetFeatureOfType(ctx, FeatureType.WireRodProductCreation);
          _featureDelayHeadEnters = _measurementsHandler.GetFeatureOfType(ctx, FeatureType.ProductionGapStart);
          _featureDelayTailLeaves = _measurementsHandler.GetFeatureOfType(ctx, FeatureType.ProductionGapEnd);
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

    public virtual async Task<DCPEBasId> ProcessL1BaseIdRequestAsync(DCL1BasIdRequest message)
    {
      DCPEBasId baseIdMessage = new DCPEBasId();

      try
      {
        using (PEContext ctx = new PEContext())
        {
          MVHRawMaterial rawMat = await _rawMaterialHandler.CreateRawMaterial(ctx, message.IsSimnu, (message.IsSimnu ? "SIM" : "RawMaterial") + DateTime.Now.ToString("yyyyMMddHHmmssfff"));
          _rawMaterialHandler.AddRawMaterial(ctx, rawMat);
          _rawMaterialHandler.UpdateOverallStep(rawMat);
          await ctx.SaveChangesAsync();


          baseIdMessage.RequestToken = message.RequestToken;
          baseIdMessage.BasId = (uint)rawMat.ExternalRawMaterialId;
        }
      }
      catch (DbUpdateException e)
      {
        NotificationController.RegisterAlarm(Defs.ErrorDuringSave, $"Saving of created RawMaterial failed");
        NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
        throw new Exception("Internal exception", e);
      }
      catch (Exception e)
      {
        NotificationController.RegisterAlarm(Defs.CreateRawMaterial, $"Creating raw material failed");
        NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
        throw new Exception("Internal exception", e);
      }

      return baseIdMessage;
    }

    //public virtual async Task<DataContractBase> ProcessFinishedMessageAsync(DCL1MaterialFinished message)
    //{
    //  DataContractBase result = new DataContractBase();

    //  using (PEContext ctx = new PEContext())
    //  {
    //    try
    //    {
    //      MVHRawMaterial rawMaterial = await _rawMaterialHandler.FindRawMaterialByExternalIdAsync(ctx, message.BasId);

    //      rawMaterial.Status = RawMaterialStatus.ENUM_Rolled;


    //      await ctx.SaveChangesAsync();

    //    }
    //    catch (InvalidOperationException)
    //    {
    //      NotificationController.RegisterAlarm(Defs.RawMaterialNotExist, $"RawMaterial with external id:{message.BasId} not exist");
    //      NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
    //      throw new Exception();
    //    }
    //    catch (DbUpdateException)
    //    {
    //      NotificationController.RegisterAlarm(Defs.ErrorDuringSave, $"Saving of status RawMaterial with id:{message.BasId} failed");
    //      NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
    //      throw new Exception();
    //    }
    //    catch (Exception)
    //    {
    //      NotificationController.RegisterAlarm(Defs.MarkAsFinished, $"Set as finished { RawMaterialStatus.ENUM_Rolled.ToString()} raw material with external id :{message.BasId} failed");
    //      NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
    //      throw new Exception();
    //    }
    //  }

    //  return result;
    //}

    public virtual async Task<DataContractBase> ProcessScrapMessageAsync(DCL1ScrapData message)
    {
      DataContractBase result = new DataContractBase();

      using (PEContext ctx = new PEContext())
      {
        try
        {
          MVHRawMaterial rawMaterial = await _rawMaterialHandler.FindRawMaterialByExternalIdAsync(ctx, message.BasId,true);

          switch (message.TypeOfScrap)
          {
            case TypeOfScrap.CanBeRolled:
              {
                rawMaterial.Status = RawMaterialStatus.ENUM_ScrapCanBeRolledAgain;
                break;
              }
            case TypeOfScrap.ScrapFromMill:
              {
                rawMaterial.Status = RawMaterialStatus.ENUM_ScrapFromMill;
                break;
              }
            case TypeOfScrap.CanOrCannotLogicInL2:
              {
                //Check if material is after or before first mill stand
                //Take all material steps
                bool isDetected = false;
                foreach (MVHRawMaterialsStep element in rawMaterial.MVHRawMaterialsSteps)
                {
                  MVHAsset asset = _rawMaterialHandler.GetFMVHAssetByStep(ctx, element);
                  if (asset != null)
                  {
                    if (asset.OrderSeq >= 2310)
                    {
                      rawMaterial.Status = RawMaterialStatus.ENUM_ScrapFromMill;
                      isDetected = true;
                      break;
                    }
                  }
                }

                if (isDetected == false)
                  rawMaterial.Status = RawMaterialStatus.ENUM_ScrapCanBeRolledAgain;
                break;
              }
            default:
              {
                throw new Exception();
              }
          }

          await ctx.SaveChangesAsync();
          await _sendOffice.SendScrapEventAsync(message);


          //SendOfficeResult<DataContractBase> tmpResult = null;

          //tmpResult = await _sendOffice.SendTriggerToWBMAsync(new DCTriggerData() { triggerType = TriggerType.Scrap, materialId = rawMaterial.RawMaterialId });

          //if (tmpResult.OperationSuccess)
          //  NotificationController.Info("SendTrigger to WBF - success");
          //else
          //  NotificationController.Error("SendTrigger to WBF  - error");

          //tmpResult = await _sendOffice.SendTriggerToTRKAsync(new DCTriggerData() { triggerType = TriggerType.Scrap, materialId = rawMaterial.RawMaterialId });

          //if (tmpResult.OperationSuccess)
          //  NotificationController.Info("SendTrigger to TRK - success");
          //else
          //  NotificationController.Error("SendTrigger to TRK  - error");


        }
        catch (InvalidOperationException)
        {
          NotificationController.RegisterAlarm(Defs.RawMaterialNotExist, $"RawMaterial with external id:{message.BasId} not exist");
          NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
          throw new Exception();
        }
        catch (DbUpdateException)
        {
          NotificationController.RegisterAlarm(Defs.ErrorDuringSave, $"Saving of scrapping RawMaterial with id:{message.BasId} failed");
          NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
          throw new Exception();
        }
        catch (Exception)
        {
          NotificationController.RegisterAlarm(Defs.MarkAsScrap, $"Set as scrap {message.TypeOfScrap.ToString()} raw material with external id :{message.BasId} failed");
          NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
          throw new Exception();
        }
      }

      return result;
    }

    public virtual async Task<DataContractBase> ProcessCutDataMessageAsync(DCL1CutData message)
    {
      DataContractBase result = new DataContractBase();

      using (PEContext ctx = new PEContext())
      {
        try
        {
          MVHRawMaterial rawMaterial = await _rawMaterialHandler.FindRawMaterialByExternalIdAsync(ctx, message.BasId, true);

          switch (message.TypeOfCut)
          {
            case TypeOfCut.HeadCut:
              {
                rawMaterial.MVHRawMaterialsSteps.Add(_rawMaterialHandler.CreateHeadCutStep(rawMaterial, message));
                break;
              }
            case TypeOfCut.TailCut:
              {
                rawMaterial.MVHRawMaterialsSteps.Add(_rawMaterialHandler.CreateTailCutStep(rawMaterial, message));
                break;
              }
            case TypeOfCut.SampleCut:
              {
                rawMaterial.MVHRawMaterialsSteps.Add(_rawMaterialHandler.CreateSampleCutStep(rawMaterial, message));
                break;
              }
          }
          _rawMaterialHandler.UpdateOverallStep(rawMaterial);
          await ctx.SaveChangesAsync();

        }
        catch (InvalidOperationException ex)
        {
          NotificationController.RegisterAlarm(Defs.RawMaterialNotExist, $"RawMaterial with external id:{message.BasId} not exist");
          NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
          throw ex;
        }
        catch (DbUpdateException ex)
        {
          NotificationController.RegisterAlarm(Defs.ErrorDuringSave, $"Saving of scrapping RawMaterial with id:{message.BasId} failed");
          NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
          throw ex;
        }
        catch (Exception ex)
        {
          NotificationController.RegisterAlarm(Defs.CutMaterial, $"Cutting raw material with external id :{message.BasId} failed");
          NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
          throw ex;
        }
      }

      return result;
    }


    #region material operations

    public virtual async Task<DataContractBase> MaterialStateOperations(DCMeasData message)
    {

      try
      {
        // L3 - Raw material assignment
        if (_featureForL3MaterialAssignment != null && message.EventCode == _featureForL3MaterialAssignment.FeatureCode)
          await L3MaterialAssignment(message);

        //  Automatic product creation for WR
        if (_featureForWRProductCreation != null && message.EventCode == _featureForWRProductCreation.FeatureCode)
        {
          await ProductCreationAsync(message);
          await RemoveFinishedOrdersFromScheduleAsync(); 
        }
          

        //  delay tail leave
        if (_featureDelayTailLeaves != null && message.EventCode == _featureDelayTailLeaves.FeatureCode)
          await CallDelayTailLeaveAsync(message);

        //  delay head enters
        if (_featureDelayHeadEnters != null && message.EventCode == _featureDelayHeadEnters.FeatureCode)
          await CallDelayHeadEntersAsync(message);


        await UpdateRawMaterialStatusAsync(message);
      }
      catch (Exception ex)
      {
        NotificationController.Error($"Exception: {ex.Message}");
      }

     

      return new DataContractBase();
    }



    private async Task L3MaterialAssignment(DCMeasData message)
    {
      SendOfficeResult<DCL1L3MaterialConnector> result = null;

      using (PEContext ctx = new PEContext())
      {
        try
        {
          MVHRawMaterial rawMaterial = await _rawMaterialHandler.FindRawMaterialByExternalIdAsync(ctx, message.BasId);
          result = await _sendOffice.SendRequestForL3MaterialAsync(new DCFeatureRelatedOperationData() { FeatureType = _featureForL3MaterialAssignment.EnumFeatureType, RawMaterialId = rawMaterial.RawMaterialId });

          if (result.OperationSuccess)
          {
            NotificationController.Info($"Assigning L3 material ({result.DataConctract.PRMMaterialId}) for Raw Material ({result.DataConctract.RawMaterialId}) BasId: {message.BasId}");
            //assign material
            rawMaterial.FKMaterialId = result.DataConctract.PRMMaterialId;
            await ctx.SaveChangesAsync();
          }

          else
            NotificationController.Error($"Error during assigning L3 material for BasId: {message.BasId}");
        }
        catch
        {
          NotificationController.Error($"Error during assigning L3 material for BasId: {message.BasId}");
        }
      }
    }
    private async Task ProductCreationAsync(DCMeasData message)
    {
      using (PEContext ctx = new PEContext())
      {
        SendOfficeResult<DCProductData> result = new SendOfficeResult<DCProductData>();

        try
        {
          MVHRawMaterial rawMaterial = await _rawMaterialHandler.FindRawMaterialByExternalIdAsync(ctx, message.BasId,true);
          DCMaterialProductionEnd messageToSend = new DCMaterialProductionEnd() { RawMaterialId = rawMaterial.RawMaterialId };

          NotificationController.Info($"Requesting product creation for RawMaterial {messageToSend.RawMaterialId}");
          result = await _sendOffice.SendRequestToCreateProductAsync(new DCRawMaterialData()
          {
            RawMaterialId = rawMaterial.RawMaterialId,
            FKMaterialId = rawMaterial.FKMaterialId,
            OverallWeight = rawMaterial.MVHRawMaterialsSteps
               .Where(w => w.ProcessingStepNo == RawMaterialStepNo.OverallStep)
               .Single()
               .Weight.GetValueOrDefault()
          });

          if (result.OperationSuccess)
          {
            try
            {
              //assign product to raw material 
              rawMaterial.FKProductId = result.DataConctract.ProductId;
              await ctx.SaveChangesAsync();
            }
            catch (InvalidOperationException ex)
            {
              NotificationController.RegisterAlarm(Defs.RawMaterialNotExist, $"RawMaterial with id:{messageToSend.RawMaterialId} not exist");
              NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
              throw new Exception(ex.Message, ex);
            }
            catch (DbUpdateException ex)
            {
              NotificationController.RegisterAlarm(Defs.ErrorDuringSave, $"update RawMaterial with id:{messageToSend.RawMaterialId} failed");
              NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
              throw new Exception(ex.Message, ex);
            }
            catch (Exception ex)
            {
              NotificationController.RegisterAlarm(Defs.ConnectRawMaterialWithPRMMaterial, $"ConnectRawMaterialWithL3Material thrown unexpected error");
              NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
              throw new Exception(ex.Message, ex);
            }
          }

          else
            NotificationController.Error("Send Raw Material Data to PRM - error");
        }
        catch (Exception ex)
        {
          NotificationController.RegisterAlarm(Defs.RawMaterialNotExist, $"RawMaterial with basid:{message.BasId} not exist");
          NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
          throw new Exception(ex.Message, ex);
        }
      }
    }
    private async Task UpdateRawMaterialStatusAsync(DCMeasData message)
    {

      using (PEContext ctx = new PEContext())
      {
        MVHRawMaterial rawMaterial = await _rawMaterialHandler.FindRawMaterialByExternalIdAsync(ctx, message.BasId, true);
        MVHFeature currentFeature = await ctx.MVHFeatures.Where(w => w.FeatureCode == message.EventCode).SingleAsync();
        NotificationController.Info($"Setting status for raw material {rawMaterial.RawMaterialId}, last status: {rawMaterial.Status}");

        RawMaterialStatus newStatus = rawMaterial.Status;

        switch (currentFeature.EnumFeatureType)
        {
          case FeatureType.Charge:
            newStatus = RawMaterialStatus.ENUM_Charged;
            break;
          case FeatureType.Discharge:
            newStatus = RawMaterialStatus.ENUM_Discharged;
            break;
          case FeatureType.InFinalProduction:
            newStatus = RawMaterialStatus.ENUM_InFinalProduction;
            break;
          case FeatureType.InMill:
            newStatus = RawMaterialStatus.ENUM_InMill;
            break;
          case FeatureType.Rolled:
            newStatus = RawMaterialStatus.ENUM_Rolled;
            break;
          case FeatureType.UnCharge:
            newStatus = RawMaterialStatus.ENUM_Unassigned;
            break;
          case FeatureType.UnDischarge:
            newStatus = RawMaterialStatus.ENUM_Charged;
            break;
          default:
            break;
        }

        if (newStatus != rawMaterial.Status)
        {
          rawMaterial.Status = newStatus;
          NotificationController.Info($"Setting status for raw material {rawMaterial.RawMaterialId}, new status: {rawMaterial.Status}");
          await ctx.SaveChangesAsync();
        }
        await ModuleController.HmiRefresh(HMIRefreshKeys.Schedule);
      }
    }
    private async Task CallDelayTailLeaveAsync(DCMeasData message)
    {
      SendOfficeResult<DataContractBase> result = null;

      result = await _sendOffice.SendTailLeavesToDLSAsync(new DCDelayEvent());

      if (result.OperationSuccess)
      {
        NotificationController.Info($"Successful triggering delay module for BasId: {message.BasId}");
      }

      else
        NotificationController.Error($"Error during triggering delay module for BasId: {message.BasId}");
    }
    private async Task CallDelayHeadEntersAsync(DCMeasData message)
    {
      SendOfficeResult<DataContractBase> result = null;

      result = await _sendOffice.SendHeadEnterToDLSAsync(new DCDelayEvent());

      if (result.OperationSuccess)
      {
        NotificationController.Info($"Successful triggering delay module for BasId: {message.BasId}");
      }

      else
        NotificationController.Error($"Error during triggering delay module for BasId: {message.BasId}");
    }
    private async Task RemoveFinishedOrdersFromScheduleAsync()
    {
      SendOfficeResult<DataContractBase> result = null;

      result = await _sendOffice.SendRemoveFinishedOrdersFromScheduleAsync(new DataContractBase());

      if (result.OperationSuccess)
      {
        NotificationController.Info($"Successful triggering schedule cleanup");
      }

      else
        NotificationController.Error($"Error during triggering triggering schedule cleanup");
    }

    #endregion








    /// <summary>
    /// After send request to PPL to get properties from next material in schedule create new rawMaterialsStep ( initial ) and fill it with given in message properties
    /// </summary>
    /// <param name="message"></param>
    /// <returns></returns>
    public virtual async Task<DataContractBase> AssignL3MaterialPropertiesForRawMaterialAsync(DCL3MaterialForRawMaterial message)
    {
      DataContractBase result = new DataContractBase();

      try
      {
        using (PEContext ctx = new PEContext())
        {
          MVHRawMaterial rawMaterial = await _rawMaterialHandler.FindRawMaterialByExternalIdAsync(ctx, message.ExternalRawMaterialId, true);
          {
            rawMaterial.FKMaterialId = message.NextInScheduleL3Material;
            MVHRawMaterialsStep step = rawMaterial.MVHRawMaterialsSteps.Where(w => w.ProcessingStepNo == RawMaterialStepNo.FirstStep).Single();
            {
              step.ProcessingStepNo = RawMaterialStepNo.FirstStep;
              step.Weight = message.Weigth;
              step.Width = message.Width;
              step.Thickness = message.Thickness;
              step.Length = message.Length;
            }
            rawMaterial.MVHRawMaterialsSteps.Add(step);
          }

          _rawMaterialHandler.UpdateOverallStep(rawMaterial);
          await ctx.SaveChangesAsync();

        }
      }
      catch (InvalidOperationException)
      {
        NotificationController.RegisterAlarm(Defs.RawMaterialNotExist, $"RawMaterial with external id:{message.ExternalRawMaterialId} not exist");
        NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
        throw new Exception();
      }
      catch (DbUpdateException)
      {
        NotificationController.RegisterAlarm(Defs.ErrorDuringSave, $"Saving of created RawMaterial failed");
        NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
        throw new Exception();
      }
      catch (Exception)
      {
        NotificationController.RegisterAlarm(Defs.CreateRawMaterial, $"AssignL3MaterialPropertiesForRawMaterial failed");
        NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
        throw new Exception();
      }

      return result;
    }

    public virtual async Task<DCPEBasId> ProcessDivisionMaterialMessageAsync(DCL1MaterialDivision message)
    {
      DCPEBasId baseIdMessage = new DCPEBasId();

      try
      {
        using (PEContext ctx = new PEContext())
        {

          double? tempWeight = 0;


          //material to divide
          MVHRawMaterial rawMaterial = await _rawMaterialHandler.FindRawMaterialByExternalIdAsync(ctx, message.ParentBasId, true);
          //change mother properties if needed
          rawMaterial.MVHRawMaterialsSteps.Add(_rawMaterialHandler.MakeDivisionStep(ctx, rawMaterial, message, out tempWeight));
          rawMaterial.ChildsNo++;
          rawMaterial.IsVirtual = true;

          _rawMaterialHandler.UpdateOverallStep(rawMaterial);

          //new material created with overall step (default)
          MVHRawMaterial newMaterial = await _rawMaterialHandler.CreateRawMaterial(ctx, rawMaterial.IsDummy, rawMaterial.RawMaterialName + "_" + rawMaterial.ChildsNo);
          //set properties from parent
          {
            newMaterial.ParentRawMaterialId = rawMaterial.RawMaterialId;
            newMaterial.FKMaterialId = rawMaterial.FKMaterialId;
            newMaterial.CuttingSeqNo = rawMaterial.ChildsNo;
            newMaterial.Status = rawMaterial.Status;
            //newMaterial.MVHRawMaterialsSteps.Add(_rawMaterialHandler.CloneRawMaterialStep(rawMaterial.MVHRawMaterialsSteps.Where(w => w.ProcessingStepNo == RawMaterialStepNo.FirstStep).Single()));
            //newMaterial.MVHRawMaterialsSteps.Add(_rawMaterialHandler.CloneRawMaterialStep(rawMaterial.MVHRawMaterialsSteps.Where(w => w.ProcessingStepNo == RawMaterialStepNo.OverallStep).Single()));

          }
          _rawMaterialHandler.UpadeRawMaterialStep(newMaterial.MVHRawMaterialsSteps.Where(w => w.ProcessingStepNo == RawMaterialStepNo.FirstStep).Single(), rawMaterial.MVHRawMaterialsSteps.Where(w => w.ProcessingStepNo == RawMaterialStepNo.FirstStep).Single());
          _rawMaterialHandler.UpadeRawMaterialStep(newMaterial.MVHRawMaterialsSteps.Where(w => w.ProcessingStepNo == RawMaterialStepNo.OverallStep).Single(), rawMaterial.MVHRawMaterialsSteps.Where(w => w.ProcessingStepNo == RawMaterialStepNo.OverallStep).Single());

          newMaterial.MVHRawMaterialsSteps.Last().Weight = tempWeight;
          newMaterial.MVHRawMaterialsSteps.Last().Length = message.NewMaterialLength;
          newMaterial.MVHRawMaterialsSteps.Last().Elongation = 1;

          _rawMaterialHandler.AddRawMaterial(ctx, newMaterial);

          _rawMaterialHandler.UpdateOverallStep(newMaterial);

          await ctx.SaveChangesAsync();

          baseIdMessage.RequestToken = message.RequestToken;
          baseIdMessage.BasId = (uint)(newMaterial.ExternalRawMaterialId);
          baseIdMessage.IsDivide = true;
        }
      }
      catch (InvalidOperationException)
      {
        NotificationController.RegisterAlarm(Defs.RawMaterialNotExist, $"RawMaterial with external id:{0} not found", message.ParentBasId);
        NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
        throw new Exception();
      }
      catch (Exception)
      {
        NotificationController.RegisterAlarm(Defs.DivideMaterial, $"ProcessDivisionMaterialMessage failed {0}", message.ParentBasId);
        NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
        throw new Exception();
      }


      return baseIdMessage;
    }

  




    //public virtual async Task<DataContractBase> ChangeMaterialStatusAsync(DCNewMaterialStatus message)
    //{
    //  DataContractBase result = new DataContractBase();

    //  using (PEContext ctx = new PEContext())
    //  {
    //    try
    //    {
    //      MVHRawMaterial rawMaterial = await _rawMaterialHandler.FindRawMaterialByIdAsync(ctx, message.MaterialId);

    //      rawMaterial.Status = message.Status;

    //      await ctx.SaveChangesAsync();

    //    }
    //    catch (InvalidOperationException)
    //    {
    //      NotificationController.RegisterAlarm(Defs.RawMaterialNotExist, $"RawMaterial with external id:{message.MaterialId} not exist");
    //      NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
    //      throw new Exception();
    //    }
    //    catch (DbUpdateException)
    //    {
    //      NotificationController.RegisterAlarm(Defs.ErrorDuringSave, $"Saving of status {message.Status} RawMaterial with id:{message.MaterialId} failed");
    //      NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
    //      throw new Exception();
    //    }
    //    catch (Exception)
    //    {
    //      NotificationController.RegisterAlarm(Defs.MarkAsFinished, $"Set as{ message.Status} raw material with external id :{message.MaterialId} failed");
    //      NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
    //      throw new Exception();
    //    }
    //  }

    //  return result;
    //}

    public virtual async Task<DataContractBase> AssignMaterialsAsync(DCMaterialAssign message)
    {
      DataContractBase result = new DataContractBase();

      try
      {
        using (PEContext ctx = new PEContext())
        {
          MVHRawMaterial rawMaterial = await _rawMaterialHandler.FindRawMaterialByIdAsync(ctx, message.RawMaterialId);

          rawMaterial.FKMaterialId = message.L3MaterialId;

          await ctx.SaveChangesAsync();

        }
      }
      catch (InvalidOperationException)
      {
        NotificationController.RegisterAlarm(Defs.RawMaterialNotExist, $"RawMaterial with external id:{message.RawMaterialId} not exist");
        NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
        throw new Exception();
      }
      catch (DbUpdateException)
      {
        NotificationController.RegisterAlarm(Defs.ErrorDuringSave, $"Saving L3 material {message.L3MaterialId} on RawMaterial with id:{message.RawMaterialId} failed");
        NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
        throw new Exception();
      }
      catch (Exception)
      {
        NotificationController.RegisterAlarm(Defs.L3MaterialAssign, $"Saving L3 material {message.L3MaterialId} on RawMaterial with id:{message.RawMaterialId} failed");
        NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
        throw new Exception();
      }


      return result;
    }

    public virtual async Task<DataContractBase> UnassignMaterialAsync(DCMaterialAssign message)
    {
      DataContractBase result = new DataContractBase();

      try
      {
        using (PEContext ctx = new PEContext())
        {
          MVHRawMaterial rawMaterial = await _rawMaterialHandler.FindRawMaterialByIdAsync(ctx, message.RawMaterialId);

          rawMaterial.FKMaterialId = null;

          await ctx.SaveChangesAsync();

        }
      }
      catch (InvalidOperationException)
      {
        NotificationController.RegisterAlarm(Defs.RawMaterialNotExist, $"RawMaterial with external id:{message.RawMaterialId} not exist");
        NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
        throw new Exception();
      }
      catch (DbUpdateException)
      {
        NotificationController.RegisterAlarm(Defs.ErrorDuringSave, $"Saving RawMaterial with id:{message.RawMaterialId} failed");
        NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
        throw new Exception();
      }
      catch (Exception)
      {
        NotificationController.RegisterAlarm(Defs.L3MaterialAssign, $"Saving L3 material {message.L3MaterialId} on RawMaterial with id:{message.RawMaterialId} failed");
        NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
        throw new Exception();
      }

      return result;
    }

    public virtual async Task<DataContractBase> MarkMaterialAsScrapAsync(DCMaterialMarkedAsScrap message)
    {
      DataContractBase result = new DataContractBase();
      bool isDetected = false;
      try
      {
        using (PEContext ctx = new PEContext())
        {
          MVHRawMaterial rawMaterial = await _rawMaterialHandler.FindRawMaterialByIdAsync(ctx, message.RawMaterialId, true);

          //Take all material steps
          foreach(MVHRawMaterialsStep element in rawMaterial.MVHRawMaterialsSteps)
          {
            MVHAsset asset = _rawMaterialHandler.GetFMVHAssetByStep(ctx, element);
            if (asset != null)
            {
              if (asset.OrderSeq >= 2310)
              {
                rawMaterial.Status = RawMaterialStatus.ENUM_ScrapFromMill;
                isDetected = true;
                break;
              }
            }
          }

          if(isDetected == false)
            rawMaterial.Status = RawMaterialStatus.ENUM_ScrapCanBeRolledAgain;
          await ctx.SaveChangesAsync();

          await ModuleController.HmiRefresh(HMIRefreshKeys.RawMaterialDetails);

        }
      }
      catch (DbUpdateException)
      {
        NotificationController.RegisterAlarm(Defs.ErrorDuringSave, $"Marking as scrap RawMaterial with id:{message.RawMaterialId} failed");
        NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
        throw new ModuleMessageException(ModuleController.ModuleName, Defs.MarkAsScrap, $"Marking as scrap: {RawMaterialStatus.ENUM_ScrapFromMill} on RawMaterial with id:{message.RawMaterialId} failed", RawMaterialStatus.ENUM_ScrapFromMill, message.RawMaterialId);
      }
      catch (Exception)
      {
        NotificationController.RegisterAlarm(Defs.MarkAsScrap, $"Marking as scrap: {RawMaterialStatus.ENUM_ScrapFromMill} on RawMaterial with id:{message.RawMaterialId} failed", RawMaterialStatus.ENUM_ScrapFromMill, message.RawMaterialId);
        NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
        throw new ModuleMessageException(ModuleController.ModuleName, Defs.MarkAsScrap, $"Marking as scrap: {RawMaterialStatus.ENUM_ScrapFromMill} on RawMaterial with id:{message.RawMaterialId} failed", RawMaterialStatus.ENUM_ScrapFromMill, message.RawMaterialId);
      }


      return result;
    }




  }
}
