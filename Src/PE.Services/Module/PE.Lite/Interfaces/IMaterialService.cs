using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using PE.HMIWWW.ViewModel.Module.Lite.Material;

namespace PE.HMIWWW.Services.Module.PE.Lite.Interfaces
{
    public interface IMaterialService
    {
        DataSourceResult GetMaterialSearchList(ModelStateDictionary modelState, DataSourceRequest request);
        VM_MaterialOverview GetMaterialById(long? materialId);

        VM_MaterialOverview GetMaterialDetails(ModelStateDictionary modelState, long MaterialId);
    }
}
