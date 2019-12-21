using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PE.HMIWWW.Core
{
	public class SMFViewEngine : RazorViewEngine
	{
		public SMFViewEngine()
		{
      string[] viewLocations = new[] {
								"~/Views/Module/{1}/{0}.cshtml",
								"~/Views/System/{1}/{0}.cshtml",
                // etc
            };

			this.PartialViewLocationFormats = viewLocations;
			this.ViewLocationFormats = viewLocations;
		}
	}
}
