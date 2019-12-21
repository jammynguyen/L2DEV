using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using PE.HMIWWW.ViewModel.Module.Lite.Furnace;
using PE.HMIWWW.ViewModel.Module.Lite.Heat;
using PE.HMIWWW.ViewModel.Module.Lite.Material;
using PE.HMIWWW.ViewModel.Module.Lite.WorkOrder;

namespace PE.HMIWWW.Services.Module.PE.Lite.Interfaces
{
	public interface IFurnaceService
	{
    IEnumerable<VM_Furnace> GetMaterialInFurnace();
    VM_RawMaterialOverview GetMaterialDetails(ModelStateDictionary modelState, long RawMaterialId);
    VM_HeatOverview GetHeatDetails(ModelStateDictionary modelState, long heatId);
    VM_WorkOrderOverview GetWorkOrderDetails(ModelStateDictionary modelState, long workOrderId);

  }
}
