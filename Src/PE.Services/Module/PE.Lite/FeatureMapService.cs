using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using PE.DbEntity.Models;
using PE.HMIWWW.Core.Resources;
using PE.HMIWWW.Core.ViewModel;
using PE.HMIWWW.Services.Module.PE.Lite.Interfaces;
using PE.HMIWWW.ViewModel.Module.Lite.Heat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Data.Entity;
using PE.HMIWWW.Core.Service;
using PE.HMIWWW.ViewModel.Module.Lite.WorkOrder;
using PE.HMIWWW.ViewModel.Module.Lite.FeatureMap;

namespace PE.HMIWWW.Services.Module.PE.Lite
{
  public class FeatureMapService : BaseService, IFeatureMap
    {


    public DataSourceResult GetFeatureMapOverList(ModelStateDictionary modelState, DataSourceRequest request)
    {

            using (PEContext ctx = new PEContext())
            {
                IQueryable<V_FeaturesMap> List = ctx.V_FeaturesMap.AsQueryable();

                return List.ToDataSourceResult<V_FeaturesMap, VM_FeatureMap>(request, modelState, data => new VM_FeatureMap(data));
            }


    }


  }
}
