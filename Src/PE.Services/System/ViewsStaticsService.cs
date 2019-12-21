using System;
using System.Collections.Generic;
using System.Linq;
using SMF.DbEntity.Model;
using SMF.DbEntity;
using SMF.RepositoryPatternExt;
using Kendo.Mvc.UI;
using System.Web.Mvc;
using PE.HMIWWW.ViewModel.System;
using PE.DbEntity;
using Kendo.Mvc.Extensions;
using PE.HMIWWW.Core.Service;

namespace PE.HMIWWW.Services.System
{
	public interface IViewsStaticsService
	{
		DataSourceResult GetViewsStatisticsList(ModelStateDictionary modelState, [DataSourceRequest]DataSourceRequest request);
	}

	public class ViewsStaticsService : BaseService, IViewsStaticsService
	{
		#region interface
		public DataSourceResult GetViewsStatisticsList(ModelStateDictionary modelState, [DataSourceRequest] DataSourceRequest request)
		{
			DataSourceResult returnValue = null;

			// VALIDATE ENTRY PARAMETERS
			if (!modelState.IsValid)
			{
				return returnValue;
			}
      //END OF VALIDATION

      //DB OPERATION 
      //using (SMFUnitOfWork uow = new SMFUnitOfWork())
      //{
      using (SMFContext SMFctx = new SMFContext())
      {
        //var list = uow.Repository<ViewsStatistic>().Query().OrderBy(z => z.OrderByDescending(s => s.TimePerRecord)).Get().ToList();
        List<ViewsStatistic> list = SMFctx.ViewsStatistics.OrderByDescending(o => o.TimePerRecord).ToList();
				returnValue = list.ToDataSourceResult<ViewsStatistic, VM_ViewsStatistics>(request, modelState, data => new VM_ViewsStatistics(data));
			}
			//END OF DB OPERATION

			return returnValue;
		}
		#endregion


	}
}
