using Kendo.Mvc.UI;
using PE.HMIWWW.Core.ViewModel;
using PE.HMIWWW.ViewModel.Module.Lite.Material;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PE.HMIWWW.Services.Module.PE.Lite.Interfaces
{
   public interface IAssetService
    {
        DataSourceResult GetAssetOverList(ModelStateDictionary modelState, DataSourceRequest request);

        DataSourceResult GetFeatureByAssetId(ModelStateDictionary modelState, DataSourceRequest request,long assetId);

        DataSourceResult GetTriggerOverViewList(ModelStateDictionary modelState, DataSourceRequest request);

        DataSourceResult GetTriggerDetailByTriggerCode(ModelStateDictionary modelState, DataSourceRequest request, string triggerCode);
    }
}
