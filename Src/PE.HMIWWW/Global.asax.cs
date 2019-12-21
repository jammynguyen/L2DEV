using PE.HMIWWW.Core;
using PE.HMIWWW.Core.Authorization;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace PE.HMIWWW
{
	public class MvcApplication : System.Web.HttpApplication
	{
		protected void Application_Start()
		{
			ViewEngines.Engines.Add(new SMFViewEngine());
			Database.SetInitializer<ApplicationDbContext>(null);
			AreaRegistration.RegisterAllAreas();
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
			RouteConfig.RegisterRoutes(RouteTable.Routes);
			BundleConfig.RegisterBundles(BundleTable.Bundles);
		}
	}
}
