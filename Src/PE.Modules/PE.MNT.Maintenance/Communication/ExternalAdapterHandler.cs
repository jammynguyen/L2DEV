using Ninject;
using Ninject.Parameters;
using PE.DTO.Internal.Maintenance;
using PE.Interfaces.Lite;
using SMF.Core.DC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PE.Interfaces.Managers;
using PE.MNT.Managers;

namespace PE.MNT.Maintenance.Communication
{
  internal static class ExternalAdapterHandler
  {
    private static readonly IEquipmentGroupManager _equipmentGroupManager;
    private static readonly IEquipmentManager _equipmentManager;
    private static readonly IEquipmentAccuManager _equipmentAccuManager;

    static ExternalAdapterHandler()
    {
      using (IKernel kernel = new StandardKernel())
      {
        kernel.Bind<IEquipmentGroupManager>().To<EquipmentGroupManager>();
        _equipmentGroupManager = kernel.Get<IEquipmentGroupManager>(new ConstructorArgument("sendOffice", new SendOffice()));

        kernel.Bind<IEquipmentManager>().To<EquipmentManager>();
        _equipmentManager = kernel.Get<IEquipmentManager>(new ConstructorArgument("sendOffice", new SendOffice()));

        kernel.Bind<IEquipmentAccuManager>().To<EquipmentAccuManager>();
        _equipmentAccuManager = kernel.Get<IEquipmentAccuManager>(new ConstructorArgument("sendOffice", new SendOffice()));
      }
    }

    internal static async Task<DataContractBase> CreateEquipmentGroupAsync(DCEquipmentGroup dc)
    {
      return await _equipmentGroupManager.CreateEquipmentGroupAsync(dc);
    }

    internal static async Task<DataContractBase> DeleteEquipmentGroupAsync(DCEquipmentGroup dc)
    {
      return await _equipmentGroupManager.DeleteEquipmentGroupAsync(dc);
    }

    internal static async Task<DataContractBase> UpdateEquipmentGroupAsync(DCEquipmentGroup dc)
    {
      return await _equipmentGroupManager.UpdateEquipmentGroupAsync(dc);
    }

    internal static async Task<DataContractBase> CloneEquipmentAsync(DCEquipment dc)
    {
      return await _equipmentManager.CloneEquipmentAsync(dc);
    }

    internal static async Task<DataContractBase> CreateEquipmentAsync(DCEquipment dc)
    {
      return await _equipmentManager.CreateEquipmentAsync(dc);
    }

    internal static async Task<DataContractBase> UpdateEquipmentAsync(DCEquipment dc)
    {
      return await _equipmentManager.UpdateEquipmentAsync(dc);
    }
    internal static async Task<DataContractBase> DeleteEquipmentAsync(DCEquipment dc)
    {
      return await _equipmentManager.DeleteEquipmentAsync(dc);
    }

    internal static async Task<DataContractBase> AccumulateEquipmentUsageAsync(DCEquipmentAccu dc)
    {
      return await _equipmentAccuManager.AccumulateEquipmentUsageAsync(dc);
    }
  }
}
