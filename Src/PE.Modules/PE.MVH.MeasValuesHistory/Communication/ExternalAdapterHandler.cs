using Ninject;
using Ninject.Parameters;
using PE.DTO.Internal.Adapter;
using PE.DTO.Internal.MVHistory;
using PE.DTO.Internal.ProdManager;
using PE.DTO.Internal.Schedule;
using PE.Interfaces.Managers;
using PE.MVH.Managers;
using PE.MVH.MeasValuesHistory.Module;
using SMF.Core.DC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PE.MVH.MeasValuesHistory.Communication
{
  internal static class ExternalAdapterHandler
  {
    private static readonly IDefectCatalogueManager _defectCatalogueManager;
    private static readonly IRawMaterialManager _rawMaterialManager;
    private static readonly IMeasurementManager _measurementManager;

    static ExternalAdapterHandler()
    {
      using (IKernel kernel = new StandardKernel())
      {
        //kernel.Bind<IDefectCatalogueManager>().To<DefectCatalogueManager>();
        kernel.Bind<IRawMaterialManager>().To<RawMaterialManager>();
        kernel.Bind<IMeasurementManager>().To<MeasurementManager>();

        //_defectCatalogueManager = kernel.Get<IDefectCatalogueManager>(new ConstructorArgument("sendOffice", new SendOffice()));
        _rawMaterialManager = kernel.Get<IRawMaterialManager>(new ConstructorArgument("sendOffice", new SendOffice()));
        _measurementManager = kernel.Get<IMeasurementManager>(new ConstructorArgument("sendOffice", new SendOffice()));
      }
    }

    #region L1

    internal static async Task<DCPEBasId> ProcessL1BaseIdRequestAsync(DCL1BasIdRequest message)
    {
      return await _rawMaterialManager.ProcessL1BaseIdRequestAsync(message);
    }
    internal static async Task<DataContractBase> ProcessScrapMessageAsync(DCL1ScrapData message)
    {
      return await _rawMaterialManager.ProcessScrapMessageAsync(message);
    }

    internal static async Task<DataContractBase> ProcessCutDataMessageAsync(DCL1CutData message)
    {
      return await _rawMaterialManager.ProcessCutDataMessageAsync(message);
    }

    internal static async Task<DCPEBasId> ProcessDivisionMaterialMessageAsync(DCL1MaterialDivision message)
    {
      return await _rawMaterialManager.ProcessDivisionMaterialMessageAsync(message);
    }

    internal static async Task<DataContractBase> ProcessL1MeasurementAsync(DCMeasData message)
    {
      if (message.BasId != 0)// if measurement is not consumption
      {
        //assync call
        Task task = Task.Run(() => _rawMaterialManager.MaterialStateOperations(message));
      }
      return await _measurementManager.ProcessMeasurementAsync(message);
      
    }
    internal static async Task<DataContractBase> ProcessL1SampleMeasurementAsync(DCMeasDataSample message)
    {
      //assync call
      Task task = Task.Run(() => _rawMaterialManager.MaterialStateOperations(message));
      return await _measurementManager.ProcessMeasurementAsync(message);
    }

    #endregion


    #region HMI

    internal static async Task<DataContractBase> AssignMaterialsAsync(DCMaterialAssign message)
    {
      return await _rawMaterialManager.AssignMaterialsAsync(message);
    }

    internal static async Task<DataContractBase> UnassignMaterialAsync(DCMaterialAssign message)
    {
      return await _rawMaterialManager.UnassignMaterialAsync(message);
    }

    #endregion











    //internal static async Task<DataContractBase> ProcessFinishedMessageAsync(DCL1MaterialFinished message)
    //{
    //  return await _rawMaterialManager.ProcessFinishedMessageAsync(message);
    //}

    //internal static async Task<DataContractBase> ChangeMaterialStatusAsync(DCNewMaterialStatus message)
    //{
    //  return await _rawMaterialManager.ChangeMaterialStatusAsync(message);
    //}



    internal static async Task<DataContractBase> MarkMaterialAsScrapAsync(DCMaterialMarkedAsScrap message)
    {
      return await _rawMaterialManager.MarkMaterialAsScrapAsync(message);
    }

    //internal static async Task<DataContractBase> ProcessProductionEndAsync(DCMaterialProductionEnd dataToSend)
    //{
    //  return await _rawMaterialManager.MaterialProductionEndAsync(dataToSend);
    //}

    //internal static async Task<DataContractBase> ConnectRawMaterialWithProductAsync(DCProductData dataToSend)
    //{
    //  return await _rawMaterialManager.ConnectRawMaterialWithProductAsync(dataToSend);
    //}
  }
}
