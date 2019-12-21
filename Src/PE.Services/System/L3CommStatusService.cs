using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using PE.DbEntity.Enums;
using PE.DbEntity.Models;
using PE.HMIWWW.Core.Service;
using PE.HMIWWW.ViewModel.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PE.HMIWWW.Services.System
{

    public interface IL3CommStatusService
    {
        DataSourceResult GetL3TransferTableWorkOrderList(ModelStateDictionary modelState, [DataSourceRequest]DataSourceRequest request);

        DataSourceResult GetL3TransferTableGeneralList(ModelStateDictionary modelState, [DataSourceRequest] DataSourceRequest request);

    }
    public class L3CommStatusService : BaseService, IL3CommStatusService
    {
        public DataSourceResult GetL3TransferTableWorkOrderList(ModelStateDictionary modelState, [DataSourceRequest] DataSourceRequest request)
        {
            DataSourceResult result = null;

            using (PEContext ctx = new PEContext())
            {
                result = ctx.L3L2WorkOrderDefinition
                            .ToDataSourceResult<L3L2WorkOrderDefinition, VM_L3L2WorkOrderDefinition>(request, modelState, x => new VM_L3L2WorkOrderDefinition(x));
            }

            return result;
        }

        public DataSourceResult GetL3TransferTableGeneralList(ModelStateDictionary modelState, [DataSourceRequest] DataSourceRequest request)
        {
            DataSourceResult result = null;

            using (PEContext ctx = new PEContext())
            {
                result = ctx.V_L3L2TransferTablesSummary
                            .ToDataSourceResult<V_L3L2TransferTablesSummary, VM_L3TransferTableGeneral>(request, modelState, x => new VM_L3TransferTableGeneral(x));
            }

            return result;
        }


    }
}
