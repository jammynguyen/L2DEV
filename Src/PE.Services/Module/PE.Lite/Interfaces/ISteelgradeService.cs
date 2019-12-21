using Kendo.Mvc.UI;
using PE.HMIWWW.Core.ViewModel;
using PE.HMIWWW.ViewModel.Module.Lite.Steelgrade;
using PE.DTO.Internal.ProdManager;
using SMF.Core.DC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PE.HMIWWW.Services.Module.PE.Lite.Interfaces
{
  public interface ISteelgradeService
  {
    DataSourceResult GetSteelgradeList(ModelStateDictionary modelState, [DataSourceRequest]DataSourceRequest request);
    VM_Steelgrade GetSteelgrade(ModelStateDictionary modelState, long id);
    Task<VM_Base> CreateSteelgrade(ModelStateDictionary modelState, VM_Steelgrade steelgrade);
    Task<VM_Base> UpdateSteelgrade(ModelStateDictionary modelState, VM_Steelgrade steelgrade);
    Task<VM_Base> DeleteSteelgrade(ModelStateDictionary modelState, VM_Steelgrade steelgradeId);
    IList<VM_SteelGroup> GetSteelgradeGroups();
    VM_Steelgrade GetSteelgradeDetails(ModelStateDictionary modelState, long Id);
  }
}
