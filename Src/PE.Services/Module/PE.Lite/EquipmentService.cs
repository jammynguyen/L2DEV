using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using PE.DbEntity.Models;
using PE.DbEntity.Enums;
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
using PE.HMIWWW.ViewModel.System;

namespace PE.HMIWWW.Services.Module.PE.Lite
{
  public class EquipmentService : BaseService, IEquipmentService
  {
    public VM_Equipment GetEquipment(long id)
    {
      VM_Equipment result = null;

      using (PEContext ctx = new PEContext())
      {
        MNTEquipment eq = ctx.MNTEquipments.Include(i => i.MNTEquipmentGroup)
          .SingleOrDefault(x => x.EquipmentId == id);
        result = eq == null ? null : new VM_Equipment(eq);
      }

      return result;
    }

    public VM_Equipment GetEmptyEquipment()
    {
      VM_Equipment result = new VM_Equipment();
      return result;
    }

    public DataSourceResult GetEquipmentList(ModelStateDictionary modelState, [DataSourceRequest] DataSourceRequest request)
    {
      DataSourceResult result = null;

      using (PEContext ctx = new PEContext())
      {
        result = ctx.MNTEquipments.Include(i => i.MNTEquipmentGroup)
          .ToDataSourceResult<MNTEquipment, VM_Equipment>(request, modelState, x => new VM_Equipment(x));
      }

      return result;
    }

    public async Task<VM_Base> CreateEquipment(ModelStateDictionary modelState, VM_Equipment vm)
    {
      VM_Base result = new VM_Base();

      if (!modelState.IsValid)
        return result;

      UnitConverterHelper.ConvertToSi(ref vm);

      DCEquipment dc = new DCEquipment();
      dc.AlarmValue = (long?)vm.AlarmValue;
      dc.EquipmentCode = vm.EquipmentCode;
      dc.EquipmentDescription = vm.EquipmentDescription;
      dc.EquipmentGroupId = vm.EquipmentGroupId;
      dc.EquipmentId = vm.EquipmentId;
      dc.EquipmentName = vm.EquipmentName;
      dc.ServiceExpires = vm.ServiceExpires;
      dc.WarningValue = (long?)vm.WarningValue;

      SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.SendEquipmentCreateRequest(dc);

      HandleWarnings(sendOfficeResult, ref modelState);

      return result;
    }

    public async Task<VM_Base> UpdateEquipment(ModelStateDictionary modelState, VM_Equipment vm)
    {
      VM_Base result = new VM_Base();

      if (!modelState.IsValid)
        return result;

      UnitConverterHelper.ConvertToSi(ref vm);

      DCEquipment dc = new DCEquipment();
      dc.AlarmValue = (long?)vm.AlarmValue;
      dc.EquipmentCode = vm.EquipmentCode;
      dc.EquipmentDescription = vm.EquipmentDescription;
      dc.EquipmentGroupId = vm.EquipmentGroupId;
      dc.EquipmentId = vm.EquipmentId;
      dc.EquipmentName = vm.EquipmentName;
      dc.ServiceExpires = vm.ServiceExpires;
      dc.WarningValue = (long?)vm.WarningValue;
			dc.EqStatus = (EquipmentStatus)vm.EquipmentStatus;
      dc.Remark = vm.Remark;

      SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.SendEquipmentUpdateRequest(dc);

      HandleWarnings(sendOfficeResult, ref modelState);

      return result;
    }

    public async Task<VM_Base> DeleteEquipment(long id)
    {
      VM_Base result = new VM_Base();

      DCEquipment dc = new DCEquipment();
      dc.EquipmentId = id;

      SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.SendEquipmentDeleteRequest(dc);

      //HandleWarnings(sendOfficeResult, ref modelState);

      return result;
    }

    public async Task<VM_Base> CloneEquipment(ModelStateDictionary modelState, VM_Equipment vm)
    {
      VM_Base result = new VM_Base();
      UnitConverterHelper.ConvertToSi(ref vm);
      DCEquipment dc = new DCEquipment();
      dc.EquipmentCode = vm.EquipmentCode;
      dc.EquipmentId = vm.EquipmentId;
      dc.EquipmentName = vm.EquipmentName;
      dc.EquipmentDescription = vm.EquipmentDescription;
      dc.WarningValue = vm.WarningValue;
      dc.AlarmValue = vm.AlarmValue;

      SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.SendEquipmentCloneRequest(dc);

      HandleWarnings(sendOfficeResult, ref modelState);

      return result;
    }

    public DataSourceResult GetEquipmentHistoryList(long id, ModelStateDictionary modelState, [DataSourceRequest] DataSourceRequest request)
    {
      DataSourceResult result = null;

      using (PEContext ctx = new PEContext())
      {
        result = ctx.MNTEquipmentHistories.Where(w => w.FKEquipmentId == id).OrderBy(o => o.CreatedTs)
          .ToDataSourceResult<MNTEquipmentHistory, VM_EquipmentHistory>(request, modelState, x => new VM_EquipmentHistory(x));
      }

      return result;
    }
    public IList<VM_EquipmentGroup> GetEquipmentGroupList()
    {
      List<VM_EquipmentGroup> result = new List<VM_EquipmentGroup>();
      using (PEContext ctx = new PEContext())
      {
        IQueryable<MNTEquipmentGroup> dbList = ctx.MNTEquipmentGroups.OrderBy(o=>o.EquipmentGroupCode).AsQueryable();
        foreach (MNTEquipmentGroup item in dbList)
        {
          result.Add(new VM_EquipmentGroup(item));
        }
      }
      return result;
    }

    public VM_LongId GetEquipmentHistory(long id)
    {
      VM_LongId vm = new VM_LongId();
      vm.Id = id;
      return vm;
    }
  }
}
