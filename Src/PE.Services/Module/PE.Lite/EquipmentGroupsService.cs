using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using PE.DbEntity.Models;
using PE.HMIWWW.Core.Communication;
using PE.HMIWWW.Core.Service;
using PE.HMIWWW.Core.ViewModel;
using PE.HMIWWW.Services.Module.PE.Lite.Interfaces;
using PE.DTO.Internal.Maintenance;
using SMF.Core.DC;
using SMF.HMIWWW.UnitConverter;
using SMF.Module.Core;
using SMF.Module.Notification;
using SMF.RepositoryPatternExt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Data.Entity;
using PE.HMIWWW.ViewModel.Module.Lite.Maintenance;
using PE.HMIWWW.Core.Resources;

namespace PE.HMIWWW.Services.Module.PE.Lite
{
  public class EquipmentGroupsService : BaseService, IEquipmentGroupsService
  {
    public VM_EquipmentGroup GetEquipmentGroup(long id)
    {
      VM_EquipmentGroup result = null;

      using (PEContext ctx = new PEContext())
      {
        MNTEquipmentGroup grp = ctx.MNTEquipmentGroups
          .SingleOrDefault(x => x.EquipmentGroupId == id);
        result = grp == null ? null : new VM_EquipmentGroup(grp);
      }

      return result;
    }

    public VM_EquipmentGroup GetEmptyEquipmentGroup()
    {
      VM_EquipmentGroup result = new VM_EquipmentGroup();
      return result;
    }

    public DataSourceResult GetEquipmentGroupsList(ModelStateDictionary modelState, [DataSourceRequest] DataSourceRequest request)
    {
      DataSourceResult result = null;

      using (PEContext ctx = new PEContext())
      {
        result = ctx.MNTEquipmentGroups
          .ToDataSourceResult<MNTEquipmentGroup, VM_EquipmentGroup>(request, modelState, x => new VM_EquipmentGroup(x));
      }

      return result;
    }

    public async Task<VM_Base> CreateEquipmentGroup(ModelStateDictionary modelState, VM_EquipmentGroup vm)
    {
      VM_Base result = new VM_Base();

      if (!modelState.IsValid)
        return result;

      UnitConverterHelper.ConvertToSi(ref vm);

      DCEquipmentGroup dc = new DCEquipmentGroup();
      dc.EquipmentGroupCode = vm.EquipmentGroupCode;
			dc.EquipmentGroupName = vm.EquipmentGroupName;
			dc.Description = vm.EquipmentGroupDescription;

      SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.SendEquipmentGroupCreateRequest(dc);

      HandleWarnings(sendOfficeResult, ref modelState);

      return result;
    }

    public async Task<VM_Base> UpdateEquipmentGroup(ModelStateDictionary modelState, VM_EquipmentGroup vm)
    {
      VM_Base result = new VM_Base();

      if (!modelState.IsValid)
        return result;

      UnitConverterHelper.ConvertToSi(ref vm);

      DCEquipmentGroup dc = new DCEquipmentGroup();
      dc.EquipmentGroupId = vm.EquipmentGroupId;
      dc.EquipmentGroupCode = vm.EquipmentGroupCode;
      dc.EquipmentGroupName = vm.EquipmentGroupName;
      dc.Description = vm.EquipmentGroupDescription;

      SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.SendEquipmentGroupUpdateRequest(dc);

      HandleWarnings(sendOfficeResult, ref modelState);

      return result;
    }

    public async Task<VM_Base> DeleteEquipmentGroup(long id)
    {
      VM_Base result = new VM_Base();

      DCEquipmentGroup dc = new DCEquipmentGroup();
      dc.EquipmentGroupId = id;

      SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.SendEquipmentGroupDeleteRequest(dc);

      //HandleWarnings(sendOfficeResult, ref modelState);

      return result;
    }
  }
}
