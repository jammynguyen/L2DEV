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
using PE.HMIWWW.ViewModel.Module.Lite.Asset;
using PE.HMIWWW.ViewModel.Module.Lite.Feature;

namespace PE.HMIWWW.Services.Module.PE.Lite
{
    public class AssetService : BaseService, IAssetService
    {


        public DataSourceResult GetAssetOverList(ModelStateDictionary modelState, DataSourceRequest request)
        {

            using (PEContext ctx = new PEContext())
            {
                IQueryable<V_Assets> List = ctx.V_Assets.AsQueryable();

                return ctx.V_Assets.ToDataSourceResult<V_Assets, VM_Asset>(request, modelState, data => new VM_Asset(data));
            }
        }

        public DataSourceResult GetFeatureByAssetId(ModelStateDictionary modelState, DataSourceRequest request, long assetId)
        {
            using (PEContext ctx = new PEContext())
            {
                IQueryable<MVHFeature> List = ctx.MVHFeatures.Where(x => x.FKAssetId == assetId).AsQueryable();

                return List.ToDataSourceResult<MVHFeature, VM_Feature>(request, modelState, data => new VM_Feature(data));
            }
        }


        public DataSourceResult GetTriggerOverViewList(ModelStateDictionary modelState, DataSourceRequest request)
        {
            using (PEContext ctx = new PEContext())
            {
                IQueryable<MVHTrigger> List = ctx.MVHTriggers.AsQueryable();
                //DataSourceResult res = ctx.Database.SqlQuery<DataSourceResult>("select triggercode, triggername, IsActive from V_TriggerOverview  group by triggercode,triggername,IsActive").FirstOrDefault();
                // return ctx.V_TriggerOverview.ToDataSourceResult<V_TriggerOverview, VM_TriggerOverView>(request, modelState, data => new VM_TriggerOverView(data.TriggerCode, data.TriggerName, data.IsActive));
               return List.ToDataSourceResult<MVHTrigger, VM_Triggers>(request, modelState, data => new VM_Triggers(data));

            }
        }


        public DataSourceResult GetTriggerDetailByTriggerCode(ModelStateDictionary modelState, DataSourceRequest request, string triggerCode)
        {
            using (PEContext ctx = new PEContext())
            {
                IQueryable<V_TriggerOverview> List = ctx.V_TriggerOverview.Where(x => x.TriggerCode == triggerCode).AsQueryable();

                return List.ToDataSourceResult<V_TriggerOverview, VM_TriggerOverView>(request, modelState, data => new VM_TriggerOverView(data));
            }
        }

    }
}
