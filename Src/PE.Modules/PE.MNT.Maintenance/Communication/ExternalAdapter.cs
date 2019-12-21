using PE.DTO.Internal.Maintenance;
using SMF.Core.DC;
using SMF.Module.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;


namespace PE.MNT.Maintenance.Communication
{
  [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Reentrant, InstanceContextMode = InstanceContextMode.Single)]
  public class ExternalAdapter : ExternalAdapterBase, PE.Interfaces.Lite.IMaintenance
  {
    #region ctor
    public ExternalAdapter() : base(typeof(PE.Interfaces.Lite.IMaintenance)) { }
    #endregion

    #region HMI
    public async Task<DataContractBase> CreateEquipmentGroupAsync(DCEquipmentGroup dc)
    {
      return await HandleIncommingMethod(ExternalAdapterHandler.CreateEquipmentGroupAsync, dc);
    }
    public async Task<DataContractBase> DeleteEquipmentGroupAsync(DCEquipmentGroup dc)
    {
      return await HandleIncommingMethod(ExternalAdapterHandler.DeleteEquipmentGroupAsync, dc);
    }

    public async Task<DataContractBase> UpdateEquipmentGroupAsync(DCEquipmentGroup dc)
    {
      return await HandleIncommingMethod(ExternalAdapterHandler.UpdateEquipmentGroupAsync, dc);
    }

    public async Task<DataContractBase> CloneEquipmentAsync(DCEquipment dc)
    {
      return await HandleIncommingMethod(ExternalAdapterHandler.CloneEquipmentAsync, dc);
    }
    public async Task<DataContractBase> CreateEquipmentAsync(DCEquipment dc)
    {
      return await HandleIncommingMethod(ExternalAdapterHandler.CreateEquipmentAsync, dc);
    }

    public async Task<DataContractBase> UpdateEquipmentAsync(DCEquipment dc)
    {
      return await HandleIncommingMethod(ExternalAdapterHandler.UpdateEquipmentAsync, dc);
    }

    public async Task<DataContractBase> DeleteEquipmentAsync(DCEquipment dc)
    {
      return await HandleIncommingMethod(ExternalAdapterHandler.DeleteEquipmentAsync, dc);
    }


    #endregion


    #region Tracking (usage accumulation)

    public async Task<DataContractBase> AccumulateEquipmentUsageAsync(DCEquipmentAccu dc)
    {
      return await HandleIncommingMethod(ExternalAdapterHandler.AccumulateEquipmentUsageAsync, dc);
    }

    #endregion

  }
}
